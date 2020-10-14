using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IRIS10ClockITWPF.Classes
{
    public static class CryptoHelper
    {
        public static void ComputePassword(string plainPassword, out string passwordHash, out string passwordSalt)
        {
            passwordSalt = GenerateSalt();
            passwordHash = ComputeHash(plainPassword, passwordSalt);
        }

        public static string GenerateSalt()
        {
            byte[] data = new byte[66];
            new RNGCryptoServiceProvider().GetBytes(data);

            return Convert.ToBase64String(data);
        }

        public static string ComputeHash(string plainPassword, string passwordSalt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainPassword);
            byte[] saltBytes = Convert.FromBase64String(passwordSalt);

            using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 5))
            {
                byte[] hashedPasswordBytes = hasher.GetBytes(129);
                return Convert.ToBase64String(hashedPasswordBytes);
            }
        }

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }

        public static string Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Unzip(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        public static string GeneratePassword()
            {
                // Generate a password that meets the reuirements.

                const string LOWER = "abcdefghijklmnopqrstuvwxyz";
                const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string NUMBER = "0123456789";
                //const string SPECIAL = @"~!@#$%^&*():;[]{}<>,.?/\|";

      
                // Make a list of allowed characters.
                string allowed = "";
                 allowed += LOWER;
                allowed += UPPER;
                allowed += NUMBER;
                //allowed += SPECIAL;

                // Pick the number of characters.
                int min_chars = 8;
                int max_chars = 16;
                int num_chars = RandomInteger(min_chars, max_chars);

                // Satisfy requirements.
                string password = "";
                if ((password.IndexOfAny(LOWER.ToCharArray()) == -1))
                    password += RandomChar(LOWER);
                if ((password.IndexOfAny(UPPER.ToCharArray()) == -1))
                    password += RandomChar(UPPER);
                if ( (password.IndexOfAny(NUMBER.ToCharArray()) == -1))
                    password += RandomChar(NUMBER);
                //if ((password.IndexOfAny(SPECIAL.ToCharArray()) == -1))
                //    password += RandomChar(SPECIAL);


                // Add the remaining characters randomly.
                while (password.Length < num_chars)
                    password += allowed.Substring(
                        RandomInteger(0, allowed.Length - 1), 1);

                // Randomize (to mix up the required characters at the front).
                password = RandomizeString(password);

                return password;
            }
    

        // The random number provider.
        private static RNGCryptoServiceProvider Rand = new RNGCryptoServiceProvider();

        // Return a random integer between a min and max value.
        private static int RandomInteger(int min, int max)
        {
            uint scale = uint.MaxValue;
            while (scale == uint.MaxValue)
            {
                // Get four random bytes.
                byte[] four_bytes = new byte[4];
                Rand.GetBytes(four_bytes);

                // Convert that into an uint.
                scale = BitConverter.ToUInt32(four_bytes, 0);
            }

            // Add min to the scaled difference between max and min.
            return (int) (min + (max - min) * (scale / (double) uint.MaxValue));
        }

        private static string RandomChar(string str)
        {
            return str.Substring(RandomInteger(0, str.Length - 1), 1);
        }

        // Return a random permutation of a string.
        private static string RandomizeString(string str)
        {
            string result = "";
            while (str.Length > 0)
            {
                // Pick a random character.
                int i = RandomInteger(0, str.Length - 1);
                result += str.Substring(i, 1);
                str = str.Remove(i, 1);
            }
            return result;
        }

        public static string Encrypt(string encryptString, string encryptKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decrypt(string cipherText, string encryptKey)
        {
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
