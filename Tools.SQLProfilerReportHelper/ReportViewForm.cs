using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Tools.SQLProfilerReportHelper
{
	public partial class ReportViewForm : Form
	{
		Helper TableUtil { get; set; }

		public ReportViewForm(Helper tableUtil)
		{
			TableUtil = tableUtil;
			InitializeComponent();
		}

		IEnumerable<Model.DetailStat> Data { get; set; }

		public void LoadDetailStat(IEnumerable<Model.DetailStat> data)
		{
			Data = data;

			listViewDetails.Groups.Clear();
			listViewDetails.Columns.Clear();

			listViewDetails.Columns.Add("DatabaseName");
			listViewDetails.Columns.Add("TextKey-key");
			listViewDetails.Columns.Add("avg(CPU)-key");
			listViewDetails.Columns.Add("avg(Duration)-key");
			listViewDetails.Columns.Add("% Duration-key");
			listViewDetails.Columns.Add("avg(Reads)-key");
			listViewDetails.Columns.Add("Count-key");
			listViewDetails.Columns.Add("TextKey");

			listViewDetails.Columns.Add("min(CPU)");
			listViewDetails.Columns.Add("avg(CPU)");
			listViewDetails.Columns.Add("max(CPU)");
			listViewDetails.Columns.Add("sum(CPU)");
			listViewDetails.Columns.Add("% CPU");

			listViewDetails.Columns.Add("min(Duration)");
			listViewDetails.Columns.Add("avg(Duration)");
			listViewDetails.Columns.Add("max(Duration)");
			listViewDetails.Columns.Add("sum(Duration)");
			listViewDetails.Columns.Add("% Duration");

			listViewDetails.Columns.Add("min(Reads)");
			listViewDetails.Columns.Add("avg(Reads)");
			listViewDetails.Columns.Add("max(Reads)");
			listViewDetails.Columns.Add("sum(Reads)");
			listViewDetails.Columns.Add("% Reads");

			listViewDetails.Columns.Add("min(Writes)");
			listViewDetails.Columns.Add("avg(Writes)");
			listViewDetails.Columns.Add("max(Writes)");
			listViewDetails.Columns.Add("sum(Writes)");
			listViewDetails.Columns.Add("% Writes");

			listViewDetails.Columns.Add("Count");
			listViewDetails.Columns.Add("% Count");

            UpdateDetailStat(data);
		}

        public void UpdateDetailStat(IEnumerable<Model.DetailStat> data)
        {
            Data = data;
            listViewDetails.Items.Clear();
            foreach (Model.DetailStat dataItem in data)
            {
                ListViewItem item = listViewDetails.Items.Add(dataItem.DatabaseName);
                item.SubItems.Add(dataItem.TextKeyKey.ToString());
                item.SubItems.Add(dataItem.AvgCPUKey.ToString());
                item.SubItems.Add(dataItem.AvgDurationKey.ToString());
                item.SubItems.Add(dataItem.PercentDurationKey.ToString());
                item.SubItems.Add(dataItem.AvgReadsKey.ToString());
                item.SubItems.Add(dataItem.CountKey.ToString());
                item.SubItems.Add(dataItem.TextKey.ToString());

                item.SubItems.Add(dataItem.MinCPU.ToString());
                item.SubItems.Add(dataItem.AvgCPU.ToString());
                item.SubItems.Add(dataItem.MaxCPU.ToString());
                item.SubItems.Add(dataItem.SumCPU.ToString());
                item.SubItems.Add(dataItem.PercentCPU.ToString());

                item.SubItems.Add(dataItem.MinDuration.ToString());
                item.SubItems.Add(dataItem.AvgDuration.ToString());
                item.SubItems.Add(dataItem.MaxDuration.ToString());
                item.SubItems.Add(dataItem.SumDuration.ToString());
                item.SubItems.Add(dataItem.PercentDuration.ToString());

                item.SubItems.Add(dataItem.MinReads.ToString());
                item.SubItems.Add(dataItem.AvgReads.ToString());
                item.SubItems.Add(dataItem.MaxReads.ToString());
                item.SubItems.Add(dataItem.SumReads.ToString());
                item.SubItems.Add(dataItem.PercentReads.ToString());

                item.SubItems.Add(dataItem.MinWrites.ToString());
                item.SubItems.Add(dataItem.AvgWrites.ToString());
                item.SubItems.Add(dataItem.MaxWrites.ToString());
                item.SubItems.Add(dataItem.SumWrites.ToString());
                item.SubItems.Add(dataItem.PercentWrites.ToString());

                item.SubItems.Add(dataItem.Count.ToString());
                item.SubItems.Add(dataItem.PercentCount.ToString());

                item.Tag = dataItem;
            }
        }

		private void listViewDetails_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			ListViewItem item = e.Item;
			if (item != null)
			{
				Model.DetailStat dataItem = item.Tag as Model.DetailStat;

				dataItem = TableUtil.FillDetailStat(dataItem);

				this.textBoxMinDuration.Text = trancate(dataItem.TextDataMinDuration);
				this.textBoxMaxDuration.Text = trancate(dataItem.TextDataMaxDuration);

				this.textBoxMinCPU.Text = trancate(dataItem.TextDataMinCPU);
				this.textBoxMaxCPU.Text = trancate(dataItem.TextDataMaxCPU);

				this.textBoxMinReads.Text = trancate(dataItem.TextDataMinReads);
				this.textBoxMaxReads.Text = trancate(dataItem.TextDataMaxReads);

				this.textBoxMinWrites.Text = trancate(dataItem.TextDataMinWrites);
				this.textBoxMaxWrites.Text = trancate(dataItem.TextDataMaxWrites);
			}
		}

		private string trancate(string content)
		{
			int size = 10000;
			if (content.Length > size)
			{
				return content.Substring(0, size);
			}
			else
			{
				return content;
			}
		}

		private void TrySaveFile(string fileName, string content)
		{
			try{
				System.IO.File.WriteAllText(fileName, content);
			}
			catch(Exception)
			{
			}
		}

		private void listViewDetails_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (e.Column >= 0 && e.Column <= 8)
			{
				IEnumerable<Model.DetailStat> data = null;
				switch (e.Column)
				{
					case 0:
						data = Data.OrderBy(item => item.DatabaseName);
						break;
					case 1:
						data = Data.OrderBy(item => item.TextKeyKey);
						break;
					case 2:
						data = Data.OrderByDescending(item => item.AvgCPUKey);
						break;
					case 3:
						data = Data.OrderByDescending(item => item.AvgDurationKey);
						break;
					case 4:
						data = Data.OrderByDescending(item => item.PercentDurationKey);
						break;
					case 5:
						data = Data.OrderByDescending(item => item.AvgReadsKey);
						break;
					case 6:
						data = Data.OrderByDescending(item => item.CountKey);
						break;
				}
                UpdateDetailStat(data);
			}
		}
	}
}
