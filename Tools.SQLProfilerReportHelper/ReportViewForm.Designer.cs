namespace Tools.SQLProfilerReportHelper
{
	partial class ReportViewForm
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
			this.splitContainerMain = new System.Windows.Forms.SplitContainer();
			this.listViewDetails = new System.Windows.Forms.ListView();
			this.tabControlTextData = new System.Windows.Forms.TabControl();
			this.tabPageMinDuration = new System.Windows.Forms.TabPage();
			this.textBoxMinDuration = new System.Windows.Forms.TextBox();
			this.tabPageMaxDuration = new System.Windows.Forms.TabPage();
			this.textBoxMaxDuration = new System.Windows.Forms.TextBox();
			this.tabPageMinReads = new System.Windows.Forms.TabPage();
			this.textBoxMinReads = new System.Windows.Forms.TextBox();
			this.tabPageMaxReads = new System.Windows.Forms.TabPage();
			this.textBoxMaxReads = new System.Windows.Forms.TextBox();
			this.tabPageMinCPU = new System.Windows.Forms.TabPage();
			this.textBoxMinCPU = new System.Windows.Forms.TextBox();
			this.tabPageMaxCPU = new System.Windows.Forms.TabPage();
			this.textBoxMaxCPU = new System.Windows.Forms.TextBox();
			this.tabPageMinWrites = new System.Windows.Forms.TabPage();
			this.textBoxMinWrites = new System.Windows.Forms.TextBox();
			this.tabPageMaxWrites = new System.Windows.Forms.TabPage();
			this.textBoxMaxWrites = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
			this.splitContainerMain.Panel1.SuspendLayout();
			this.splitContainerMain.Panel2.SuspendLayout();
			this.splitContainerMain.SuspendLayout();
			this.tabControlTextData.SuspendLayout();
			this.tabPageMinDuration.SuspendLayout();
			this.tabPageMaxDuration.SuspendLayout();
			this.tabPageMinReads.SuspendLayout();
			this.tabPageMaxReads.SuspendLayout();
			this.tabPageMinCPU.SuspendLayout();
			this.tabPageMaxCPU.SuspendLayout();
			this.tabPageMinWrites.SuspendLayout();
			this.tabPageMaxWrites.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainerMain
			// 
			this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainerMain.Location = new System.Drawing.Point(0, 0);
			this.splitContainerMain.Name = "splitContainerMain";
			this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainerMain.Panel1
			// 
			this.splitContainerMain.Panel1.Controls.Add(this.listViewDetails);
			// 
			// splitContainerMain.Panel2
			// 
			this.splitContainerMain.Panel2.Controls.Add(this.tabControlTextData);
			this.splitContainerMain.Size = new System.Drawing.Size(1032, 592);
			this.splitContainerMain.SplitterDistance = 339;
			this.splitContainerMain.TabIndex = 0;
			// 
			// listViewDetails
			// 
			this.listViewDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewDetails.HideSelection = false;
			this.listViewDetails.Location = new System.Drawing.Point(0, 0);
			this.listViewDetails.Name = "listViewDetails";
			this.listViewDetails.Size = new System.Drawing.Size(1032, 339);
			this.listViewDetails.TabIndex = 0;
			this.listViewDetails.UseCompatibleStateImageBehavior = false;
			this.listViewDetails.View = System.Windows.Forms.View.Details;
			this.listViewDetails.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewDetails_ColumnClick);
			this.listViewDetails.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewDetails_ItemSelectionChanged);
			// 
			// tabControlTextData
			// 
			this.tabControlTextData.Controls.Add(this.tabPageMinDuration);
			this.tabControlTextData.Controls.Add(this.tabPageMaxDuration);
			this.tabControlTextData.Controls.Add(this.tabPageMinReads);
			this.tabControlTextData.Controls.Add(this.tabPageMaxReads);
			this.tabControlTextData.Controls.Add(this.tabPageMinCPU);
			this.tabControlTextData.Controls.Add(this.tabPageMaxCPU);
			this.tabControlTextData.Controls.Add(this.tabPageMinWrites);
			this.tabControlTextData.Controls.Add(this.tabPageMaxWrites);
			this.tabControlTextData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlTextData.Location = new System.Drawing.Point(0, 0);
			this.tabControlTextData.Name = "tabControlTextData";
			this.tabControlTextData.SelectedIndex = 0;
			this.tabControlTextData.Size = new System.Drawing.Size(1032, 249);
			this.tabControlTextData.TabIndex = 0;
			// 
			// tabPageMinDuration
			// 
			this.tabPageMinDuration.Controls.Add(this.textBoxMinDuration);
			this.tabPageMinDuration.Location = new System.Drawing.Point(4, 22);
			this.tabPageMinDuration.Name = "tabPageMinDuration";
			this.tabPageMinDuration.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMinDuration.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMinDuration.TabIndex = 0;
			this.tabPageMinDuration.Text = "min(Duration)";
			this.tabPageMinDuration.UseVisualStyleBackColor = true;
			// 
			// textBoxMinDuration
			// 
			this.textBoxMinDuration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMinDuration.Location = new System.Drawing.Point(3, 3);
			this.textBoxMinDuration.Multiline = true;
			this.textBoxMinDuration.Name = "textBoxMinDuration";
			this.textBoxMinDuration.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMinDuration.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMinDuration.TabIndex = 0;
			// 
			// tabPageMaxDuration
			// 
			this.tabPageMaxDuration.Controls.Add(this.textBoxMaxDuration);
			this.tabPageMaxDuration.Location = new System.Drawing.Point(4, 22);
			this.tabPageMaxDuration.Name = "tabPageMaxDuration";
			this.tabPageMaxDuration.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMaxDuration.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMaxDuration.TabIndex = 1;
			this.tabPageMaxDuration.Text = "max(Duration)";
			this.tabPageMaxDuration.UseVisualStyleBackColor = true;
			// 
			// textBoxMaxDuration
			// 
			this.textBoxMaxDuration.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMaxDuration.Location = new System.Drawing.Point(3, 3);
			this.textBoxMaxDuration.Multiline = true;
			this.textBoxMaxDuration.Name = "textBoxMaxDuration";
			this.textBoxMaxDuration.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMaxDuration.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMaxDuration.TabIndex = 1;
			// 
			// tabPageMinReads
			// 
			this.tabPageMinReads.Controls.Add(this.textBoxMinReads);
			this.tabPageMinReads.Location = new System.Drawing.Point(4, 22);
			this.tabPageMinReads.Name = "tabPageMinReads";
			this.tabPageMinReads.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMinReads.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMinReads.TabIndex = 2;
			this.tabPageMinReads.Text = "min(Reads)";
			this.tabPageMinReads.UseVisualStyleBackColor = true;
			// 
			// textBoxMinReads
			// 
			this.textBoxMinReads.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMinReads.Location = new System.Drawing.Point(3, 3);
			this.textBoxMinReads.Multiline = true;
			this.textBoxMinReads.Name = "textBoxMinReads";
			this.textBoxMinReads.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMinReads.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMinReads.TabIndex = 1;
			// 
			// tabPageMaxReads
			// 
			this.tabPageMaxReads.Controls.Add(this.textBoxMaxReads);
			this.tabPageMaxReads.Location = new System.Drawing.Point(4, 22);
			this.tabPageMaxReads.Name = "tabPageMaxReads";
			this.tabPageMaxReads.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMaxReads.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMaxReads.TabIndex = 3;
			this.tabPageMaxReads.Text = "max(Reads)";
			this.tabPageMaxReads.UseVisualStyleBackColor = true;
			// 
			// textBoxMaxReads
			// 
			this.textBoxMaxReads.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMaxReads.Location = new System.Drawing.Point(3, 3);
			this.textBoxMaxReads.Multiline = true;
			this.textBoxMaxReads.Name = "textBoxMaxReads";
			this.textBoxMaxReads.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMaxReads.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMaxReads.TabIndex = 1;
			// 
			// tabPageMinCPU
			// 
			this.tabPageMinCPU.Controls.Add(this.textBoxMinCPU);
			this.tabPageMinCPU.Location = new System.Drawing.Point(4, 22);
			this.tabPageMinCPU.Name = "tabPageMinCPU";
			this.tabPageMinCPU.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMinCPU.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMinCPU.TabIndex = 4;
			this.tabPageMinCPU.Text = "min(CPU)";
			this.tabPageMinCPU.UseVisualStyleBackColor = true;
			// 
			// textBoxMinCPU
			// 
			this.textBoxMinCPU.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMinCPU.Location = new System.Drawing.Point(3, 3);
			this.textBoxMinCPU.Multiline = true;
			this.textBoxMinCPU.Name = "textBoxMinCPU";
			this.textBoxMinCPU.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMinCPU.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMinCPU.TabIndex = 1;
			// 
			// tabPageMaxCPU
			// 
			this.tabPageMaxCPU.Controls.Add(this.textBoxMaxCPU);
			this.tabPageMaxCPU.Location = new System.Drawing.Point(4, 22);
			this.tabPageMaxCPU.Name = "tabPageMaxCPU";
			this.tabPageMaxCPU.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMaxCPU.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMaxCPU.TabIndex = 5;
			this.tabPageMaxCPU.Text = "max(CPU)";
			this.tabPageMaxCPU.UseVisualStyleBackColor = true;
			// 
			// textBoxMaxCPU
			// 
			this.textBoxMaxCPU.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMaxCPU.Location = new System.Drawing.Point(3, 3);
			this.textBoxMaxCPU.Multiline = true;
			this.textBoxMaxCPU.Name = "textBoxMaxCPU";
			this.textBoxMaxCPU.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMaxCPU.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMaxCPU.TabIndex = 1;
			// 
			// tabPageMinWrites
			// 
			this.tabPageMinWrites.Controls.Add(this.textBoxMinWrites);
			this.tabPageMinWrites.Location = new System.Drawing.Point(4, 22);
			this.tabPageMinWrites.Name = "tabPageMinWrites";
			this.tabPageMinWrites.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMinWrites.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMinWrites.TabIndex = 6;
			this.tabPageMinWrites.Text = "min(Writes)";
			this.tabPageMinWrites.UseVisualStyleBackColor = true;
			// 
			// textBoxMinWrites
			// 
			this.textBoxMinWrites.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMinWrites.Location = new System.Drawing.Point(3, 3);
			this.textBoxMinWrites.Multiline = true;
			this.textBoxMinWrites.Name = "textBoxMinWrites";
			this.textBoxMinWrites.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMinWrites.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMinWrites.TabIndex = 1;
			// 
			// tabPageMaxWrites
			// 
			this.tabPageMaxWrites.Controls.Add(this.textBoxMaxWrites);
			this.tabPageMaxWrites.Location = new System.Drawing.Point(4, 22);
			this.tabPageMaxWrites.Name = "tabPageMaxWrites";
			this.tabPageMaxWrites.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageMaxWrites.Size = new System.Drawing.Size(1024, 223);
			this.tabPageMaxWrites.TabIndex = 7;
			this.tabPageMaxWrites.Text = "max(Writes)";
			this.tabPageMaxWrites.UseVisualStyleBackColor = true;
			// 
			// textBoxMaxWrites
			// 
			this.textBoxMaxWrites.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxMaxWrites.Location = new System.Drawing.Point(3, 3);
			this.textBoxMaxWrites.Multiline = true;
			this.textBoxMaxWrites.Name = "textBoxMaxWrites";
			this.textBoxMaxWrites.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxMaxWrites.Size = new System.Drawing.Size(1018, 217);
			this.textBoxMaxWrites.TabIndex = 1;
			// 
			// ReportViewForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1032, 592);
			this.Controls.Add(this.splitContainerMain);
			this.Name = "ReportViewForm";
			this.Text = "ReportViewForm";
			this.splitContainerMain.Panel1.ResumeLayout(false);
			this.splitContainerMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
			this.splitContainerMain.ResumeLayout(false);
			this.tabControlTextData.ResumeLayout(false);
			this.tabPageMinDuration.ResumeLayout(false);
			this.tabPageMinDuration.PerformLayout();
			this.tabPageMaxDuration.ResumeLayout(false);
			this.tabPageMaxDuration.PerformLayout();
			this.tabPageMinReads.ResumeLayout(false);
			this.tabPageMinReads.PerformLayout();
			this.tabPageMaxReads.ResumeLayout(false);
			this.tabPageMaxReads.PerformLayout();
			this.tabPageMinCPU.ResumeLayout(false);
			this.tabPageMinCPU.PerformLayout();
			this.tabPageMaxCPU.ResumeLayout(false);
			this.tabPageMaxCPU.PerformLayout();
			this.tabPageMinWrites.ResumeLayout(false);
			this.tabPageMinWrites.PerformLayout();
			this.tabPageMaxWrites.ResumeLayout(false);
			this.tabPageMaxWrites.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainerMain;
		private System.Windows.Forms.ListView listViewDetails;
		private System.Windows.Forms.TabControl tabControlTextData;
		private System.Windows.Forms.TabPage tabPageMinDuration;
		private System.Windows.Forms.TextBox textBoxMinDuration;
		private System.Windows.Forms.TabPage tabPageMaxDuration;
		private System.Windows.Forms.TextBox textBoxMaxDuration;
		private System.Windows.Forms.TabPage tabPageMinReads;
		private System.Windows.Forms.TextBox textBoxMinReads;
		private System.Windows.Forms.TabPage tabPageMaxReads;
		private System.Windows.Forms.TextBox textBoxMaxReads;
		private System.Windows.Forms.TabPage tabPageMinCPU;
		private System.Windows.Forms.TextBox textBoxMinCPU;
		private System.Windows.Forms.TabPage tabPageMaxCPU;
		private System.Windows.Forms.TextBox textBoxMaxCPU;
		private System.Windows.Forms.TabPage tabPageMinWrites;
		private System.Windows.Forms.TextBox textBoxMinWrites;
		private System.Windows.Forms.TabPage tabPageMaxWrites;
		private System.Windows.Forms.TextBox textBoxMaxWrites;
	}
}