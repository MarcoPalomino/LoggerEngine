using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggerEngine.Util;
using LoggerEngine.Controller;
using LoggerEngine.Entities;

namespace LoggerEngine.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyFileFactoryAssemblyIsLoaded()
        {
            //Arrange
            var loggerController = new LoggerController();
            var engineLogger = AppConstant.LogEngine.File;
            var assemblyName = "FileLogger";
            var domainName = "FileNewDomain";

            loggerController.LoadAssembly(engineLogger, assemblyName, domainName);

            //Act
            var assemblyLoaded = loggerController.IsAlreadyLoaded(AppConstant.LogEngine.File);

            //Assert
            Assert.IsTrue(assemblyLoaded);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyLogIsSuccessfullySavedFromFileEngine()
        {
            //Arrange
            var loggerController = new LoggerController();
            var engineLogger = AppConstant.LogEngine.File;
            var assemblyName = "FileLogger";
            var domainName = "FileNewDomain";
            var logToSave = new LoggerEntities.Log("LogType","LogMessage");
            var methodToInvoke = AppConstant.LogMethod.DoLog;

            loggerController.LoadAssembly(engineLogger, assemblyName, domainName);

            //Act
            loggerController.ProcessMethod(engineLogger, methodToInvoke, logToSave);

            //Assert
            Assert.IsTrue(logToSave.LogStatus.Equals(AppConstant.LogStatus.Saved));
        }
    }
}
