namespace Tools.SQLProfilerReportHelper
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Drawing;
	using System.Linq;
	using System.Text;
	using System.Windows.Forms;
    using System.IO;

	public partial class MainForm : Form
    {
        class ConnectionParam
        {
            public string SqlServer { get; set; }
            public string Database { get; set; }
            public string ConnectionString { get { return string.Format("{0}\t{1}", SqlServer.Trim(), Database.Trim()); } }
            public void Parse(string connectionString)
            {
                char[] splitters = { '\t', ' ' };
                string[] connectionStringParts = connectionString.Split(splitters, 2, StringSplitOptions.RemoveEmptyEntries);
                SqlServer = Database = string.Empty;
                if(connectionStringParts.Length >= 1)
                {
                    SqlServer = connectionStringParts[0];
                }
                if (connectionStringParts.Length >= 2)
                {
                    Database = connectionStringParts[1];
                }
            }
        }

        Helper TableUtil { get; set; }
        string SettingsFolderName { get { return "SQLProfilerReportHelper"; } }
        string SettingsFileName { get { return "resentConnectionParams.txt"; } }
        List<ConnectionParam> ConnectionParameters { get; set; }

        public MainForm()
        {
            InitializeComponent();
			TableUtil = new Helper();
            backgroundWorkerPrepareTabele.WorkerReportsProgress = true;
            backgroundWorkerPrepareTabele.WorkerSupportsCancellation = true;
            loadConnectionParamSettings();
            comboBoxSQLServer.AutoCompleteCustomSource.Clear();
            comboBoxSQLServer.Items.Clear();
            comboBoxDB.AutoCompleteCustomSource.Clear();
            comboBoxDB.Items.Clear();

            foreach(ConnectionParam option in ConnectionParameters)
            {
                comboBoxSQLServer.AutoCompleteCustomSource.Add(option.SqlServer);
                comboBoxSQLServer.Items.Add(option.SqlServer);

                comboBoxDB.AutoCompleteCustomSource.Add(option.Database);
                comboBoxDB.Items.Add(option.Database);
            }

        }

        private void saveConnectionParamSettings(ConnectionParam connection)
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string settingsFolderPath = Path.Combine(appDataPath, SettingsFolderName);
            if(!Directory.Exists(settingsFolderPath))
            {
                Directory.CreateDirectory(settingsFolderPath);
            }
            string settingsFilePath = Path.Combine(settingsFolderPath, SettingsFileName);
            if(!File.Exists(settingsFilePath))
            {
                StreamWriter writer = File.CreateText(settingsFilePath);
                writer.Close();
            }
            string[] settings = File.ReadAllLines(settingsFilePath);
            if(!settings.Contains(connection.ConnectionString, StringComparer.InvariantCultureIgnoreCase))
            {
                File.AppendAllText(settingsFilePath, connection.ConnectionString);
            }
        }

        private void loadConnectionParamSettings()
        {
            ConnectionParameters = new List<ConnectionParam>();
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string settingsFolderPath = Path.Combine(appDataPath, SettingsFolderName);
            string settingsFilePath = Path.Combine(settingsFolderPath, SettingsFileName);
            if (File.Exists(settingsFilePath))
            {
                string[] settings = File.ReadAllLines(settingsFilePath);
                foreach (string connectionParam in settings)
                {
                    ConnectionParam param = new ConnectionParam();
                    param.Parse(connectionParam);
                    ConnectionParameters.Add(param);
                }
            }

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            var sqlServer = this.comboBoxSQLServer.Text;
            var dataBase = this.comboBoxDB.Text;

            try
            {
                TableUtil.Connect(sqlServer, dataBase);
                this.comboBoxTable.Items.Clear();
                this.comboBoxTable.Items.AddRange(TableUtil.Tables);
                ConnectionParam options = new ConnectionParam()
                {
                    SqlServer = sqlServer,
                    Database = dataBase,
                };
                saveConnectionParamSettings(options);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageBox.Show(
                    string.Format("Не удалось соединиться с базой данных {0} на сервере {1} и получить список таблиц.\n{2}"
                        , dataBase
                        , sqlServer
                        , ex.Message
                    )
                    , "Ошибка соединения" );
            }
        }

        private void buttonTextKeyCheck_Click(object sender, EventArgs e)
        {
            var tableName = this.comboBoxTable.Text;
            this.checkBoxTextKeyStatus.Checked = TableUtil.ColumnExistInTable(tableName, "TextKey");
            this.buttonTextKeyCreate.Enabled = !this.checkBoxTextKeyStatus.Checked;
            this.buttonStartSP.Enabled = this.checkBoxTextKeyStatus.Checked;
            this.buttonDetailReportCheck.Enabled = this.checkBoxTextKeyStatus.Checked;
            this.buttonDraftReportCheck.Enabled = this.checkBoxTextKeyStatus.Checked;
            this.buttonErrorStatCheck.Enabled = this.checkBoxTextKeyStatus.Checked;

            this.textBoxRowCount.Text = TableUtil.RowCountForPrepare.ToString();
            this.textBoxPreparedRowCount.Text = TableUtil.RowCountPrepared.ToString();

        }

        private void comboBoxTable_TextChanged(object sender, EventArgs e)
        {
            TableUtil.TableName = this.comboBoxTable.Text;
            this.buttonTextKeyCheck.Enabled = true;
            this.buttonDeadlockReportCheck.Enabled = true;
			this.buttonMinuteAndSecondCheck.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPrepareTabele.IsBusy != true)
            {
                TableUtil.RowCountForPrepare = TableUtil.GetRowCountForPrepare();
                TableUtil.RowCountPrepared = 0;

                this.textBoxRowCount.Text = TableUtil.RowCountForPrepare.ToString();
                this.textBoxPreparedRowCount.Text = TableUtil.RowCountPrepared.ToString();
                TableUtil.StartTime = System.DateTime.Now;
                this.textBoxStartTime.Text = TableUtil.StartTime.ToString();
                this.textBoxStopTime.Text = TableUtil.ExpectedStopTime.ToString();

                backgroundWorkerPrepareTabele.RunWorkerAsync();

                this.buttonStart.Enabled = false;
                this.buttonStop.Enabled = true;

				this.buttonStartSP.Enabled = false;
				this.buttonStopSP.Enabled = false;
			}
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            if (backgroundWorkerPrepareTabele.WorkerSupportsCancellation == true)
            {
                backgroundWorkerPrepareTabele.CancelAsync();

                this.buttonStop.Enabled = false;
                this.buttonStart.Enabled = true;

				this.buttonStopSP.Enabled = false;
				this.buttonStartSP.Enabled = true;
			}
        }

        private void backgroundWorkerPrepareTabele_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
			try
			{
				TableUtil.DropIndexOnTextKeys();
			}
			catch (Exception ex)
			{ }

			while (TableUtil.PreparedIsComplete == false)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    TableUtil.PrepareTextKeys();
                    worker.ReportProgress(100 * TableUtil.RowCountPrepared / TableUtil.RowCountForPrepare);
                }
            }
			if (TableUtil.PreparedIsComplete)
			{
				worker.ReportProgress(100);
			}
        }

        private void backgroundWorkerPrepareTabele_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.textBoxPreparedRowCount.Text = TableUtil.RowCountPrepared.ToString();
            this.textBoxPreparedRowProgress.Text = string.Format("{0} %", e.ProgressPercentage.ToString());
            this.textBoxStopTime.Text = TableUtil.ExpectedStopTime.ToString();
        }

        private void buttonDraftReportCheck_Click(object sender, EventArgs e)
        {
            var isExist = TableUtil.TableExist(TableUtil.TableNameDraft);
            this.checkBoxDraftReportStatus.Checked = isExist;
            this.buttonDraftReportCreate.Enabled = !isExist;
        }

        private void buttonTextKeyCreate_Click(object sender, EventArgs e)
        {
            //TableUtil.CreateIndexes();
            TableUtil.CreateTextKey();
            buttonTextKeyCheck_Click(sender, e);
        }

        private void buttonDetailReportCheck_Click(object sender, EventArgs e)
        {
            var tableExist = TableUtil.TableExist(TableUtil.TableNameDetail);
            this.checkBoxDetailReportStatus.Checked = tableExist;
            this.buttonDetailReportCreate.Enabled = !this.checkBoxDetailReportStatus.Checked;
        }

        private void buttonDetailReportCreate_Click(object sender, EventArgs e)
        {
            TableUtil.CreateDetailReport();
            this.checkBoxDetailReportStatus.Checked = true;
            this.buttonDetailReportCreate.Enabled = false;
        }

        private void buttonDraftReportCreate_Click(object sender, EventArgs e)
        {
            TableUtil.CreateDraftReport();
            this.checkBoxDraftReportStatus.Checked = true;
            this.buttonDraftReportCreate.Enabled = false;
        }

        private void buttonErrorStatCheck_Click(object sender, EventArgs e)
        {
            var tableExist = TableUtil.TableExist(TableUtil.TableNameError);
            this.checkBoxErrorReportStatus.Checked = tableExist;
            this.buttonErrorReportCreate.Enabled = !tableExist;
        }

        private void buttonErrorReportCreate_Click(object sender, EventArgs e)
        {
            TableUtil.CreateErrorReport();
            this.checkBoxErrorReportStatus.Checked = true;
            this.buttonErrorReportCreate.Enabled = false;
        }

        private void buttonDeadlockReportCheck_Click(object sender, EventArgs e)
        {
            var tableExist = TableUtil.TableExist(TableUtil.TableNameDeadlock);
            this.checkBoxDeadlockReportStatus.Checked = tableExist;
            this.buttonDeadlockReportCreate.Enabled = !tableExist;
        }

        private void buttonDeadlockReportCreate_Click(object sender, EventArgs e)
        {
            TableUtil.CreateDeadlockReport();
            this.checkBoxDeadlockReportStatus.Checked = true;
            this.buttonDeadlockReportCreate.Enabled = false;
        }

		private void buttonMinuteAndSecondCheck_Click(object sender, EventArgs e)
		{
			var columnsExist = TableUtil.ColumnExistInTable(TableUtil.TableName, "Second01");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Second05");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Second10");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Munute01");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Munute02");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Munute03");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Munute04");
			columnsExist = columnsExist && TableUtil.ColumnExistInTable(TableUtil.TableName, "Munute05");

			this.checkBoxMinuteAndSecondStatus.Checked = columnsExist;
			this.buttonMinuteAndSecondCreate.Enabled = !columnsExist;
		}

		private void buttonMinuteAndSecondCreate_Click(object sender, EventArgs e)
		{
			TableUtil.CreateMinuteAndSecondColumn();
			TableUtil.FillMinuteAndSecondColumn();
			buttonMinuteAndSecondCheck_Click(sender, e);
		}

		private void DisableAllButtons()
		{
			this.buttonMinuteAndSecondCheck.Enabled = false;
			this.buttonConnect.Enabled = false;
			this.buttonDeadlockReportCheck.Enabled = false;
			this.buttonDeadlockReportCreate.Enabled = false;
			this.buttonDetailReportCheck.Enabled = false;
			this.buttonDetailReportCreate.Enabled = false;
			this.buttonDraftReportCheck.Enabled = false;
			this.buttonDraftReportCreate.Enabled = false;
			this.buttonErrorReportCreate.Enabled = false;
			this.buttonErrorStatCheck.Enabled = false;
			this.buttonMinuteAndSecondCheck.Enabled = false;
			this.buttonMinuteAndSecondCreate.Enabled = false;
			this.buttonStart.Enabled = false;
			this.buttonStop.Enabled = false;
			this.buttonTextKeyCheck.Enabled = false;
			this.buttonTextKeyCreate.Enabled = false;
		}

		private void CheckEnableAllButtons(object sender, EventArgs e)
		{
			this.buttonDeadlockReportCheck.Enabled = true;
			buttonDeadlockReportCheck_Click(sender, e);

			this.buttonMinuteAndSecondCheck.Enabled = true;
			buttonMinuteAndSecondCheck_Click(sender, e);
		}

		private void backgroundWorkerPrepareTabele_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.buttonStart.Enabled = true;
			this.buttonStartSP.Enabled = true;

			this.buttonStop.Enabled = false;
			this.buttonStopSP.Enabled = false;


			try
			{
				TableUtil.CreateIndexOnTextKeys();
			}
			catch (Exception ex)
			{
			}
		}

		private void backgroundWorkerPrepareTableSP_DoWork(object sender, DoWorkEventArgs e)
		{
			BackgroundWorker worker = sender as BackgroundWorker;
			try
			{
				TableUtil.DropIndexOnTextKeys();
			}
			catch (Exception ex)
			{ }

			while (!TableUtil.PreparedIsCompleteSP)
			{
				if (worker.CancellationPending)
				{
					e.Cancel = true;
					break;
				}
				else
				{
					// Perform a time consuming operation and report progress.
					TableUtil.PrepareTextKeysSP();
					worker.ReportProgress(100 * TableUtil.RowCountPreparedSP / TableUtil.RowCountForPrepareSP);
				}
			}
			if (TableUtil.PreparedIsCompleteSP)
			{
				worker.ReportProgress(100);
			}
		}

		private void buttonStartSP_Click(object sender, EventArgs e)
		{
			if (backgroundWorkerPrepareTableSP.IsBusy != true)
			{
				TableUtil.RowCountForPrepareSP = TableUtil.GetRowCountForPrepareSP();
				TableUtil.RowCountPreparedSP = 0;

				this.textBoxRowCount.Text = TableUtil.RowCountForPrepareSP.ToString();
				this.textBoxPreparedRowCount.Text = TableUtil.RowCountPreparedSP.ToString();
				TableUtil.StartTime = System.DateTime.Now;
				this.textBoxStartTime.Text = TableUtil.StartTime.ToString();
				this.textBoxStopTime.Text = TableUtil.ExpectedStopTimeSP.ToString();

				backgroundWorkerPrepareTableSP.RunWorkerAsync();

				this.buttonStartSP.Enabled = false;
				this.buttonStopSP.Enabled = true;

				this.buttonStart.Enabled = false;
				this.buttonStop.Enabled = false;
			}
		}

		private void buttonStopSP_Click(object sender, EventArgs e)
		{
			if (backgroundWorkerPrepareTableSP.WorkerSupportsCancellation == true)
			{
				backgroundWorkerPrepareTableSP.CancelAsync();

				this.buttonStop.Enabled = false;
				this.buttonStart.Enabled = true;

				this.buttonStopSP.Enabled = false;
				this.buttonStartSP.Enabled = true;
			}

		}

		private void backgroundWorkerPrepareTableSP_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.textBoxPreparedRowCount.Text = TableUtil.RowCountPreparedSP.ToString();
			this.textBoxPreparedRowProgress.Text = string.Format("{0} %", e.ProgressPercentage.ToString());
			this.textBoxStopTime.Text = TableUtil.ExpectedStopTimeSP.ToString();
		}

		private void backgroundWorkerPrepareTableSP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.buttonStart.Enabled = true;
			this.buttonStartSP.Enabled = true;

			this.buttonStop.Enabled = false;
			this.buttonStopSP.Enabled = false;

		}

		private void buttonDetailReportView_Click(object sender, EventArgs e)
		{
			ReportViewForm reportView = new ReportViewForm(TableUtil);
			reportView.LoadDetailStat(
				TableUtil.GetDetailStat()
				);
			reportView.Show();
        }

        private void buttonCheckFunction_Click(object sender, EventArgs e)
        {
            bool functionExist = TableUtil.FunctionExists("PrepareTextData4");
            this.checkBoxFunctionExist.Checked = functionExist;
            this.buttonCreateFunction.Enabled = !functionExist;
        }

        private void buttonCreateFunction_Click(object sender, EventArgs e)
        {
            TableUtil.CreateFunctionPrepareTextData();
            this.checkBoxFunctionExist.Checked = true;
            this.buttonCreateFunction.Enabled = false;
        }
    }
}
