//===============================================================================
// LoggerEngine.Controller
// Looger Controller Application Block
// Version	: 1.0
// Author	: Marco Palomino
// Contact	: marcopalomino.v@gmail.com.pe
//===============================================================================

using LoggerEngine.AssemblyManager;
using LoggerEngine.Entities;
using LoggerEngine.Util;
using System.Collections.Generic;

namespace LoggerEngine.Controller
{
    /// <summary>
    /// 
    /// </summary>
    public class LoggerController
    {
        /// <summary>
        /// 
        /// </summary>
        public LoggerAssemblyManager _loggerAssemblyManager = new LoggerAssemblyManager();
        public LoggerAssemblyManager LoggerAssemblyManager
        {
            get
            {
                if (_loggerAssemblyManager == null)
                    _loggerAssemblyManager = new LoggerAssemblyManager();

                return _loggerAssemblyManager;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerEngineType"></param>
        /// <param name="assemblyName"></param>
        /// <param name="domainName"></param>
        public bool LoadAssembly(string loggerEngineType, string assemblyName, string domainName) {

            return LoggerAssemblyManager.LoadAssembly(loggerEngineType, assemblyName, domainName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerEngineType"></param>
        /// <param name="assemblyName"></param>
        /// <param name="domainName"></param>
        public bool UnloadAssembly(string loggerEngineType, string assemblyName, string domainName)
        {
            return LoggerAssemblyManager.UnloadAssembly(loggerEngineType, assemblyName, domainName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerEngineType"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        public bool ProcessMethod(string loggerEngineType, string method,  LoggerEntities.Log logMessage)
        {
            return LoggerAssemblyManager.ProcessMethod(loggerEngineType, method, logMessage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentEngineSelected"></param>
        /// <returns></returns>
        public bool IsAlreadyLoaded(string currentEngineSelected)
        {
            return LoggerAssemblyManager.IsAlreadyLoaded(currentEngineSelected);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<string> GetCurrentAssemblies()
        {
            var currentLoadedAssemblies = new List<string>();
            LoggerAssemblyManager.CurrentAssemblies.ForEach(assembly =>
            {
                currentLoadedAssemblies.Add(assembly.Value.FullName);
            });

            return currentLoadedAssemblies;
        }
    }
}
