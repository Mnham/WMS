using Serilog;

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WMS.Manager.Infrastructure.Helpers
{
    /// <summary>
    /// Представляет Логгер.
    /// </summary>
    public sealed class LogManager
    {
        /// <summary>
        /// Полное имя класса.
        /// </summary>
        private string _classFullName;

        /// <summary>
        /// Возвращает экземпляр Логгера на класс, вызвавший метод.
        /// </summary>
        public static LogManager GetCurrentClassLogger()
        {
            LogManager logger = new();
            logger._classFullName = NameOfCallingClass();

            return logger;
        }

        /// <summary>
        /// Записывает в журнал события уровня Verbose.
        /// </summary>
        public void Verbose([CallerMemberName] string caller = null) =>
           Log.Logger.Verbose("{Caller}", GetFullCallerName(caller));

        /// <summary>
        /// Записывает в журнал события уровня Verbose.
        /// </summary>
        public void Verbose(object value, [CallerMemberName] string caller = null) =>
            Log.Logger.Verbose("{Caller} {@Value}", GetFullCallerName(caller), value);

        /// <summary>
        /// Записывает в журнал события уровня Debug.
        /// </summary>
        public void Debug(object value, [CallerMemberName] string caller = null) =>
            Log.Logger.Debug("{Caller} {@Value}", GetFullCallerName(caller), value);

        /// <summary>
        /// Записывает в журнал события уровня Info.
        /// </summary>
        public void Info(object value, [CallerMemberName] string caller = null) =>
            Log.Logger.Information("{Caller} {@Value}", GetFullCallerName(caller), value);

        /// <summary>
        /// Записывает в журнал события уровня Error.
        /// </summary>
        public void Error(Exception ex, [CallerMemberName] string caller = null) =>
            Log.Logger.Error(ex, "{Caller}", GetFullCallerName(caller));

        /// <summary>
        /// Возвращает полное имя класса.
        /// </summary>
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

        /// <summary>
        /// Возвращает полное имя вызывающего метода.
        /// </summary>
        private string GetFullCallerName(string caller) => $"{_classFullName}.{caller}";
    }
}