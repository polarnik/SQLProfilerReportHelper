using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.SQLProfilerReportHelper.Model
{
	public class DetailStat
	{
		public string DatabaseName { get; set; }
		public string TextKeyKey { get; set; }
		public int? AvgCPUKey { get; set; }
		public long? AvgDurationKey { get; set; }
		public double? PercentDurationKey { get; set; }
		public long? AvgReadsKey { get; set; }
		public int? CountKey { get; set; }

		public string TextKey { get; set; }

		public int? MinCPU { get; set; }
		public int? AvgCPU { get; set; }
		public int? MaxCPU { get; set; }
		public int? SumCPU { get; set; }
		public double? PercentCPU { get; set; }

		public long? MinDuration { get; set; }
		public long? AvgDuration { get; set; }
		public long? MaxDuration { get; set; }
		public long? SumDuration { get; set; }
		public double? PercentDuration { get; set; }

		public long? MinReads { get; set; }
		public long? AvgReads { get; set; }
		public long? MaxReads { get; set; }
		public long? SumReads { get; set; }
		public double? PercentReads { get; set; }

		public long? MinWrites { get; set; }
		public long? AvgWrites { get; set; }
		public long? MaxWrites { get; set; }
		public long? SumWrites { get; set; }
		public double? PercentWrites { get; set; }

		public int? Count { get; set; }
		public double? PercentCount { get; set; }

		public string TextDataMinDuration { get; set; }
		public string TextDataMaxDuration { get; set; }

		public string TextDataMinReads { get; set; }
		public string TextDataMaxReads { get; set; }

		public string TextDataMinCPU { get; set; }
		public string TextDataMaxCPU { get; set; }

		public string TextDataMinWrites { get; set; }
		public string TextDataMaxWrites { get; set; }

		public long? MinDurationRaw { get; set; }
		public long? MaxDurationRaw { get; set; }
	}
}
