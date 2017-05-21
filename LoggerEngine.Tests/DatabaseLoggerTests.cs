using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggerEngine.Util;
using LoggerEngine.Controller;
using LoggerEngine.Entities;

namespace LoggerEngine.Tests
{
    [TestClass]
    public class DatabaseLoggerTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyDatabaseFactoryAssemblyIsLoaded()
        {
            //Arrange
            var loggerController = new LoggerController();
            var engineLogger = AppConstant.LogEngine.Database;
            var assemblyName = "DatabaseLogger";
            var domainName = "DatabaseNewDomain";

            loggerController.LoadAssembly(engineLogger, assemblyName, domainName);

            //Act
            var assemblyLoaded = loggerController.IsAlreadyLoaded(AppConstant.LogEngine.Database);

            //Assert
            Assert.IsTrue(assemblyLoaded);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyLogIsSuccessfullySavedFromDatabaseEngine()
        {
            //Arrange
            var loggerController = new LoggerController();
            var engineLogger = AppConstant.LogEngine.Database;
            var assemblyName = "DatabaseLogger";
            var domainName = "DatabaseNewDomain";
            var logToSave = new LoggerEntities.Log("LogType", "LogMessage");
            var methodToInvoke = AppConstant.LogMethod.DoLog;

            loggerController.LoadAssembly(engineLogger, assemblyName, domainName);
            
            //Act
            loggerController.ProcessMethod(engineLogger, methodToInvoke, logToSave);

            //Assert
            Assert.IsTrue(logToSave.LogStatus.Equals(AppConstant.LogStatus.Saved));
        }
    }
}
