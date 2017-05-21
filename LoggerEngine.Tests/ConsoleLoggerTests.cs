using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoggerEngine.Util;
using LoggerEngine.Controller;
using LoggerEngine.Entities;

namespace LoggerEngine.Tests
{
    /// <summary>
    /// Descripción resumida de ConsoleLoggerTests
    /// </summary>
    [TestClass]
    public class ConsoleLoggerTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyConsoleFactoryAssemblyIsLoaded()
        {
            //Arrange
            var loggerController = new LoggerController();
            var engineLogger = AppConstant.LogEngine.Console;
            var assemblyName = "ConsoleLogger";
            var domainName = "ConsoleNewDomain";

            loggerController.LoadAssembly(engineLogger, assemblyName, domainName);

            //Act
            var assemblyLoaded = loggerController.IsAlreadyLoaded(AppConstant.LogEngine.Console);

            //Assert
            Assert.IsTrue(assemblyLoaded);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VerifyLogIsSuccessfullySavedFromConsoleEngine()
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
