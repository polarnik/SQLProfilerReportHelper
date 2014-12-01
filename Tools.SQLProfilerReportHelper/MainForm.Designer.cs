namespace Tools.SQLProfilerReportHelper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorkerPrepareTabele = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerPrepareTableSP = new System.ComponentModel.BackgroundWorker();
            this.groupBoxConnect = new System.Windows.Forms.GroupBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxDB = new System.Windows.Forms.ComboBox();
            this.comboBoxSQLServer = new System.Windows.Forms.ComboBox();
            this.labelDB = new System.Windows.Forms.Label();
            this.labelSQLServer = new System.Windows.Forms.Label();
            this.buttonCheckFunction = new System.Windows.Forms.Button();
            this.groupBoxFunction = new System.Windows.Forms.GroupBox();
            this.buttonCreateFunction = new System.Windows.Forms.Button();
            this.checkBoxFunctionExist = new System.Windows.Forms.CheckBox();
            this.labelFunctionStatus = new System.Windows.Forms.Label();
            this.groupBoxTable = new System.Windows.Forms.GroupBox();
            this.buttonCheckTable = new System.Windows.Forms.Button();
            this.comboBoxTable = new System.Windows.Forms.ComboBox();
            this.labelTable = new System.Windows.Forms.Label();
            this.groupBoxPrepare = new System.Windows.Forms.GroupBox();
            this.buttonStopSP = new System.Windows.Forms.Button();
            this.buttonStartSP = new System.Windows.Forms.Button();
            this.buttonTextKeyCreate = new System.Windows.Forms.Button();
            this.buttonTextKeyCheck = new System.Windows.Forms.Button();
            this.checkBoxTextKeyStatus = new System.Windows.Forms.CheckBox();
            this.labelTextKeyStatus = new System.Windows.Forms.Label();
            this.textBoxStopTime = new System.Windows.Forms.TextBox();
            this.textBoxPreparedRowProgress = new System.Windows.Forms.TextBox();
            this.textBoxPreparedRowCount = new System.Windows.Forms.TextBox();
            this.textBoxStartTime = new System.Windows.Forms.TextBox();
            this.textBoxRowCount = new System.Windows.Forms.TextBox();
            this.labelPreparedRowProgress = new System.Windows.Forms.Label();
            this.labelStopTime = new System.Windows.Forms.Label();
            this.labelPreparedRowCount = new System.Windows.Forms.Label();
            this.labelStartTime = new System.Windows.Forms.Label();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelRowCount = new System.Windows.Forms.Label();
            this.groupBoxReports = new System.Windows.Forms.GroupBox();
            this.buttonMinuteAndSecondCreate = new System.Windows.Forms.Button();
            this.buttonDeadlockReportCreate = new System.Windows.Forms.Button();
            this.buttonMinuteAndSecondCheck = new System.Windows.Forms.Button();
            this.checkBoxMinuteAndSecondStatus = new System.Windows.Forms.CheckBox();
            this.buttonDetailReportView = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonDeadlockReportCheck = new System.Windows.Forms.Button();
            this.checkBoxDeadlockReportStatus = new System.Windows.Forms.CheckBox();
            this.buttonDetailReportCreate = new System.Windows.Forms.Button();
            this.labelDeadlockReportStatus = new System.Windows.Forms.Label();
            this.buttonDraftReportCreate = new System.Windows.Forms.Button();
            this.buttonDetailReportCheck = new System.Windows.Forms.Button();
            this.buttonErrorReportCreate = new System.Windows.Forms.Button();
            this.checkBoxDetailReportStatus = new System.Windows.Forms.CheckBox();
            this.buttonDraftReportCheck = new System.Windows.Forms.Button();
            this.labelDetailReportStatus = new System.Windows.Forms.Label();
            this.checkBoxDraftReportStatus = new System.Windows.Forms.CheckBox();
            this.buttonErrorStatCheck = new System.Windows.Forms.Button();
            this.labelDraftReportStatus = new System.Windows.Forms.Label();
            this.checkBoxErrorReportStatus = new System.Windows.Forms.CheckBox();
            this.labelErrorReportStatus = new System.Windows.Forms.Label();
            this.panelConsole = new System.Windows.Forms.Panel();
            this.groupBoxConnect.SuspendLayout();
            this.groupBoxFunction.SuspendLayout();
            this.groupBoxTable.SuspendLayout();
            this.groupBoxPrepare.SuspendLayout();
            this.groupBoxReports.SuspendLayout();
            this.panelConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorkerPrepareTabele
            // 
            this.backgroundWorkerPrepareTabele.WorkerReportsProgress = true;
            this.backgroundWorkerPrepareTabele.WorkerSupportsCancellation = true;
            this.backgroundWorkerPrepareTabele.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPrepareTabele_DoWork);
            this.backgroundWorkerPrepareTabele.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerPrepareTabele_ProgressChanged);
            this.backgroundWorkerPrepareTabele.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPrepareTabele_RunWorkerCompleted);
            // 
            // backgroundWorkerPrepareTableSP
            // 
            this.backgroundWorkerPrepareTableSP.WorkerReportsProgress = true;
            this.backgroundWorkerPrepareTableSP.WorkerSupportsCancellation = true;
            this.backgroundWorkerPrepareTableSP.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerPrepareTableSP_DoWork);
            this.backgroundWorkerPrepareTableSP.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerPrepareTableSP_ProgressChanged);
            this.backgroundWorkerPrepareTableSP.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerPrepareTableSP_RunWorkerCompleted);
            // 
            // groupBoxConnect
            // 
            this.groupBoxConnect.Controls.Add(this.buttonConnect);
            this.groupBoxConnect.Controls.Add(this.comboBoxDB);
            this.groupBoxConnect.Controls.Add(this.comboBoxSQLServer);
            this.groupBoxConnect.Controls.Add(this.labelDB);
            this.groupBoxConnect.Controls.Add(this.labelSQLServer);
            this.groupBoxConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxConnect.Location = new System.Drawing.Point(0, 0);
            this.groupBoxConnect.Name = "groupBoxConnect";
            this.groupBoxConnect.Size = new System.Drawing.Size(502, 75);
            this.groupBoxConnect.TabIndex = 1;
            this.groupBoxConnect.TabStop = false;
            this.groupBoxConnect.Text = "Connect";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonConnect.Location = new System.Drawing.Point(393, 44);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(100, 23);
            this.buttonConnect.TabIndex = 27;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxDB
            // 
            this.comboBoxDB.FormattingEnabled = true;
            this.comboBoxDB.Location = new System.Drawing.Point(133, 46);
            this.comboBoxDB.Name = "comboBoxDB";
            this.comboBoxDB.Size = new System.Drawing.Size(253, 21);
            this.comboBoxDB.TabIndex = 26;
            // 
            // comboBoxSQLServer
            // 
            this.comboBoxSQLServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxSQLServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxSQLServer.FormattingEnabled = true;
            this.comboBoxSQLServer.Items.AddRange(new object[] {
            "orpkasnpo\\sql2008",
            "OTLOADSQL"});
            this.comboBoxSQLServer.Location = new System.Drawing.Point(133, 19);
            this.comboBoxSQLServer.Name = "comboBoxSQLServer";
            this.comboBoxSQLServer.Size = new System.Drawing.Size(253, 21);
            this.comboBoxSQLServer.Sorted = true;
            this.comboBoxSQLServer.TabIndex = 25;
            // 
            // labelDB
            // 
            this.labelDB.AutoSize = true;
            this.labelDB.Location = new System.Drawing.Point(7, 49);
            this.labelDB.Name = "labelDB";
            this.labelDB.Size = new System.Drawing.Size(56, 13);
            this.labelDB.TabIndex = 24;
            this.labelDB.Text = "Database:";
            // 
            // labelSQLServer
            // 
            this.labelSQLServer.AutoSize = true;
            this.labelSQLServer.Location = new System.Drawing.Point(7, 22);
            this.labelSQLServer.Name = "labelSQLServer";
            this.labelSQLServer.Size = new System.Drawing.Size(65, 13);
            this.labelSQLServer.TabIndex = 23;
            this.labelSQLServer.Text = "SQL Server:";
            // 
            // buttonCheckFunction
            // 
            this.buttonCheckFunction.Location = new System.Drawing.Point(133, 16);
            this.buttonCheckFunction.Name = "buttonCheckFunction";
            this.buttonCheckFunction.Size = new System.Drawing.Size(74, 23);
            this.buttonCheckFunction.TabIndex = 28;
            this.buttonCheckFunction.Text = "Check";
            this.buttonCheckFunction.UseVisualStyleBackColor = true;
            this.buttonCheckFunction.Click += new System.EventHandler(this.buttonCheckFunction_Click);
            // 
            // groupBoxFunction
            // 
            this.groupBoxFunction.Controls.Add(this.buttonCheckFunction);
            this.groupBoxFunction.Controls.Add(this.buttonCreateFunction);
            this.groupBoxFunction.Controls.Add(this.checkBoxFunctionExist);
            this.groupBoxFunction.Controls.Add(this.labelFunctionStatus);
            this.groupBoxFunction.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxFunction.Location = new System.Drawing.Point(0, 75);
            this.groupBoxFunction.Name = "groupBoxFunction";
            this.groupBoxFunction.Size = new System.Drawing.Size(502, 45);
            this.groupBoxFunction.TabIndex = 42;
            this.groupBoxFunction.TabStop = false;
            this.groupBoxFunction.Text = "Function PrepareTextData";
            // 
            // buttonCreateFunction
            // 
            this.buttonCreateFunction.Enabled = false;
            this.buttonCreateFunction.Location = new System.Drawing.Point(312, 16);
            this.buttonCreateFunction.Name = "buttonCreateFunction";
            this.buttonCreateFunction.Size = new System.Drawing.Size(74, 23);
            this.buttonCreateFunction.TabIndex = 41;
            this.buttonCreateFunction.Text = "Create";
            this.buttonCreateFunction.UseVisualStyleBackColor = true;
            this.buttonCreateFunction.Click += new System.EventHandler(this.buttonCreateFunction_Click);
            // 
            // checkBoxFunctionExist
            // 
            this.checkBoxFunctionExist.AutoSize = true;
            this.checkBoxFunctionExist.Enabled = false;
            this.checkBoxFunctionExist.Location = new System.Drawing.Point(214, 20);
            this.checkBoxFunctionExist.Name = "checkBoxFunctionExist";
            this.checkBoxFunctionExist.Size = new System.Drawing.Size(91, 17);
            this.checkBoxFunctionExist.TabIndex = 40;
            this.checkBoxFunctionExist.Text = "Function exist";
            this.checkBoxFunctionExist.UseVisualStyleBackColor = true;
            // 
            // labelFunctionStatus
            // 
            this.labelFunctionStatus.AutoSize = true;
            this.labelFunctionStatus.Location = new System.Drawing.Point(6, 21);
            this.labelFunctionStatus.Name = "labelFunctionStatus";
            this.labelFunctionStatus.Size = new System.Drawing.Size(82, 13);
            this.labelFunctionStatus.TabIndex = 39;
            this.labelFunctionStatus.Text = "Function status:";
            // 
            // groupBoxTable
            // 
            this.groupBoxTable.Controls.Add(this.buttonCheckTable);
            this.groupBoxTable.Controls.Add(this.comboBoxTable);
            this.groupBoxTable.Controls.Add(this.labelTable);
            this.groupBoxTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxTable.Location = new System.Drawing.Point(0, 120);
            this.groupBoxTable.Name = "groupBoxTable";
            this.groupBoxTable.Size = new System.Drawing.Size(502, 45);
            this.groupBoxTable.TabIndex = 2;
            this.groupBoxTable.TabStop = false;
            this.groupBoxTable.Text = "Table";
            // 
            // buttonCheckTable
            // 
            this.buttonCheckTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheckTable.Location = new System.Drawing.Point(393, 14);
            this.buttonCheckTable.Name = "buttonCheckTable";
            this.buttonCheckTable.Size = new System.Drawing.Size(100, 23);
            this.buttonCheckTable.TabIndex = 29;
            this.buttonCheckTable.Text = "Check All";
            this.buttonCheckTable.UseVisualStyleBackColor = true;
            this.buttonCheckTable.Visible = false;
            // 
            // comboBoxTable
            // 
            this.comboBoxTable.FormattingEnabled = true;
            this.comboBoxTable.Location = new System.Drawing.Point(135, 16);
            this.comboBoxTable.Name = "comboBoxTable";
            this.comboBoxTable.Size = new System.Drawing.Size(251, 21);
            this.comboBoxTable.TabIndex = 27;
            this.comboBoxTable.TextChanged += new System.EventHandler(this.comboBoxTable_TextChanged);
            // 
            // labelTable
            // 
            this.labelTable.AutoSize = true;
            this.labelTable.Location = new System.Drawing.Point(9, 19);
            this.labelTable.Name = "labelTable";
            this.labelTable.Size = new System.Drawing.Size(77, 13);
            this.labelTable.TabIndex = 22;
            this.labelTable.Text = "Profiling Table:";
            // 
            // groupBoxPrepare
            // 
            this.groupBoxPrepare.Controls.Add(this.buttonStopSP);
            this.groupBoxPrepare.Controls.Add(this.buttonStartSP);
            this.groupBoxPrepare.Controls.Add(this.buttonTextKeyCreate);
            this.groupBoxPrepare.Controls.Add(this.buttonTextKeyCheck);
            this.groupBoxPrepare.Controls.Add(this.checkBoxTextKeyStatus);
            this.groupBoxPrepare.Controls.Add(this.labelTextKeyStatus);
            this.groupBoxPrepare.Controls.Add(this.textBoxStopTime);
            this.groupBoxPrepare.Controls.Add(this.textBoxPreparedRowProgress);
            this.groupBoxPrepare.Controls.Add(this.textBoxPreparedRowCount);
            this.groupBoxPrepare.Controls.Add(this.textBoxStartTime);
            this.groupBoxPrepare.Controls.Add(this.textBoxRowCount);
            this.groupBoxPrepare.Controls.Add(this.labelPreparedRowProgress);
            this.groupBoxPrepare.Controls.Add(this.labelStopTime);
            this.groupBoxPrepare.Controls.Add(this.labelPreparedRowCount);
            this.groupBoxPrepare.Controls.Add(this.labelStartTime);
            this.groupBoxPrepare.Controls.Add(this.buttonStop);
            this.groupBoxPrepare.Controls.Add(this.buttonStart);
            this.groupBoxPrepare.Controls.Add(this.labelRowCount);
            this.groupBoxPrepare.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxPrepare.Location = new System.Drawing.Point(0, 165);
            this.groupBoxPrepare.Name = "groupBoxPrepare";
            this.groupBoxPrepare.Size = new System.Drawing.Size(502, 182);
            this.groupBoxPrepare.TabIndex = 27;
            this.groupBoxPrepare.TabStop = false;
            this.groupBoxPrepare.Text = "Prepare for detail sql profiler report";
            // 
            // buttonStopSP
            // 
            this.buttonStopSP.Enabled = false;
            this.buttonStopSP.Location = new System.Drawing.Point(393, 70);
            this.buttonStopSP.Name = "buttonStopSP";
            this.buttonStopSP.Size = new System.Drawing.Size(100, 23);
            this.buttonStopSP.TabIndex = 36;
            this.buttonStopSP.Text = "Stop (SP)";
            this.buttonStopSP.UseVisualStyleBackColor = true;
            this.buttonStopSP.Click += new System.EventHandler(this.buttonStopSP_Click);
            // 
            // buttonStartSP
            // 
            this.buttonStartSP.Enabled = false;
            this.buttonStartSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStartSP.Location = new System.Drawing.Point(392, 44);
            this.buttonStartSP.Name = "buttonStartSP";
            this.buttonStartSP.Size = new System.Drawing.Size(100, 23);
            this.buttonStartSP.TabIndex = 35;
            this.buttonStartSP.Text = "Start:SP";
            this.buttonStartSP.UseVisualStyleBackColor = true;
            this.buttonStartSP.Click += new System.EventHandler(this.buttonStartSP_Click);
            // 
            // buttonTextKeyCreate
            // 
            this.buttonTextKeyCreate.Enabled = false;
            this.buttonTextKeyCreate.Location = new System.Drawing.Point(312, 18);
            this.buttonTextKeyCreate.Name = "buttonTextKeyCreate";
            this.buttonTextKeyCreate.Size = new System.Drawing.Size(74, 23);
            this.buttonTextKeyCreate.TabIndex = 34;
            this.buttonTextKeyCreate.Text = "Create";
            this.buttonTextKeyCreate.UseVisualStyleBackColor = true;
            this.buttonTextKeyCreate.Click += new System.EventHandler(this.buttonTextKeyCreate_Click);
            // 
            // buttonTextKeyCheck
            // 
            this.buttonTextKeyCheck.Enabled = false;
            this.buttonTextKeyCheck.Location = new System.Drawing.Point(133, 18);
            this.buttonTextKeyCheck.Name = "buttonTextKeyCheck";
            this.buttonTextKeyCheck.Size = new System.Drawing.Size(74, 23);
            this.buttonTextKeyCheck.TabIndex = 33;
            this.buttonTextKeyCheck.Text = "Check";
            this.buttonTextKeyCheck.UseVisualStyleBackColor = true;
            this.buttonTextKeyCheck.Click += new System.EventHandler(this.buttonTextKeyCheck_Click);
            // 
            // checkBoxTextKeyStatus
            // 
            this.checkBoxTextKeyStatus.AutoSize = true;
            this.checkBoxTextKeyStatus.Enabled = false;
            this.checkBoxTextKeyStatus.Location = new System.Drawing.Point(214, 22);
            this.checkBoxTextKeyStatus.Name = "checkBoxTextKeyStatus";
            this.checkBoxTextKeyStatus.Size = new System.Drawing.Size(89, 17);
            this.checkBoxTextKeyStatus.TabIndex = 32;
            this.checkBoxTextKeyStatus.Text = "TextKey exist";
            this.checkBoxTextKeyStatus.UseVisualStyleBackColor = true;
            // 
            // labelTextKeyStatus
            // 
            this.labelTextKeyStatus.AutoSize = true;
            this.labelTextKeyStatus.Location = new System.Drawing.Point(7, 23);
            this.labelTextKeyStatus.Name = "labelTextKeyStatus";
            this.labelTextKeyStatus.Size = new System.Drawing.Size(113, 13);
            this.labelTextKeyStatus.TabIndex = 31;
            this.labelTextKeyStatus.Text = "TextKey create status:";
            // 
            // textBoxStopTime
            // 
            this.textBoxStopTime.Location = new System.Drawing.Point(135, 148);
            this.textBoxStopTime.Name = "textBoxStopTime";
            this.textBoxStopTime.Size = new System.Drawing.Size(251, 20);
            this.textBoxStopTime.TabIndex = 30;
            // 
            // textBoxPreparedRowProgress
            // 
            this.textBoxPreparedRowProgress.Location = new System.Drawing.Point(135, 122);
            this.textBoxPreparedRowProgress.Name = "textBoxPreparedRowProgress";
            this.textBoxPreparedRowProgress.Size = new System.Drawing.Size(251, 20);
            this.textBoxPreparedRowProgress.TabIndex = 29;
            // 
            // textBoxPreparedRowCount
            // 
            this.textBoxPreparedRowCount.Location = new System.Drawing.Point(135, 96);
            this.textBoxPreparedRowCount.Name = "textBoxPreparedRowCount";
            this.textBoxPreparedRowCount.Size = new System.Drawing.Size(251, 20);
            this.textBoxPreparedRowCount.TabIndex = 28;
            // 
            // textBoxStartTime
            // 
            this.textBoxStartTime.Location = new System.Drawing.Point(135, 70);
            this.textBoxStartTime.Name = "textBoxStartTime";
            this.textBoxStartTime.Size = new System.Drawing.Size(251, 20);
            this.textBoxStartTime.TabIndex = 27;
            // 
            // textBoxRowCount
            // 
            this.textBoxRowCount.Location = new System.Drawing.Point(135, 44);
            this.textBoxRowCount.Name = "textBoxRowCount";
            this.textBoxRowCount.Size = new System.Drawing.Size(251, 20);
            this.textBoxRowCount.TabIndex = 26;
            // 
            // labelPreparedRowProgress
            // 
            this.labelPreparedRowProgress.AutoSize = true;
            this.labelPreparedRowProgress.Location = new System.Drawing.Point(15, 125);
            this.labelPreparedRowProgress.Name = "labelPreparedRowProgress";
            this.labelPreparedRowProgress.Size = new System.Drawing.Size(116, 13);
            this.labelPreparedRowProgress.TabIndex = 25;
            this.labelPreparedRowProgress.Text = "Prepared row progress:";
            // 
            // labelStopTime
            // 
            this.labelStopTime.AutoSize = true;
            this.labelStopTime.Location = new System.Drawing.Point(15, 151);
            this.labelStopTime.Name = "labelStopTime";
            this.labelStopTime.Size = new System.Drawing.Size(54, 13);
            this.labelStopTime.TabIndex = 24;
            this.labelStopTime.Text = "Stop time:";
            // 
            // labelPreparedRowCount
            // 
            this.labelPreparedRowCount.AutoSize = true;
            this.labelPreparedRowCount.Location = new System.Drawing.Point(15, 99);
            this.labelPreparedRowCount.Name = "labelPreparedRowCount";
            this.labelPreparedRowCount.Size = new System.Drawing.Size(103, 13);
            this.labelPreparedRowCount.TabIndex = 23;
            this.labelPreparedRowCount.Text = "Prepared row count:";
            // 
            // labelStartTime
            // 
            this.labelStartTime.AutoSize = true;
            this.labelStartTime.Location = new System.Drawing.Point(15, 73);
            this.labelStartTime.Name = "labelStartTime";
            this.labelStartTime.Size = new System.Drawing.Size(54, 13);
            this.labelStartTime.TabIndex = 22;
            this.labelStartTime.Text = "Start time:";
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(392, 148);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(100, 23);
            this.buttonStop.TabIndex = 21;
            this.buttonStop.Text = "Stop (SQL)";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Enabled = false;
            this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonStart.Location = new System.Drawing.Point(392, 122);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(100, 23);
            this.buttonStart.TabIndex = 20;
            this.buttonStart.Text = "Start:SQL";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelRowCount
            // 
            this.labelRowCount.AutoSize = true;
            this.labelRowCount.Location = new System.Drawing.Point(15, 47);
            this.labelRowCount.Name = "labelRowCount";
            this.labelRowCount.Size = new System.Drawing.Size(62, 13);
            this.labelRowCount.TabIndex = 19;
            this.labelRowCount.Text = "Row count:";
            // 
            // groupBoxReports
            // 
            this.groupBoxReports.Controls.Add(this.buttonMinuteAndSecondCreate);
            this.groupBoxReports.Controls.Add(this.buttonDeadlockReportCreate);
            this.groupBoxReports.Controls.Add(this.buttonMinuteAndSecondCheck);
            this.groupBoxReports.Controls.Add(this.checkBoxMinuteAndSecondStatus);
            this.groupBoxReports.Controls.Add(this.buttonDetailReportView);
            this.groupBoxReports.Controls.Add(this.label2);
            this.groupBoxReports.Controls.Add(this.buttonDeadlockReportCheck);
            this.groupBoxReports.Controls.Add(this.checkBoxDeadlockReportStatus);
            this.groupBoxReports.Controls.Add(this.buttonDetailReportCreate);
            this.groupBoxReports.Controls.Add(this.labelDeadlockReportStatus);
            this.groupBoxReports.Controls.Add(this.buttonDraftReportCreate);
            this.groupBoxReports.Controls.Add(this.buttonDetailReportCheck);
            this.groupBoxReports.Controls.Add(this.buttonErrorReportCreate);
            this.groupBoxReports.Controls.Add(this.checkBoxDetailReportStatus);
            this.groupBoxReports.Controls.Add(this.buttonDraftReportCheck);
            this.groupBoxReports.Controls.Add(this.labelDetailReportStatus);
            this.groupBoxReports.Controls.Add(this.checkBoxDraftReportStatus);
            this.groupBoxReports.Controls.Add(this.buttonErrorStatCheck);
            this.groupBoxReports.Controls.Add(this.labelDraftReportStatus);
            this.groupBoxReports.Controls.Add(this.checkBoxErrorReportStatus);
            this.groupBoxReports.Controls.Add(this.labelErrorReportStatus);
            this.groupBoxReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBoxReports.Location = new System.Drawing.Point(0, 347);
            this.groupBoxReports.Name = "groupBoxReports";
            this.groupBoxReports.Size = new System.Drawing.Size(502, 160);
            this.groupBoxReports.TabIndex = 4;
            this.groupBoxReports.TabStop = false;
            this.groupBoxReports.Text = "Reports";
            // 
            // buttonMinuteAndSecondCreate
            // 
            this.buttonMinuteAndSecondCreate.Enabled = false;
            this.buttonMinuteAndSecondCreate.Location = new System.Drawing.Point(312, 129);
            this.buttonMinuteAndSecondCreate.Name = "buttonMinuteAndSecondCreate";
            this.buttonMinuteAndSecondCreate.Size = new System.Drawing.Size(74, 23);
            this.buttonMinuteAndSecondCreate.TabIndex = 38;
            this.buttonMinuteAndSecondCreate.Text = "Create";
            this.buttonMinuteAndSecondCreate.UseVisualStyleBackColor = true;
            this.buttonMinuteAndSecondCreate.Click += new System.EventHandler(this.buttonMinuteAndSecondCreate_Click);
            // 
            // buttonDeadlockReportCreate
            // 
            this.buttonDeadlockReportCreate.Enabled = false;
            this.buttonDeadlockReportCreate.Location = new System.Drawing.Point(312, 100);
            this.buttonDeadlockReportCreate.Name = "buttonDeadlockReportCreate";
            this.buttonDeadlockReportCreate.Size = new System.Drawing.Size(74, 23);
            this.buttonDeadlockReportCreate.TabIndex = 38;
            this.buttonDeadlockReportCreate.Text = "Create";
            this.buttonDeadlockReportCreate.UseVisualStyleBackColor = true;
            this.buttonDeadlockReportCreate.Click += new System.EventHandler(this.buttonDeadlockReportCreate_Click);
            // 
            // buttonMinuteAndSecondCheck
            // 
            this.buttonMinuteAndSecondCheck.Enabled = false;
            this.buttonMinuteAndSecondCheck.Location = new System.Drawing.Point(133, 129);
            this.buttonMinuteAndSecondCheck.Name = "buttonMinuteAndSecondCheck";
            this.buttonMinuteAndSecondCheck.Size = new System.Drawing.Size(74, 23);
            this.buttonMinuteAndSecondCheck.TabIndex = 37;
            this.buttonMinuteAndSecondCheck.Text = "Check";
            this.buttonMinuteAndSecondCheck.UseVisualStyleBackColor = true;
            this.buttonMinuteAndSecondCheck.Click += new System.EventHandler(this.buttonMinuteAndSecondCheck_Click);
            // 
            // checkBoxMinuteAndSecondStatus
            // 
            this.checkBoxMinuteAndSecondStatus.AutoSize = true;
            this.checkBoxMinuteAndSecondStatus.Enabled = false;
            this.checkBoxMinuteAndSecondStatus.Location = new System.Drawing.Point(214, 133);
            this.checkBoxMinuteAndSecondStatus.Name = "checkBoxMinuteAndSecondStatus";
            this.checkBoxMinuteAndSecondStatus.Size = new System.Drawing.Size(82, 17);
            this.checkBoxMinuteAndSecondStatus.TabIndex = 36;
            this.checkBoxMinuteAndSecondStatus.Text = "Report exist";
            this.checkBoxMinuteAndSecondStatus.UseVisualStyleBackColor = true;
            // 
            // buttonDetailReportView
            // 
            this.buttonDetailReportView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDetailReportView.Location = new System.Drawing.Point(393, 71);
            this.buttonDetailReportView.Name = "buttonDetailReportView";
            this.buttonDetailReportView.Size = new System.Drawing.Size(100, 23);
            this.buttonDetailReportView.TabIndex = 31;
            this.buttonDetailReportView.Text = "View";
            this.buttonDetailReportView.UseVisualStyleBackColor = true;
            this.buttonDetailReportView.Click += new System.EventHandler(this.buttonDetailReportView_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Diagram report status:";
            // 
            // buttonDeadlockReportCheck
            // 
            this.buttonDeadlockReportCheck.Enabled = false;
            this.buttonDeadlockReportCheck.Location = new System.Drawing.Point(133, 100);
            this.buttonDeadlockReportCheck.Name = "buttonDeadlockReportCheck";
            this.buttonDeadlockReportCheck.Size = new System.Drawing.Size(74, 23);
            this.buttonDeadlockReportCheck.TabIndex = 37;
            this.buttonDeadlockReportCheck.Text = "Check";
            this.buttonDeadlockReportCheck.UseVisualStyleBackColor = true;
            this.buttonDeadlockReportCheck.Click += new System.EventHandler(this.buttonDeadlockReportCheck_Click);
            // 
            // checkBoxDeadlockReportStatus
            // 
            this.checkBoxDeadlockReportStatus.AutoSize = true;
            this.checkBoxDeadlockReportStatus.Enabled = false;
            this.checkBoxDeadlockReportStatus.Location = new System.Drawing.Point(214, 104);
            this.checkBoxDeadlockReportStatus.Name = "checkBoxDeadlockReportStatus";
            this.checkBoxDeadlockReportStatus.Size = new System.Drawing.Size(82, 17);
            this.checkBoxDeadlockReportStatus.TabIndex = 36;
            this.checkBoxDeadlockReportStatus.Text = "Report exist";
            this.checkBoxDeadlockReportStatus.UseVisualStyleBackColor = true;
            // 
            // buttonDetailReportCreate
            // 
            this.buttonDetailReportCreate.Enabled = false;
            this.buttonDetailReportCreate.Location = new System.Drawing.Point(312, 71);
            this.buttonDetailReportCreate.Name = "buttonDetailReportCreate";
            this.buttonDetailReportCreate.Size = new System.Drawing.Size(74, 23);
            this.buttonDetailReportCreate.TabIndex = 30;
            this.buttonDetailReportCreate.Text = "Create";
            this.buttonDetailReportCreate.UseVisualStyleBackColor = true;
            this.buttonDetailReportCreate.Click += new System.EventHandler(this.buttonDetailReportCreate_Click);
            // 
            // labelDeadlockReportStatus
            // 
            this.labelDeadlockReportStatus.AutoSize = true;
            this.labelDeadlockReportStatus.Location = new System.Drawing.Point(7, 105);
            this.labelDeadlockReportStatus.Name = "labelDeadlockReportStatus";
            this.labelDeadlockReportStatus.Size = new System.Drawing.Size(117, 13);
            this.labelDeadlockReportStatus.TabIndex = 35;
            this.labelDeadlockReportStatus.Text = "Deadlock report status:";
            // 
            // buttonDraftReportCreate
            // 
            this.buttonDraftReportCreate.Enabled = false;
            this.buttonDraftReportCreate.Location = new System.Drawing.Point(312, 42);
            this.buttonDraftReportCreate.Name = "buttonDraftReportCreate";
            this.buttonDraftReportCreate.Size = new System.Drawing.Size(74, 23);
            this.buttonDraftReportCreate.TabIndex = 34;
            this.buttonDraftReportCreate.Text = "Create";
            this.buttonDraftReportCreate.UseVisualStyleBackColor = true;
            this.buttonDraftReportCreate.Click += new System.EventHandler(this.buttonDraftReportCreate_Click);
            // 
            // buttonDetailReportCheck
            // 
            this.buttonDetailReportCheck.Enabled = false;
            this.buttonDetailReportCheck.Location = new System.Drawing.Point(133, 71);
            this.buttonDetailReportCheck.Name = "buttonDetailReportCheck";
            this.buttonDetailReportCheck.Size = new System.Drawing.Size(74, 23);
            this.buttonDetailReportCheck.TabIndex = 29;
            this.buttonDetailReportCheck.Text = "Check";
            this.buttonDetailReportCheck.UseVisualStyleBackColor = true;
            this.buttonDetailReportCheck.Click += new System.EventHandler(this.buttonDetailReportCheck_Click);
            // 
            // buttonErrorReportCreate
            // 
            this.buttonErrorReportCreate.Enabled = false;
            this.buttonErrorReportCreate.Location = new System.Drawing.Point(312, 13);
            this.buttonErrorReportCreate.Name = "buttonErrorReportCreate";
            this.buttonErrorReportCreate.Size = new System.Drawing.Size(74, 23);
            this.buttonErrorReportCreate.TabIndex = 38;
            this.buttonErrorReportCreate.Text = "Create";
            this.buttonErrorReportCreate.UseVisualStyleBackColor = true;
            this.buttonErrorReportCreate.Click += new System.EventHandler(this.buttonErrorReportCreate_Click);
            // 
            // checkBoxDetailReportStatus
            // 
            this.checkBoxDetailReportStatus.AutoSize = true;
            this.checkBoxDetailReportStatus.Enabled = false;
            this.checkBoxDetailReportStatus.Location = new System.Drawing.Point(214, 75);
            this.checkBoxDetailReportStatus.Name = "checkBoxDetailReportStatus";
            this.checkBoxDetailReportStatus.Size = new System.Drawing.Size(82, 17);
            this.checkBoxDetailReportStatus.TabIndex = 28;
            this.checkBoxDetailReportStatus.Text = "Report exist";
            this.checkBoxDetailReportStatus.UseVisualStyleBackColor = true;
            // 
            // buttonDraftReportCheck
            // 
            this.buttonDraftReportCheck.Enabled = false;
            this.buttonDraftReportCheck.Location = new System.Drawing.Point(133, 42);
            this.buttonDraftReportCheck.Name = "buttonDraftReportCheck";
            this.buttonDraftReportCheck.Size = new System.Drawing.Size(74, 23);
            this.buttonDraftReportCheck.TabIndex = 33;
            this.buttonDraftReportCheck.Text = "Check";
            this.buttonDraftReportCheck.UseVisualStyleBackColor = true;
            this.buttonDraftReportCheck.Click += new System.EventHandler(this.buttonDraftReportCheck_Click);
            // 
            // labelDetailReportStatus
            // 
            this.labelDetailReportStatus.AutoSize = true;
            this.labelDetailReportStatus.Location = new System.Drawing.Point(6, 76);
            this.labelDetailReportStatus.Name = "labelDetailReportStatus";
            this.labelDetailReportStatus.Size = new System.Drawing.Size(98, 13);
            this.labelDetailReportStatus.TabIndex = 7;
            this.labelDetailReportStatus.Text = "Detail report status:";
            // 
            // checkBoxDraftReportStatus
            // 
            this.checkBoxDraftReportStatus.AutoSize = true;
            this.checkBoxDraftReportStatus.Enabled = false;
            this.checkBoxDraftReportStatus.Location = new System.Drawing.Point(214, 47);
            this.checkBoxDraftReportStatus.Name = "checkBoxDraftReportStatus";
            this.checkBoxDraftReportStatus.Size = new System.Drawing.Size(82, 17);
            this.checkBoxDraftReportStatus.TabIndex = 32;
            this.checkBoxDraftReportStatus.Text = "Report exist";
            this.checkBoxDraftReportStatus.UseVisualStyleBackColor = true;
            // 
            // buttonErrorStatCheck
            // 
            this.buttonErrorStatCheck.Enabled = false;
            this.buttonErrorStatCheck.Location = new System.Drawing.Point(133, 13);
            this.buttonErrorStatCheck.Name = "buttonErrorStatCheck";
            this.buttonErrorStatCheck.Size = new System.Drawing.Size(74, 23);
            this.buttonErrorStatCheck.TabIndex = 37;
            this.buttonErrorStatCheck.Text = "Check";
            this.buttonErrorStatCheck.UseVisualStyleBackColor = true;
            this.buttonErrorStatCheck.Click += new System.EventHandler(this.buttonErrorStatCheck_Click);
            // 
            // labelDraftReportStatus
            // 
            this.labelDraftReportStatus.AutoSize = true;
            this.labelDraftReportStatus.Location = new System.Drawing.Point(7, 47);
            this.labelDraftReportStatus.Name = "labelDraftReportStatus";
            this.labelDraftReportStatus.Size = new System.Drawing.Size(94, 13);
            this.labelDraftReportStatus.TabIndex = 31;
            this.labelDraftReportStatus.Text = "Draft report status:";
            // 
            // checkBoxErrorReportStatus
            // 
            this.checkBoxErrorReportStatus.AutoSize = true;
            this.checkBoxErrorReportStatus.Enabled = false;
            this.checkBoxErrorReportStatus.Location = new System.Drawing.Point(214, 17);
            this.checkBoxErrorReportStatus.Name = "checkBoxErrorReportStatus";
            this.checkBoxErrorReportStatus.Size = new System.Drawing.Size(82, 17);
            this.checkBoxErrorReportStatus.TabIndex = 36;
            this.checkBoxErrorReportStatus.Text = "Report exist";
            this.checkBoxErrorReportStatus.UseVisualStyleBackColor = true;
            // 
            // labelErrorReportStatus
            // 
            this.labelErrorReportStatus.AutoSize = true;
            this.labelErrorReportStatus.Location = new System.Drawing.Point(6, 18);
            this.labelErrorReportStatus.Name = "labelErrorReportStatus";
            this.labelErrorReportStatus.Size = new System.Drawing.Size(93, 13);
            this.labelErrorReportStatus.TabIndex = 35;
            this.labelErrorReportStatus.Text = "Error report status:";
            // 
            // panelConsole
            // 
            this.panelConsole.Controls.Add(this.groupBoxReports);
            this.panelConsole.Controls.Add(this.groupBoxPrepare);
            this.panelConsole.Controls.Add(this.groupBoxTable);
            this.panelConsole.Controls.Add(this.groupBoxFunction);
            this.panelConsole.Controls.Add(this.groupBoxConnect);
            this.panelConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConsole.Location = new System.Drawing.Point(0, 0);
            this.panelConsole.Name = "panelConsole";
            this.panelConsole.Size = new System.Drawing.Size(502, 508);
            this.panelConsole.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 508);
            this.Controls.Add(this.panelConsole);
            this.Name = "MainForm";
            this.Text = "Prepare for detail analyse";
            this.groupBoxConnect.ResumeLayout(false);
            this.groupBoxConnect.PerformLayout();
            this.groupBoxFunction.ResumeLayout(false);
            this.groupBoxFunction.PerformLayout();
            this.groupBoxTable.ResumeLayout(false);
            this.groupBoxTable.PerformLayout();
            this.groupBoxPrepare.ResumeLayout(false);
            this.groupBoxPrepare.PerformLayout();
            this.groupBoxReports.ResumeLayout(false);
            this.groupBoxReports.PerformLayout();
            this.panelConsole.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerPrepareTabele;
        private System.Windows.Forms.GroupBox groupBoxConnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxDB;
        private System.Windows.Forms.ComboBox comboBoxSQLServer;
        private System.Windows.Forms.Label labelDB;
        private System.Windows.Forms.Label labelSQLServer;
        private System.Windows.Forms.GroupBox groupBoxTable;
        private System.Windows.Forms.ComboBox comboBoxTable;
        private System.Windows.Forms.Label labelTable;
        private System.Windows.Forms.Button buttonDraftReportCreate;
        private System.Windows.Forms.Button buttonDraftReportCheck;
        private System.Windows.Forms.CheckBox checkBoxDraftReportStatus;
        private System.Windows.Forms.Label labelDraftReportStatus;
        private System.Windows.Forms.GroupBox groupBoxReports;
        private System.Windows.Forms.Button buttonErrorReportCreate;
        private System.Windows.Forms.Button buttonErrorStatCheck;
        private System.Windows.Forms.CheckBox checkBoxErrorReportStatus;
        private System.Windows.Forms.Label labelErrorReportStatus;
        private System.Windows.Forms.GroupBox groupBoxPrepare;
        private System.Windows.Forms.Button buttonTextKeyCreate;
        private System.Windows.Forms.Button buttonTextKeyCheck;
        private System.Windows.Forms.CheckBox checkBoxTextKeyStatus;
        private System.Windows.Forms.Label labelTextKeyStatus;
        private System.Windows.Forms.TextBox textBoxStopTime;
        private System.Windows.Forms.TextBox textBoxPreparedRowProgress;
        private System.Windows.Forms.TextBox textBoxPreparedRowCount;
        private System.Windows.Forms.TextBox textBoxStartTime;
        private System.Windows.Forms.TextBox textBoxRowCount;
        private System.Windows.Forms.Label labelPreparedRowProgress;
        private System.Windows.Forms.Label labelStopTime;
        private System.Windows.Forms.Label labelPreparedRowCount;
        private System.Windows.Forms.Label labelStartTime;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelRowCount;
        private System.Windows.Forms.Button buttonDetailReportCreate;
        private System.Windows.Forms.Button buttonDetailReportCheck;
        private System.Windows.Forms.CheckBox checkBoxDetailReportStatus;
        private System.Windows.Forms.Label labelDetailReportStatus;
        private System.Windows.Forms.Button buttonDeadlockReportCreate;
        private System.Windows.Forms.Button buttonDeadlockReportCheck;
        private System.Windows.Forms.CheckBox checkBoxDeadlockReportStatus;
        private System.Windows.Forms.Label labelDeadlockReportStatus;
        private System.Windows.Forms.Panel panelConsole;
		private System.Windows.Forms.Button buttonMinuteAndSecondCreate;
		private System.Windows.Forms.Button buttonMinuteAndSecondCheck;
		private System.Windows.Forms.CheckBox checkBoxMinuteAndSecondStatus;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonStopSP;
		private System.Windows.Forms.Button buttonStartSP;
		private System.ComponentModel.BackgroundWorker backgroundWorkerPrepareTableSP;
		private System.Windows.Forms.Button buttonDetailReportView;
        private System.Windows.Forms.Button buttonCheckFunction;
        private System.Windows.Forms.GroupBox groupBoxFunction;
        private System.Windows.Forms.Button buttonCreateFunction;
        private System.Windows.Forms.CheckBox checkBoxFunctionExist;
        private System.Windows.Forms.Label labelFunctionStatus;
        private System.Windows.Forms.Button buttonCheckTable;
    }
}

