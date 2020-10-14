using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Context;
using Serilog.Configuration;

namespace IRIS10ClockITWPF.Classes
{
    static class ErrorHandler
    {
        public static void ProcessError(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Log.Error("Error Occurred: {@0}", ex);
        }
    }
}
