using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggerEngine.Util;
using LoggerEngine.Controller;
using System.Collections.Generic;

namespace LoggerEngine.AssemblyManager.Tests
{
    [TestClass()]
    public class AssemblyManagerTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void VerifyLoadAssemblyIsWorking()
        {
            var loggerController = new LoggerController();
            var assemblyName = "FileLogger";
            var domainName = "NewDomainFile";

            loggerController.LoadAssembly(AppConstant.LogEngine.File, assemblyName, domainName);

            var assemblyLoaded = loggerController.IsAlreadyLoaded(AppConstant.LogEngine.File);

            Assert.IsTrue(assemblyLoaded);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void VerifyDisplayLoadedAssembliesIsWorking()
        {
            //Arrange
            var loggerController = new LoggerController();
            var assemblyName = "FileLogger";
            var domainName = "NewDomainFile";
            var loadedAssemblies = new List<string>();

            loggerController.LoadAssembly(AppConstant.LogEngine.File, assemblyName, domainName);

            //Act
            loadedAssemblies = loggerController.GetCurrentAssemblies();

            //Assert
            Assert.IsTrue(loadedAssemblies.Count == 1);
        }
    }
}