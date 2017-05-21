namespace LoggerEngine.Util
{
    public class AppConstant
    {
        /// <summary>
        /// Main Method To Invoke with Reflection
        /// </summary>
        public class LogMethod
        {
            public const string DoLog = "DoLog";
        }

        /// <summary>
        /// 
        /// </summary>
        public class LogType
        {
            public const string Warning = "Warning";
            public const string Informative = "Informative";
            public const string Error = "Error";
        }

        /// <summary>
        /// 
        /// </summary>
        public class LogEngine
        {
            public const string Database = "Database";
            public const string File = "File";
            public const string Console = "Console";
        }

        /// <summary>
        /// 
        /// </summary>
        public class LogStatus
        {
            public const string Created = "Created";
            public const string Failed = "Failed";
            public const string Saved = "Saved";
        }
    }
}
