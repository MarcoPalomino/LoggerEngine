//===============================================================================
// LoggerEngine.Entities
// Looger Entities Application Block
// Version	: 1.0
// Author	: Marco Palomino
// Contact	: marcopalomino.v@gmail.com.pe
//===============================================================================

using System.Collections.Generic;
using LoggerEngine.Util;

namespace LoggerEngine.Entities
{
    public class LoggerEntities
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> CurrentLogTypes = new List<string>
        {
            AppConstant.LogType.Warning,
            AppConstant.LogType.Informative,
            AppConstant.LogType.Error
        };

        /// <summary>
        /// 
        /// </summary>
        public List<string> AvailableAssemblies = new List<string>
        {
            AppConstant.LogType.Warning,
            AppConstant.LogType.Informative,
            AppConstant.LogType.Error
        };

        /// <summary>
        /// 
        /// </summary>
        public List<LogEngine> CurrentLogEngines = new List<LogEngine> {
            new LogEngine(AppConstant.LogEngine.Database),
            new LogEngine(AppConstant.LogEngine.File),
            new LogEngine(AppConstant.LogEngine.Console),
        };
        
        /// <summary>
        /// 
        /// </summary>
        public class LogEngine
        {
            public string LogEngineName;

            public LogEngine(string name) {
                LogEngineName = name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class Log {

            public string LogType;
            public string LogMessage;
            public string LogStatus;

            public Log(string logtype, string logMessage) {
                LogType = logtype;
                LogMessage = logMessage;
                LogStatus = AppConstant.LogStatus.Created;
            }
        }
    }
}

