using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggerEngine.Util;
using LoggerEngine.Controller;
using LoggerEngine.Entities;

namespace LoggerEngine.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class LoggerEntitiesTest
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyLogHasCreatedStatusWhenItIsCreated()
        {
            //Arrange
            var loggerController = new LoggerController();

            //Act
            var logToSave = new LoggerEntities.Log("LogType", "LogMessage");

            //Assert
            Assert.IsTrue(logToSave.LogStatus.Equals(AppConstant.LogStatus.Created));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyLogHasSavedStatusWhenPerformLogSuccessfully()
        {
            //Arrange
            var loggerController = new LoggerController();
            var engineLogger = AppConstant.LogEngine.Console;
            var assemblyName = "ConsoleLogger";
            var domainName = "ConsoleNewDomain";
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