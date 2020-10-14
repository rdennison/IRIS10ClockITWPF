using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Serilog;
using IRIS10ClockITWPF.Models;
using IRIS10ClockITWPF.Classes;

namespace IRIS10ClockITWPF.UserControls
{
    /// <summary>
    /// Interaction logic for ucLogin.xaml
    /// </summary>
    public partial class ucLogin 
    {
        private IEnumerable<IRISUserModel> data;

        public ucLogin()
        {
            InitializeComponent();

            //        if (!String.IsNullOrEmpty(Properties.Settings.Default.UserName))
            //        {
            //            UserName.Text = Properties.Settings.Default.UserName;              
            //        }

            //        if (Properties.Settings.Default.Check == true)
            //        {
            //            UserName.Text = Properties.Settings.Default.UserName;
            //            RememberMeCheckBox.IsChecked = true;
            //        }

            //        data = CoreService.LoadModel("IRISUser").Cast<IRISUserModel>();

            //        var queryUser = from UserData in data
            //                        select UserData;

            //        if (queryUser.Count() == 0)
            //        {
            //            string WelcomeMessage = "Welcome to IRIS Remote Inspections version " + RunningConfigVM.IRISRemoteVersion + "\n\n" +
            //"This appears to be the first time running this application and we need to do some initial setup (approximately 5 minutes). \n\n" +
            //"You will need to be connected to the internet so that the application can contact the IRIS server to copy your data down to this computer.\n\n" +
            //"NOTE: You may need to contact your IRIS Administrator to ensure you have been granted priveleges to run this application.\n\n" +
            //"The first step is logging in with your IRIS username and password.\n\n" +
            //"Are you ready to get started?";

            //            MessageBoxResult result = MessageBox.Show(WelcomeMessage, "IRIS Remote Inspection version " + RunningConfigVM.IRISRemoteVersion + " First Time Startup", MessageBoxButton.YesNo);
            //            if (result == MessageBoxResult.No)
            //            {
            //                Application.Current.Shutdown();
            //            }

            //        }


        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            //IEnumerable<IRISUserModel> data = CoreService.LoadModel("IRISUser").Cast<IRISUserModel>();

            var queryUser = from UserData in data
                            where !String.IsNullOrEmpty(UserData.Email) && UserData.Email.ToUpper() == UserName.Text.ToUpper()
                            select UserData;

            if (queryUser.Count() > 0)
            {
                IRISUserModel currentUser = queryUser.First();
                LocalLogin(currentUser);
            }
            else
            {
                RemoteLogin();
            }
        }

        private void LocalLogin(IRISUserModel currentUser)
        {
            Log.Debug("LocalLogin: {0}", currentUser.Email);

            string PasswordCypher = CryptoHelper.ComputeHash(password.Password, currentUser.SALT);
            if (currentUser.HashPassword == PasswordCypher)
            {
                //IEnumerable<Config> configData = CoreService.LoadModel("Config").Cast<Config>();
                //Config currentConfig = configData.First();
                //this.Visibility = Visibility.Hidden;

                //RunningConfigVM.User_Key = currentUser.User_Key.Value;
                //RunningConfigVM.Tenant_Key = currentConfig.Tenant_Key;
                //RunningConfigVM.Projection = currentConfig.Projection;
                //RunningConfigVM.FirstTimeLoad = false;

                //using (FileStream fs = new FileStream(RunningConfigVM.LicensePath, FileMode.Open))
                //{
                //    using (TextReader tr = new StreamReader(fs))
                //    {
                //        RunningConfigVM.License = tr.ReadToEnd().Trim();
                //    }
                //}

                //if (RememberMeCheckBox.IsChecked == true)
                //{
                //    Properties.Settings.Default.UserName = UserName.Text;
                //    Properties.Settings.Default.Check = true;
                //    Properties.Settings.Default.Save();
                //}

                //if (RememberMeCheckBox.IsChecked == false)
                //{
                //    Properties.Settings.Default.UserName = "";
                //    Properties.Settings.Default.Check = false;
                //    Properties.Settings.Default.Save();
                //}

                //Window mw = Application.Current.MainWindow;
                //switch (Properties.Settings.Default.FormLoad)
                //{
                //    case "Culverts": (mw as MainWindow).CulvertForm.Visibility = Visibility.Visible; break;
                //    case "Signs": (mw as MainWindow).SignForm.Visibility = Visibility.Visible; break;
                //    case "Roads": (mw as MainWindow).RoadForm.Visibility = Visibility.Visible; break;
                //    default: (mw as MainWindow).WelcomeMessage.Visibility = Visibility.Visible; break;
                //}
                //Log.Information("Successful Login for: {0}, Settings: {@1}", currentUser.Email, Properties.Settings.Default);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Invalid user name or password", "Invalid user name or password", MessageBoxButton.OK);
                Log.Error("Invalid username or password");
            }


        }

        private async void RemoteLogin()
        {
            Log.Debug("RemoteLogin");

            //RemoteLoginReturnVM login = new RemoteLoginReturnVM();
            //RemoteLoginVM userLogin = new RemoteLoginVM()
            //{
            //    UserName = UserName.Text,
            //    PassHash = CryptoHelper.Encrypt(password.Password, UserName.Text)
            //};
            //try
            //{
                //login = await CoreService.RemoteLogin(userLogin);

                //if (login.ErrorMsg == null)
                //{
                //    RunningConfigVM.License = login.License;
                //    RunningConfigVM.User_Key = login.User_Key;
                //    RunningConfigVM.FirstTimeLoad = true;

                //    if (File.Exists(RunningConfigVM.LicensePath)) File.Delete(RunningConfigVM.LicensePath); //ensure that this file only has the new data

                //    using (FileStream fs = new FileStream(RunningConfigVM.LicensePath, FileMode.OpenOrCreate))
                //    {
                //        using (TextWriter tw = new StreamWriter(fs, Encoding.UTF8, 1024, true))
                //        {
                //            tw.WriteLine(login.License);
                //        }
                //        fs.SetLength(fs.Position);
                //    }
                //    File.SetAttributes(RunningConfigVM.LicensePath, File.GetAttributes(RunningConfigVM.LicensePath) | FileAttributes.Hidden);

                //    Window mw = Application.Current.MainWindow;                    
                //    (mw as MainWindow).LoginForm.Visibility = Visibility.Hidden;

                //    Synchronize SyncWindow = new Synchronize();
                //    SyncWindow.ShowDialog();
                //    }
                //    else
                //    {
                //        MessageBoxResult result = MessageBox.Show(login.ErrorMsg, "Login Error", MessageBoxButton.OK);
                //        Log.Error("RemoteLogin Error: {@0}", login);
                //    }
                //}

                //catch(Exception ex)
                //{
                //    MessageBoxResult result = MessageBox.Show("Error connecting to IRIS Server", "Login Error", MessageBoxButton.OK);
                //    Log.Error("RemoteLogin Error: {@0}", ex);
                //}
                //}
                //}
            
            }
    }
}

