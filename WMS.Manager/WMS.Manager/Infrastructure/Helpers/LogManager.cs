using Serilog;

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WMS.Manager.Infrastructure.Helpers
{
    public sealed class LogManager
    {

        private string _classFullName;
        public static LogManager GetCurrentClassLogger()
        {
            LogManager logger = new();
            logger._classFullName = NameOfCallingClass();

            return logger;
        }

        public void Trace([CallerMemberName] string caller = null) =>
           Log.Logger.Verbose("{Caller}", GetFullCallerName(caller));


        public void Trace(object value, [CallerMemberName] string caller = null) =>
            Log.Logger.Verbose("{Caller} {@Value}", GetFullCallerName(caller), value);


        public void Debug(object value, [CallerMemberName] string caller = null) =>
            Log.Logger.Debug("{Caller} {@Value}", GetFullCallerName(caller), value);


        public void Info(object value, [CallerMemberName] string caller = null) =>
            Log.Logger.Information("{Caller} {@Value}", GetFullCallerName(caller), value);


        public void Error(Exception ex, [CallerMemberName] string caller = null) =>
            Log.Logger.Error(ex, "{Caller}", GetFullCallerName(caller));

        private static string NameOfCallingClass()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }

                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }

        private string GetFullCallerName(string caller) => $"{_classFullName}.{caller}";
    }
}