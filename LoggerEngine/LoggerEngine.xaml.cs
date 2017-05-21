using System.Windows;
using LoggerEngine.Entities;
using LoggerEngine.Controller;
using System;
using LoggerEngine.Util;

namespace LoggerEngine
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Properties

        private LoggerEntities _loggerEntities;
        private LoggerEntities LoggerEntities
        {
            get
            {
                if (_loggerEntities == null)
                    return _loggerEntities = new LoggerEntities();

                return _loggerEntities;
            }
        }

        private LoggerController _loggerController;
        private LoggerController LoggerController
        {
            get
            {
                if (_loggerController == null)
                    return _loggerController = new LoggerController();

                return _loggerController;
            }
        }

        #endregion

        #region Main 
        public MainWindow()
        {
            InitializeComponent();
            InitializeLoggerTypes();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitializeLoggerTypes()
        {
            LoggerEntities.CurrentLogEngines.ForEach(entityType =>
            {
                cmbLoggerEngineType.Items.Add(entityType.LogEngineName);
                cmbLoggerEngineTest.Items.Add(entityType.LogEngineName);
            });

            LoggerEntities.CurrentLogTypes.ForEach(logType =>
            {
                cmbLogMessageTypeTest.Items.Add(logType);
            });
        }

        #endregion

        #region Assembly Manager

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLoggerEngine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isValid = DoValidations();
                if (!isValid)
                    return;

                var currentEngineSelected = cmbLoggerEngineType.SelectedValue.ToString();
                var assemblyNameEntered = txtAssemblyName.Text.Trim();
                var domainNameEntered = txtDomainName.Text.Trim();
                var assemblyLoaded = true;

                var wasAssemblyAlreadyLoaded = LoggerController.IsAlreadyLoaded(currentEngineSelected);

                if (wasAssemblyAlreadyLoaded)
                    MessageBox.Show(string.Format("The Assembly {0} was already Loaded.", assemblyNameEntered));
                else
                {
                    assemblyLoaded = LoggerController.LoadAssembly(currentEngineSelected, assemblyNameEntered, domainNameEntered);

                    if (assemblyLoaded)
                        MessageBox.Show(string.Format("The Assembly {0} has been successfully Loaded.", assemblyNameEntered));
                    else
                        MessageBox.Show(string.Format("The Assembly {0} was not Loaded. Make sure the values entered are correct.", assemblyNameEntered));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to load the Assembly: {0}", ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveLoggerEngine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isValid = DoValidations();
                if (!isValid)
                    return;

                var currentEngineSelected = cmbLoggerEngineType.SelectedValue.ToString();
                var assemblyNameEntered = txtAssemblyName.Text.Trim();
                var domainNameEntered = txtDomainName.Text.Trim();
                var assemblyUnloaded = true;

                var wasAssemblyAlreadyLoaded = LoggerController.IsAlreadyLoaded(currentEngineSelected);
                if (wasAssemblyAlreadyLoaded)
                {
                    assemblyUnloaded = LoggerController.UnloadAssembly(currentEngineSelected, assemblyNameEntered, domainNameEntered);
                }
                else
                {
                    MessageBox.Show(string.Format("There is no Assembly {0} to Unload.", assemblyNameEntered));
                    return;
                }

                if (assemblyUnloaded)
                    MessageBox.Show(string.Format("The Assembly {0} has been successfully Unloaded.", assemblyNameEntered));
                else
                    MessageBox.Show(string.Format("The Assembly {0} was not Unloaded.", assemblyNameEntered));

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to unload the Assembly: {0}", ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisplayAssemblies_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentAssemblies = LoggerController.GetCurrentAssemblies();
                var messageWithAssemblies = string.Empty;

                currentAssemblies.ForEach(assembly =>
                {
                    messageWithAssemblies = messageWithAssemblies + assembly;
                    messageWithAssemblies += Environment.NewLine;
                });

                if (!string.IsNullOrEmpty(messageWithAssemblies))
                    MessageBox.Show(string.Format("{0}", messageWithAssemblies), "Current Logger Engines Loaded");
                else
                    MessageBox.Show(string.Format("There are no loaded Assemblies."));
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to display the Assemblies: {0}", ex.Message));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool DoValidations()
        {
            var isValid = true;

            var currentEngineSelected = cmbLoggerEngineType.SelectedValue;
            if (currentEngineSelected == null)
            {
                MessageBox.Show(string.Format("Please, select a Logger Engine"));
                isValid = false;
            }

            var assemblyNameEntered = txtAssemblyName.Text.Trim();
            if (string.IsNullOrEmpty(assemblyNameEntered))
            {
                MessageBox.Show(string.Format("Please, enter an Assembly name"));
                isValid = false;
            }

            var domainNameEntered = txtDomainName.Text.Trim();
            if (string.IsNullOrEmpty(domainNameEntered))
            {
                MessageBox.Show(string.Format("Please, enter a Domain name"));
                isValid = false;
            }
            return isValid;
        }

        #endregion

        #region Logger Engine Tests

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var isValid = DoLogValidations();
                if (!isValid)
                    return;

                var logMessage = txtLogMessage.Text.Trim();
                var logEngineType = cmbLoggerEngineTest.SelectedValue.ToString();
                var logMessageType = cmbLogMessageTypeTest.SelectedValue.ToString();
                var log = new LoggerEntities.Log(logMessageType, logMessage);
                var methodToInvoke = AppConstant.LogMethod.DoLog;

                //Processing the Log regarding the LogEngine selected
                var wasAssemblyAlreadyLoaded = LoggerController.IsAlreadyLoaded(logEngineType);
                if (wasAssemblyAlreadyLoaded)
                    LoggerController.ProcessMethod(logEngineType, methodToInvoke, log);
                else
                {
                    MessageBox.Show(string.Format("The Assembly {0} is not loaded.", logEngineType));
                    return;
                }

                //Notifiying if the Log was successfully processed
                if (log.LogStatus.Equals(AppConstant.LogStatus.Saved))
                    MessageBox.Show(string.Format("The Log was successfully Saved."), "Results");
                else
                    MessageBox.Show(string.Format("The Log was not Saved."), "Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Unable to Log: {0}", ex.Message));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool DoLogValidations()
        {
            var isValid = true;

            var engineSelected = cmbLoggerEngineTest.SelectedValue;
            if (engineSelected == null)
            {
                MessageBox.Show(string.Format("Please, select a Logger Engine for Log"));
                isValid = false;
            }

            var logTypeSelected = cmbLogMessageTypeTest.SelectedValue;
            if (logTypeSelected == null)
            {
                MessageBox.Show(string.Format("Please, select a Type of Log"));
                isValid = false;
            }

            var logMessage = txtLogMessage.Text.Trim();
            if (string.IsNullOrEmpty(logMessage))
            {
                MessageBox.Show(string.Format("Please, enter a Message for Log"));
                isValid = false;
            }
            return isValid;
        }
        #endregion

    }
}
