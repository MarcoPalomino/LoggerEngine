//===============================================================================
// LoggerEngine.AssemblyManager
// Assembly Manager Application Block
// Version	: 1.0
// Author	: Marco Palomino
// Contact	: marcopalomino.v@gmail.com.pe
//===============================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using LoggerEngine.Entities;
using LoggerEngine.Util;

namespace LoggerEngine.AssemblyManager
{
    public class LoggerAssemblyManager
    {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, AppDomain> _currentDomains;
        public Dictionary<string, AppDomain> CurrentDomains
        {
            get
            {
                if (_currentDomains == null)
                    _currentDomains = new Dictionary<string, AppDomain>();

                return _currentDomains;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, Assembly> _currentAssemblies;
        public Dictionary<string, Assembly> CurrentAssemblies
        {
            get
            {
                if (_currentAssemblies == null)
                    _currentAssemblies = new Dictionary<string, Assembly>();

                return _currentAssemblies;
            }
        }
        
        #region First Solution - Without Proxy manager

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerEngineType"></param>
        /// <param name="assemblyName"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public bool LoadAssembly(string loggerEngineType, string assemblyName, string domainName)
        {
            ValidationUtil.CheckArgumentNull(loggerEngineType,"loggerEngineType");
            ValidationUtil.CheckArgumentNull(assemblyName, "assemblyName");
            ValidationUtil.CheckArgumentNull(domainName, "domainName");

            bool assemblyLoaded = true;
            try
            {
                var adSetup = new AppDomainSetup();
                adSetup.ApplicationBase = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                // Create the new AppDomain
                var newDomain = AppDomain.CreateDomain(domainName, null, adSetup);

                // Load the Assembly
                var newAssembly = newDomain.Load(assemblyName);

                CurrentDomains[loggerEngineType] = newDomain;
                CurrentAssemblies[loggerEngineType] = newAssembly;
            }
            catch (Exception ex)
            {
                assemblyLoaded = false;
            }

            return assemblyLoaded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerEngineType"></param>
        /// <param name="assemblyName"></param>
        /// <param name="domainName"></param>
        /// <returns></returns>
        public bool UnloadAssembly(string loggerEngineType, string assemblyName, string domainName)
        {
            ValidationUtil.CheckArgumentNull(loggerEngineType, "loggerEngineType");
            ValidationUtil.CheckArgumentNull(assemblyName, "assemblyName");
            ValidationUtil.CheckArgumentNull(domainName, "domainName");

            var assemblyUnloadedFromDomain = true;
            try
            {

                if (!CurrentDomains.ContainsKey(loggerEngineType))
                    return true;

                var domainNameToUnload = CurrentDomains[loggerEngineType];

                AppDomain.Unload(domainNameToUnload);
                CurrentDomains.Remove(loggerEngineType);
                CurrentAssemblies.Remove(loggerEngineType);
            }
            catch (Exception ex)
            {
                assemblyUnloadedFromDomain = false;
            }

            return assemblyUnloadedFromDomain;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentEngineSelected"></param>
        /// <returns></returns>
        public bool IsAlreadyLoaded(string currentEngineSelected)
        {
            var isAlreadyLoaded = CurrentAssemblies.ContainsKey(currentEngineSelected);
            return isAlreadyLoaded;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggerEngineType"></param>
        /// <param name="methodToInvoke"></param>
        /// <returns></returns>
        public bool ProcessMethod(string loggerEngineType, string methodToInvoke, LoggerEntities.Log logMessage )
        {
            ValidationUtil.CheckArgumentNull(loggerEngineType, "loggerEngineType");
            ValidationUtil.CheckArgumentNull(methodToInvoke, "methodToInvoke");
            ValidationUtil.CheckArgumentNull(logMessage, "logMessage");

            bool methodProcessed = true;
            try
            {
                if (!CurrentAssemblies.ContainsKey(loggerEngineType))
                    return false;

                var assemblyToUse = CurrentAssemblies[loggerEngineType];

                //Getting the Logger component -- All assemblies (DatabaseLogger, FileLogger, ConsoleLogger) has the same Name
                Type type = assemblyToUse.GetTypes().FirstOrDefault(typess=>typess.Name == "Logger");
                var totalMethods = type.GetMethods();
                var loggerMethod = type.GetMethod(methodToInvoke);

                object classInstance = Activator.CreateInstance(type, null);
                var logMessageParams = new object[] { logMessage.LogType, logMessage.LogMessage };
                object result = loggerMethod.Invoke(classInstance, logMessageParams);

                //The DoLog method in all assemblies should return a boolean
                methodProcessed = Convert.ToBoolean(result);

                //updating the status of the Log
                logMessage.LogStatus = methodProcessed ? AppConstant.LogStatus.Saved : AppConstant.LogStatus.Failed;

            }
            catch (Exception ex)
            {
                methodProcessed = false;
                logMessage.LogStatus = AppConstant.LogStatus.Failed;
            }

            return methodProcessed;
        }

        #endregion


        #region Second Solution - Using proxy manager Need further analysis, the Invoke method cannot be called within Proxy Manager.Only properties, method information is accessed

        /*
        ///
        public bool LoadAssembly(string assemblyPath, string domainName)
        {
            // Verify if AssemblyPath exists
            if (!File.Exists(assemblyPath))
                return false;

            // Verify if the Assembly was already loaded
            if (CurrentAssemblies.ContainsKey(assemblyPath))
                return false;

            // Verify if the AppDomain exists; otherwise, create a new one
            AppDomain appDomain = null;
            if (CurrentDomains.ContainsKey(domainName))
                appDomain = CurrentDomains[domainName];
            else
            {
                appDomain = CreateChildDomain(AppDomain.CurrentDomain, domainName);
                CurrentDomains[domainName] = appDomain;
            }

            // load the assembly regarding the AppDomain specified
            try
            {
                Type proxyType = typeof(AssemblyProxy);
                
                //Proxy logic for loading the Assembly
            }
            catch
            { }

            return false;
        }
        
        private AppDomain CreateChildDomain(AppDomain mainDomain, string newDomainName)
        {
            var evidence = new Evidence(mainDomain.Evidence);
            var setup = mainDomain.SetupInformation;
            return AppDomain.CreateDomain(newDomainName, evidence, setup);
        }
        
        public bool UnloadAssembly(string assemblyPath)
        {
            //Verify if Assembly Path exists
            if (!File.Exists(assemblyPath))
                return false;

            // Verify if it was already Loaded
            if (CurrentAssemblies.ContainsKey(assemblyPath) && CurrentProxies.ContainsKey(assemblyPath))
            {
                // Verify if there are more assemblies in the AppDomain provided
                AppDomain appDomain = CurrentAssemblies[assemblyPath];
                int count = CurrentAssemblies.Values.Where(a => a == appDomain).Count();
                if (count != 1)
                    return false;

                try
                {
                    // Remove the Assembly from the dictionaries
                    
                    CurrentDomains.Remove(appDomain.FriendlyName);
                    AppDomain.Unload(appDomain);

                    CurrentAssemblies.Remove(assemblyPath);
                    CurrentProxies.Remove(assemblyPath);

                    return true;
                }
                catch
                {
                }
            }

            return false;
        }
        */
        #endregion
    }
}
