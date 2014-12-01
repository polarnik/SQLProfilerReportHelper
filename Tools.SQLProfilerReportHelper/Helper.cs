namespace Tools.SQLProfilerReportHelper
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Data.SqlClient;
	using System.Collections.ObjectModel;

	public class Helper
    {
        private SqlConnection Connection { get; set; }
        private string Server { get; set; }
        private string DataBase { get; set; }

        string tableName;
        /// <summary>
        /// get/set Имя таблицы с данными профайлинга
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set
            {
                if (TableExist(value))
                {
                    tableName = value;
                }
                else
                {
                    throw new Exception(string.Format("Table {0} not exist", tableName));
                }
            }
        }

        /// <summary>
        /// get Имя таблицы с отчётом по производительности хранимых процедур (быстрый черновой отчёт)
        /// </summary>
        public string TableNameDraft
        {
            get { return TableName + ".DraftStat"; }
        }

        /// <summary>
        /// get Имя таблицы с отчётом по производительности всех SQL-запросов
        /// </summary>
        public string TableNameDetail
        {
            get { return TableName + ".DetailStat"; }
        }

        /// <summary>
        /// get Имя таблицы с отчётом по статистике ошибок
        /// </summary>
        public string TableNameError
        {
            get { return TableName + ".ErrorStat"; }
        }

        /// <summary>
        /// get Имя таблицы с взаимоблокировками
        /// </summary>
        public string TableNameDeadlock
        {
            get { return TableName + ".DeadlockGraphs"; }
        }

        public DateTime StartTime { get; set; }
        public DateTime ExpectedStopTime { get { return GetExpectedStopTime(); } }
		public DateTime ExpectedStopTimeSP { get { return GetExpectedStopTimeSP(); } }

		public bool PreparedIsComplete { get { return (RowCountForPrepare < RowCountPrepared) || RowCountForPrepare == 0; } }

		public bool PreparedIsCompleteSP { get { return (RowCountForPrepareSP < RowCountPreparedSP) || RowCountForPrepareSP == 0; } }

        public string[] Tables { get { return GetTables(); } }

        public int RowCountForPrepare { get; set; }
        public int RowCountPrepared { get; set; }

		public int RowCountForPrepareSP { get; set; }
		public int RowCountPreparedSP { get; set; }
		
		public void Connect(string server, string database)
        {
            this.Server = server;
            this.DataBase = database;

            var connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.DataSource = this.Server;
            connectionBuilder.InitialCatalog = this.DataBase;
            connectionBuilder.IntegratedSecurity = true;

            this.Connection = new SqlConnection(connectionBuilder.ConnectionString);
            this.Connection.Open();

            this.RowCountPrepared = 0;
        }

        DateTime GetExpectedStopTime()
        {
            var duration = DateTime.Now.Subtract(this.StartTime).TotalSeconds;
            var expectedDuration = 0.0;
            if(this.RowCountPrepared > 0)
                expectedDuration = duration * this.RowCountForPrepare / this.RowCountPrepared;
            else
                expectedDuration = duration * this.RowCountForPrepare / 10;

            return this.StartTime.AddSeconds(expectedDuration);
        }

        /// <summary>
        /// Расчёт времени, необходимого для завершения операции обработки хранимых процедур.
        /// </summary>
        /// <returns>Момент времени, в который, предположительно операция завершится (время в текущем часовом поясе)</returns>
        /// <remarks>
        /// Метод зависит от внешних переменных.
        /// Предполагается, что:
        /// this.StartTime - момент времени начала операции (время в текущем часовом поясе), изначально DateTime.Now.
        /// this.RowCountPreparedSP - количество уже обработанных строк таблицы, с хранимыми процедурами, изначально 0.
        /// this.RowCountForPrepareSP - количество строк таблицы, которые нужно обработать, изначально равно количеству вызовов хранимых процедур в таблице профайлинга.
        /// </remarks>
		DateTime GetExpectedStopTimeSP()
		{
            //Длительность обработки текущего количества строк (RowCountPreparedSP)
			double duration = DateTime.Now.Subtract(this.StartTime).TotalSeconds;
            double expectedDuration = 0.0;

            //Длительность обработки полного количества строк (RowCountForPrepareSP) расчитывается по пропорции
			if (this.RowCountPreparedSP > 0)
				expectedDuration = duration * this.RowCountForPrepareSP / this.RowCountPreparedSP;
			else
                //Чтобы не было деления на 0, в качестве первого приближения расчитывается время завершения так,
                //как будто, с момента начала операции уже обраборано 10 строк из полного количества.
				expectedDuration = duration * this.RowCountForPrepareSP / 10;

			return this.StartTime.AddSeconds(expectedDuration);
		}

		public void DropIndexOnTextKeys()
		{
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandTimeout = 10000;
            command.CommandText = string.Format(@"
DROP NONCLUSTERED INDEX [IX_TraceTable_TextKey_DatabaseName]
ON [dbo].[{0}]
", this.TableName);
			command.ExecuteNonQuery();
		}

		public void CreateIndexOnTextKeys()
		{
			var command = new SqlCommand();
			command.Connection = this.Connection;
			command.CommandTimeout = 10000;
			command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_TextKey_DatabaseName]
ON [dbo].[{0}] ([DatabaseName],[TextKey])
", this.TableName);
			command.ExecuteNonQuery();
		}

        public void PrepareTextKeys()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandTimeout = 10000;
            command.CommandText = string.Format(@"
update TOP (100)
[dbo].[{0}]
set [TextKey] =	dbo.PrepareTextData4(CAST([TextData] as varchar(550)))
where [TextKey] IS NULL
and ([ObjectName] IS NULL OR [ObjectName] IN ('sp_executesql'))
and [EventClass] IN (10, 12)
", this.TableName);
            this.RowCountPrepared += command.ExecuteNonQuery();
        }

		public void PrepareTextKeysSP()
		{
			var command = new SqlCommand();
			command.Connection = this.Connection;
			command.CommandTimeout = 10000;
			command.CommandText = string.Format(@"
-- Обработка для хранимых процедур
update TOP (100000) [dbo].[{0}]
set [TextKey] =	[ObjectName]
where [ObjectName] IS NOT NULL
and [TextKey] IS NULL
and [ObjectName] NOT IN  ('sp_executesql')
and EventClass in (10, 12)
", this.TableName);
			this.RowCountPreparedSP += command.ExecuteNonQuery();
//			command.CommandText = string.Format(@"
//-- Предварительная обработка для SQL-запросов
//update TOP (2000) [dbo].[{0}]
//set [TextKey] =	NULL
//where
//([ObjectName] IS NULL OR [ObjectName] IN ('sp_executesql'))
//and EventClass in (10, 12)
//", this.TableName);
//			this.RowCountPreparedSP += command.ExecuteNonQuery();
		}

        public void CreateIndexes()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandTimeout = 60 * 60;

            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_DatabaseName_EventClass_CPU]
ON [dbo].[{0}] ([DatabaseName],[EventClass])
INCLUDE ([CPU])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_DatabaseName_EventClass_StartTime]
ON [dbo].[{0}] ([DatabaseName],[EventClass])
INCLUDE ([StartTime])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_DatabaseName_EventClass_Reads]
ON [dbo].[{0}] ([DatabaseName],[EventClass])
INCLUDE ([Reads])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_DatabaseName_EventClass_Writes]
ON [dbo].[{0}] ([DatabaseName],[EventClass])
INCLUDE ([Writes])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_DatabaseName_EventClass_Duration]
ON [dbo].[{0}] ([DatabaseName],[EventClass])
INCLUDE ([Duration])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
--CREATE NONCLUSTERED INDEX [IX_TraceTable_DatabaseName_EventClass_DurationReadsWritesCPUTextKey]
--ON [dbo].[{0}] ([DatabaseName],[EventClass])
--INCLUDE ([Duration],[Reads],[Writes],[CPU],[TextKey])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_ObjectName]
ON [dbo].[{0}] ([ObjectName])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_EventClassDurationReadsWritesCPU]
ON [dbo].[{0}] ([EventClass])
INCLUDE ([Duration],[Reads],[Writes],[CPU])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }


            try
            {
                command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTable_EventClassDurationReadsWritesCPUObjectNameDatabaseName]
ON [dbo].[{0}] ([EventClass])
INCLUDE ([Duration],[Reads],[Writes],[CPU],[ObjectName],[DatabaseName])
", this.TableName);
                command.ExecuteNonQuery();
            }
            catch (Exception) { }

        }

        public void CreateTextKey()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandTimeout = 60 * 60;
            command.CommandText = string.Format(@"
BEGIN TRANSACTION
ALTER TABLE [dbo].[{0}] ADD
	[TextKey] varchar(1000) NULL
ALTER TABLE [dbo].[{0}] SET (LOCK_ESCALATION = TABLE)
COMMIT
", this.TableName);
            command.ExecuteNonQuery();
        }

        string[] GetTables()
        {
            string[] tables = {};
            if (this.Connection.State == System.Data.ConnectionState.Open)
            {
                var command = new SqlCommand();
                command.Connection = this.Connection;
                command.CommandText = @"select TABLE_NAME
from INFORMATION_SCHEMA.COLUMNS
where COLUMN_NAME = 'EventClass'
ORDER BY TABLE_NAME";
                var reader = command.ExecuteReader();
                var tablesList = new List<string>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tablesList.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                tables = tablesList.ToArray();
            }
            return tables;
        }

        public bool TableExist(string tableName)
        {
            bool isExist = false;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {
                var command = new SqlCommand();
                command.Connection = this.Connection;
				command.CommandTimeout = 60;
                command.CommandText = @"select TABLE_NAME
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = @tableName
ORDER BY TABLE_NAME";
                command.Parameters.Add(new SqlParameter("@tableName", System.Data.SqlDbType.VarChar, 100)
                    {
                        Value = tableName
                    }
                    );
                command.Prepare();
                var reader = command.ExecuteReader();
                isExist = reader.HasRows;
                reader.Close();
            }
            return isExist;
        }

        public int GetRowCountForPrepare()
        {
            int rowCount = -1;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {
                var command = new SqlCommand();
                command.Connection = this.Connection;
				command.CommandTimeout = 60 * 60;
                command.CommandText = string.Format(@"
select count(*)
from [dbo].[{0}]
where TextKey IS NULL
and (ObjectName IS NULL OR ObjectName IN ('sp_executesql'))
and EventClass in (10, 12)", this.TableName);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        rowCount = reader.GetInt32(0);
                    }
                }
                reader.Close();
            }
            return rowCount;            
        }

		public int GetRowCountForPrepareSP()
		{
			int rowCount = -1;

			if (this.Connection.State == System.Data.ConnectionState.Open)
			{
				var command = new SqlCommand();
				command.Connection = this.Connection;
				command.CommandTimeout = 60 * 60;
				command.CommandText = string.Format(@"
select count(*)
from [dbo].[{0}]
where [ObjectName] IS NOT NULL
and [TextKey] IS NULL
and [ObjectName] NOT IN  ('sp_executesql')
and EventClass in (10, 12)
", this.TableName);
				var reader = command.ExecuteReader();
				if (reader.HasRows)
				{
					if (reader.Read())
					{
						rowCount = reader.GetInt32(0);
					}
				}
				reader.Close();
			}
			return rowCount;
		}

        public void CreateDeadlockReport()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;
            command.CommandText = string.Format(@"
CREATE TABLE [dbo].[{0}](
	[RowNumber] [int] IDENTITY(0,1) NOT NULL,
	[EventClass] [int] NULL,
	[LoginName] [nvarchar](128) NULL,
	[SPID] [int] NULL,
	[StartTime] [datetime] NULL,
	[TextData] [ntext] NULL,
	[TransactionID] [bigint] NULL,
	[GroupID] [int] NULL,
	[BinaryData] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[RowNumber] ASC
)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
", TableNameDeadlock);
            command.ExecuteNonQuery();

            command.CommandText = string.Format(@"
INSERT INTO [dbo].[{1}]
([EventClass], [BinaryData])
VALUES
(65528, 
-- columns:
--     RowNumber
--     EventClass
--     LoginName
--     SPID
--     StartTime
--     TextData
--     TransactionID
--     GroupID
--     BinaryData
0xFFFE900209004D006900630072006F0073006F00660074002000530051004C00200053006500720076006500720000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000A324006000000004F0054004C004F0041004400530051004C0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000FAFBEDFB00FCFF0C94000B0004000C000E000100FCFF0652000C004200FCFB0E1B000B000C000E00010004004200
)

INSERT INTO [dbo].[{1}]
    ([EventClass]
    ,[LoginName]
    ,[SPID]
    ,[StartTime]
    ,[TextData]
    ,[TransactionID]
    ,[GroupID]
    ,[BinaryData])
SELECT [EventClass]
      ,[LoginName]
      ,[SPID]
      ,[StartTime]
      ,[TextData]
      ,[TransactionID]
      ,DATALENGTH ( [TextData] ) as [GroupID]
  FROM [dbo].[{0}]
  WHERE [EventClass] = 148
  ORDER BY [GroupID]
", TableName, TableNameDeadlock);
            command.ExecuteNonQuery();
        }


        public void CreateDetailReport()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
			command.CommandTimeout = 160 * 60;
            command.CommandText = string.Format(@"
declare @CPUSumm int; 
declare @DurationSumm float; 
declare @ReadsSumm float;
declare @WritesSumm float;
declare @CountSumm float;

select @CPUSumm = SUM(CPU)
     , @DurationSumm = SUM(Duration)
     , @ReadsSumm = SUM(Reads)
     , @WritesSumm = SUM(Writes)
     , @CountSumm = count(*)
from [dbo].[{0}] where EventClass in (10, 12)


select
	[DatabaseName]
	, LEFT([TextKey], 40) as [TextKey-key]
	, [avg(CPU)] as [avg(CPU)-key]
	, [avg(Duration)] as [avg(Duration)-key]
	, [% Duration] as [% Duration-key]
	, [avg(Reads)] as [avg(Reads)-key]
	, [Count] as [Count-key]

	, [TextKey]

	, [min(CPU)]
	, [avg(CPU)]
	, [max(CPU)]
	, [sum(CPU)]
	, [% CPU]

	, [min(Duration)]
	, [avg(Duration)]
	, [max(Duration)]
	, [sum(Duration)]
	, [% Duration]

	, [min(Reads)]
	, [avg(Reads)]
	, [max(Reads)]
	, [sum(Reads)]
	, [% Reads]

	, [min(Writes)]
	, [avg(Writes)]
	, [max(Writes)]
	, [sum(Writes)]
	, [% Writes]

	, [Count]
	, [% Count]

	, [TextData-min(Duration)]
	, [TextData-max(Duration)]
	, [TextData-min(Reads)]
	, [TextData-max(Reads)]
	, [TextData-min(CPU)]
	, [TextData-max(CPU)]
	, [TextData-min(Writes)]
	, [TextData-max(Writes)]

	, [min(Duration)raw]
	, [max(Duration)raw]

INTO [dbo].[{1}]
from
(
	select
		--Быстрая статистика, для вставки в отчёт по тестированию
		*
		, round(cast([sum(CPU)] as float) / @CPUSumm * 100, 3) as [% CPU]

		, [min(Duration)raw]/1000 as [min(Duration)]
		, [avg(Duration)raw]/1000 as [avg(Duration)]
		, [max(Duration)raw]/1000 as [max(Duration)] 
		, [sum(Duration)raw]/1000 as [sum(Duration)]

		, round(cast([sum(Duration)raw] as float) / @DurationSumm * 100, 3) as [% Duration]

		, round(cast([sum(Reads)] as float) / @ReadsSumm * 100, 3) as [% Reads]

		, round(cast([sum(Writes)] as float) / @WritesSumm * 100, 3) as [% Writes]

		, round([Count] / @CountSumm * 100, 3) as [% Count]

		, (select top 1 [TextData] from [{0}]) as [TextData-min(Duration)]
		, (select top 1 [TextData] from [{0}]) as [TextData-max(Duration)]
		, (select top 1 [TextData] from [{0}]) as [TextData-min(CPU)]
		, (select top 1 [TextData] from [{0}]) as [TextData-max(CPU)]
		, (select top 1 [TextData] from [{0}]) as [TextData-min(Reads)]
		, (select top 1 [TextData] from [{0}]) as [TextData-max(Reads)]
		, (select top 1 [TextData] from [{0}]) as [TextData-min(Writes)]
		, (select top 1 [TextData] from [{0}]) as [TextData-max(Writes)]

	from
	(
		select
			--Быстрая статистика, для вставки в отчёт по тестированию
			[DatabaseName],
			[TextKey],
  
			--Детальная статистика
			min(CPU) as [min(CPU)], 
			avg(CPU) as [avg(CPU)], 
			max(CPU) as [max(CPU)], 
			sum(CPU) as [sum(CPU)], 

			min(Duration) as [min(Duration)raw], 
			avg(Duration) as [avg(Duration)raw], 
			max(Duration) as [max(Duration)raw], 
			sum(Duration) as [sum(Duration)raw],

			min(Reads) as [min(Reads)], 
			avg(Reads) as [avg(Reads)],
			max(Reads) as [max(Reads)], 
			sum(Reads) as [sum(Reads)], 

			min(Writes) as [min(Writes)], 
			avg(Writes) as [avg(Writes)],
			max(Writes) as [max(Writes)], 
			sum(Writes) as [sum(Writes)], 

			count(*) as [Count]
		from
			[dbo].[{0}] as TTT -- Таблица, в которую сохранили трейс. 
		where
			EventClass in (10, 12)
		group by
			[DatabaseName], [TextKey]
	) as [Statistic]
) as [Statistic2]
order by [% Duration] desc
    ", this.TableName, this.TableNameDetail);
            command.ExecuteNonQuery();

			try
			{
				CreateIndexOnTextKeys();
			}
			catch (Exception ex)
			{
			}

			command.CommandText = string.Format(@"
CREATE NONCLUSTERED INDEX [IX_TraceTableDetailStat_TextKey_DatabaseName]
ON [dbo].[{0}] ([DatabaseName],[TextKey])
", this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-min(Duration)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and Duration = [dbo].[{1}].[min(Duration)raw])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-max(Duration)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and Duration = [dbo].[{1}].[max(Duration)raw])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-min(CPU)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and CPU = [dbo].[{1}].[min(CPU)])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-max(CPU)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and CPU = [dbo].[{1}].[max(CPU)])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-min(Reads)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and Reads = [dbo].[{1}].[min(Reads)])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-max(Reads)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and Reads = [dbo].[{1}].[max(Reads)])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-min(Writes)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and Writes = [dbo].[{1}].[min(Writes)])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();

			command.CommandText = string.Format(@"
UPDATE [dbo].[{1}] SET [TextData-max(Writes)] = 
(select top 1 [TextData] from [{0}] where [TextKey] = [dbo].[{1}].[TextKey] and [DatabaseName] = [dbo].[{1}].[DatabaseName] and Writes = [dbo].[{1}].[max(Writes)])
    ", this.TableName, this.TableNameDetail);
			command.ExecuteNonQuery();
		}

        public void CreateDraftReport()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandTimeout = 60 * 60;
            command.CommandText = string.Format(@"
declare @CPUSumm int; 
declare @DurationSumm float; 
declare @ReadsSumm float;
declare @WritesSumm float;
declare @CountSumm float;

select @CPUSumm = SUM(CPU)
     , @DurationSumm = SUM(Duration)
     , @ReadsSumm = SUM(Reads)
     , @WritesSumm = SUM(Writes)
     , @CountSumm = count(*)
from [dbo].[{0}] where EventClass in (10, 12)


select
	[DatabaseName]
	, [ObjectName] as [ObjectName-key]
	, [avg(CPU)] as [avg(CPU)-key]
	, [avg(Duration)] as [avg(Duration)-key]
	, [% Duration] as [% Duration-key]
	, [avg(Reads)] as [avg(Reads)-key]
	, [Count] as [Count-key]

	, [ObjectName]

	, [min(CPU)]
	, [avg(CPU)]
	, [max(CPU)]
	, [sum(CPU)]
	, [% CPU]

	, [min(Duration)]
	, [avg(Duration)]
	, [max(Duration)]
	, [sum(Duration)]
	, [% Duration]

	, [min(Reads)]
	, [avg(Reads)]
	, [max(Reads)]
	, [sum(Reads)]
	, [% Reads]

	, [min(Writes)]
	, [avg(Writes)]
	, [max(Writes)]
	, [sum(Writes)]
	, [% Writes]

	, [Count]
	, [% Count]

	, [TextData-min(Duration)]
	, [TextData-max(Duration)]
	, [TextData-min(Reads)]
	, [TextData-max(Reads)]
	, [TextData-min(CPU)]
	, [TextData-max(CPU)]
	, [TextData-min(Writes)]
	, [TextData-max(Writes)]
INTO [dbo].[{1}]
from
(
	select
		--Быстрая статистика, для вставки в отчёт по тестированию
		*
		, round(cast([sum(CPU)] as float) / @CPUSumm * 100, 3) as [% CPU]

		, [min(Duration)raw]/1000 as [min(Duration)]
		, [avg(Duration)raw]/1000 as [avg(Duration)]
		, [max(Duration)raw]/1000 as [max(Duration)] 
		, [sum(Duration)raw]/1000 as [sum(Duration)]
		, round(cast([sum(Duration)raw] as float) / @DurationSumm * 100, 3) as [% Duration]

		, round(cast([sum(Reads)] as float) / @ReadsSumm * 100, 3) as [% Reads]

		, round(cast([sum(Writes)] as float) / @WritesSumm * 100, 3) as [% Writes]

		, round([Count] / @CountSumm * 100, 3) as [% Count]

		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and Duration = [Statistic].[min(Duration)raw]) as [TextData-min(Duration)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and Duration = [Statistic].[max(Duration)raw]) as [TextData-max(Duration)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and CPU = [Statistic].[min(CPU)]) as [TextData-min(CPU)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and CPU = [Statistic].[max(CPU)]) as [TextData-max(CPU)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and Reads = [Statistic].[min(Reads)]) as [TextData-min(Reads)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and Reads = [Statistic].[max(Reads)]) as [TextData-max(Reads)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and Writes = [Statistic].[min(Writes)]) as [TextData-min(Writes)]
		,(select top 1 [TextData] from [{0}] where [ObjectName] = [Statistic].[ObjectName] and [DatabaseName] = [Statistic].[DatabaseName] and Writes = [Statistic].[max(Writes)]) as [TextData-max(Writes)]

	from
	(
		select
			--Быстрая статистика, для вставки в отчёт по тестированию
			[DatabaseName],
			[ObjectName],
  
			--Детальная статистика
			min(CPU) as [min(CPU)], 
			avg(CPU) as [avg(CPU)], 
			max(CPU) as [max(CPU)], 
			sum(CPU) as [sum(CPU)], 

			min(Duration) as [min(Duration)raw], 
			avg(Duration) as [avg(Duration)raw], 
			max(Duration) as [max(Duration)raw], 
			sum(Duration) as [sum(Duration)raw],

			min(Reads) as [min(Reads)], 
			avg(Reads) as [avg(Reads)],
			max(Reads) as [max(Reads)], 
			sum(Reads) as [sum(Reads)], 

			min(Writes) as [min(Writes)], 
			avg(Writes) as [avg(Writes)],
			max(Writes) as [max(Writes)], 
			sum(Writes) as [sum(Writes)], 

			count(*) as [Count]
		from
			[dbo].[{0}] as TTT -- Таблица, в которую сохранили трейс. 
		where
			EventClass in (10, 12)
		group by
			[DatabaseName], [ObjectName]
	) as [Statistic]
) as [Statistic2]
order by [% Duration] desc
    ", this.TableName, this.TableNameDraft);
            command.ExecuteNonQuery();
        }

        public void CreateErrorReport()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;
            command.CommandText = string.Format(@"
SELECT [DatabaseName], [Error], [ApplicationName], [ErrorText], count(*) as [Count], Max([StartTime]) as [StartTime]
INTO [dbo].[{1}]
FROM
(
	SELECT [DatabaseName], [Error], [ApplicationName], CAST([TextData] as varchar(max)) as [ErrorText], [StartTime]
	FROM [dbo].[{0}]
	WHERE EventClass = 162
) [Errors]
GROUP BY [DatabaseName], [Error], [ApplicationName], [ErrorText]
ORDER BY [DatabaseName], [Error], [ApplicationName], [ErrorText]
            ", this.TableName, this.TableNameError);
            command.ExecuteNonQuery();
        }

        public void CreateFunctionPrepareTextData()
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
            command.CommandTimeout = 60 * 60;
            command.CommandText = @"
CREATE FUNCTION [dbo].[PrepareTextData4]
(
	-- Add the parameters for the function here
	@textData varchar(2000)
)
RETURNS varchar(400)
AS
BEGIN
	
	if(@textData is NULL)
		return 'NULL'

	-- Declare the return variable here
	DECLARE @textKey NVARCHAR(2000)

	DECLARE @replaceTextIndex int

	-- Замена перевода строки, табуляции на пробел
	SET @textKey = 
	    Replace(Replace(Replace(
	    @textData,
	    char(9), ' '), char(10), ' '), char(13), ' ')

	SET @textKey = 
	    Replace(Replace(
	    @textKey
	   , 'varchar(max)', 'varchar(9)')
	   , 'varbinary(max)', 'varbinary(9)')

	-- Подготовка к обработке бинарных констант
	SET @textKey = 
		Replace(Replace(
		@textKey,
		'=0x', '={HEX0xFF*}'),'= 0x', '= {HEX0xFF*}')

	-- Подготовка к обработке числовых констант с ведёщим символом присваивания
	SET @textKey = 
	    Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
	    @textKey,
	    '=0', N'=№'), '=1', '=№'), '=2', '=№'), '=3', '=№'), '=4', N'=№'), '=5', N'=№'), '=6', N'№'), '=7', N'=№'), '=8', N'=№'), '=9', N'=№')

	-- Подготовка к обработке числовых констант
	SET @textKey = 
	    Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
	    @textKey,
	    ' 0', N' №'), ' 1', N' №'), ' 2', N' №'), ' 3', N' №'), ' 4', N' №'), ' 5', N' №'), ' 6', N' №'), ' 7', N' №'), ' 8', N' №'), ' 9', N' №')

	-- Подготовка к обработке чисел в скобках
	SET @textKey = 
	    Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
	    @textKey,
	    '(0', N'(№'), '(1', N'(№'), '(2', N'(№'), '(3', N'(№'), '(4', N'(№'), '(5', N'(№'), '(6', N'(№'), '(7', N'(№'), '(8', N'(№'), '(9', N'(№')

	-- Обработка числовых констант
	SET @replaceTextIndex = 1
	WHILE @replaceTextIndex > 0
	BEGIN
		SET @textKey=
			Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
			@textKey,
			N'№0', N'№'),	N'№1', N'№'),	N'№2', N'№'),	N'№3', N'№'),	N'№4', N'№'),	N'№5', N'№'),	N'№6', N'№'),	N'№7', N'№'),	N'№8', N'№'),	N'№9', N'№'),	N'№№', N'№')
		
		SET @replaceTextIndex = 
		    CHARINDEX(N'№0', @textKey)+
			CHARINDEX(N'№1', @textKey)+
			CHARINDEX(N'№2', @textKey)+
			CHARINDEX(N'№3', @textKey)+
			CHARINDEX(N'№4', @textKey)+
			CHARINDEX(N'№5', @textKey)+
			CHARINDEX(N'№6', @textKey)+
			CHARINDEX(N'№7', @textKey)+
			CHARINDEX(N'№8', @textKey)+
			CHARINDEX(N'№9', @textKey)+
			CHARINDEX(N'№№', @textKey)
	END

	-- Подготовка к обработке дробной части числовых констант
	SET @textKey = 
	    Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
	    @textKey,
	    N'№.0', N'№.№'), N'№.1', N'№.№'), N'№.2', N'№.№'), N'№.3', N'№.№'), N'№.4', N'№.№'), N'№.5', N'№.№'), N'№.6', N'№.№'), N'№.7', N'№.№'), N'№.8', N'№.№'), N'№.9', N'№.№')

	-- Обработка дробной части числовых коснтант
	SET @replaceTextIndex = 1
	WHILE @replaceTextIndex > 0
	BEGIN

		SET @textKey=
			Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
			@textKey,
			N'№0', N'№'),	N'№1', N'№'),	N'№2', N'№'),	N'№3', N'№'),	N'№4', N'№'),	N'№5', N'№'),	N'№6', N'№'),	N'№7', N'№'),	N'№8', N'№'),	N'№9', N'№'),	N'№№', N'№')
		
		SET @replaceTextIndex = 
		    CHARINDEX(N'№0', @textKey)+
			CHARINDEX(N'№1', @textKey)+
			CHARINDEX(N'№2', @textKey)+
			CHARINDEX(N'№3', @textKey)+
			CHARINDEX(N'№4', @textKey)+
			CHARINDEX(N'№5', @textKey)+
			CHARINDEX(N'№6', @textKey)+
			CHARINDEX(N'№7', @textKey)+
			CHARINDEX(N'№8', @textKey)+
			CHARINDEX(N'№9', @textKey)+
			CHARINDEX(N'№№', @textKey)
	END

	SET @textKey=Replace(@textKey, N'№', '9')

	-- Обработка бинарных констант
	SET @replaceTextIndex = 1
	WHILE @replaceTextIndex > 0
	BEGIN

		SET @textKey=
			Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(Replace(
			@textKey,
			'{HEX0xFF*}0', '{HEX0xFF*}'),
			'{HEX0xFF*}1', '{HEX0xFF*}'),
			'{HEX0xFF*}2', '{HEX0xFF*}'),
			'{HEX0xFF*}3', '{HEX0xFF*}'),
			'{HEX0xFF*}4', '{HEX0xFF*}'),
			'{HEX0xFF*}5', '{HEX0xFF*}'),
			'{HEX0xFF*}6', '{HEX0xFF*}'),
			'{HEX0xFF*}7', '{HEX0xFF*}'),
			'{HEX0xFF*}8', '{HEX0xFF*}'),
			'{HEX0xFF*}9', '{HEX0xFF*}'),
			'{HEX0xFF*}A', '{HEX0xFF*}'),
			'{HEX0xFF*}B', '{HEX0xFF*}'),
			'{HEX0xFF*}C', '{HEX0xFF*}'),
			'{HEX0xFF*}D', '{HEX0xFF*}'),
			'{HEX0xFF*}E', '{HEX0xFF*}'),
			'{HEX0xFF*}F', '{HEX0xFF*}')
		
		SET @replaceTextIndex = 
			CHARINDEX('{HEX0xFF*}0', @textKey)+
			CHARINDEX('{HEX0xFF*}1', @textKey)+
			CHARINDEX('{HEX0xFF*}2', @textKey)+
		    CHARINDEX('{HEX0xFF*}3', @textKey)+
			CHARINDEX('{HEX0xFF*}4', @textKey)+
			CHARINDEX('{HEX0xFF*}5', @textKey)+
			CHARINDEX('{HEX0xFF*}6', @textKey)+
			CHARINDEX('{HEX0xFF*}7', @textKey)+
			CHARINDEX('{HEX0xFF*}8', @textKey)+
			CHARINDEX('{HEX0xFF*}9', @textKey)+
			CHARINDEX('{HEX0xFF*}A', @textKey)+
			CHARINDEX('{HEX0xFF*}B', @textKey)+
			CHARINDEX('{HEX0xFF*}C', @textKey)+
			CHARINDEX('{HEX0xFF*}D', @textKey)+
			CHARINDEX('{HEX0xFF*}E', @textKey)+
			CHARINDEX('{HEX0xFF*}F', @textKey)
	END
	set @textKey = Replace(@textKey, '{HEX0xFF*}', '0xFF')

	DECLARE @startPosition int
	DECLARE @startPositionTemp int
	DECLARE @endPosition int
	DECLARE @searchPatternStart varchar(10)
	DECLARE @searchPatternEnd varchar(10)
	DECLARE @isFound bit
	DECLARE @replaceStr varchar(10)

	set @replaceStr = '*'

	set @textKey = Replace(@textKey, '= ''', '=''')
	set @textKey = Replace(@textKey, '= N''', '=N''')

	-- Обработка строковых констант с ведущим символом присваивания
	set @endPosition = -1
	set @isFound = 1
	set @searchPatternStart = '='''
	set @searchPatternEnd = ''''
	WHILE @isFound = 1
	BEGIN
		set @isFound = 0
		set @startPositionTemp = CHARINDEX(@searchPatternStart, @textKey, @endPosition+1)
		IF @startPositionTemp > 0
		BEGIN
			set @startPosition = @startPositionTemp
			set @endPosition = CHARINDEX(@searchPatternEnd, @textKey, @startPosition + len(@searchPatternStart))
			IF @endPosition = 0
			BEGIN
				set @endPosition = LEN(@textKey)+1
			END

			set @isFound = 1
			set @textKey = LEFT(@textKey, @startPosition+len(@searchPatternStart)-1) + @replaceStr + RIGHT(@textKey, len(@textKey)-@endPosition+1)
			set @endPosition = @startPosition + len(@searchPatternStart) + len(@replaceStr) + len(@searchPatternEnd)

		END
	END

	-- Обоработка строковых unicode-констант с ведущим символом присваивания
	set @endPosition = -1
	set @isFound = 1
	set @searchPatternStart = '=N'''
	set @searchPatternEnd = ''''
	WHILE @isFound = 1
	BEGIN
		set @isFound = 0
		set @startPositionTemp = CHARINDEX(@searchPatternStart, @textKey, @endPosition+1)
		IF @startPositionTemp > 0
		BEGIN
			set @startPosition = @startPositionTemp
			set @endPosition = CHARINDEX(@searchPatternEnd, @textKey, @startPosition + len(@searchPatternStart))
			IF @endPosition = 0
			BEGIN
				set @endPosition = LEN(@textKey)+1
			END

			set @isFound = 1
			set @textKey = LEFT(@textKey, @startPosition+len(@searchPatternStart)-1) + @replaceStr + RIGHT(@textKey, len(@textKey)-@endPosition+1)
			set @endPosition = @startPosition + len(@searchPatternStart) + len(@replaceStr) + len(@searchPatternEnd)
		END
	END

	-- Обработка строковых констант с ведёщим пробелом, которые вложены в строковых константы
	set @endPosition = -1
	set @isFound = 1
	set @searchPatternStart = ' '''''
	set @searchPatternEnd = ''''''
	WHILE @isFound = 1
	BEGIN
		set @isFound = 0
		set @startPositionTemp = CHARINDEX(@searchPatternStart, @textKey, @endPosition+1)
		IF @startPositionTemp > 0
		BEGIN
			set @startPosition = @startPositionTemp
			set @endPosition = CHARINDEX(@searchPatternEnd, @textKey, @startPosition + len(@searchPatternStart))
			IF @endPosition = 0
			BEGIN
				set @endPosition = LEN(@textKey)+1
			END

			set @isFound = 1
			set @textKey = LEFT(@textKey, @startPosition+len(@searchPatternStart)-1) + @replaceStr + RIGHT(@textKey, len(@textKey)-@endPosition+1)
			set @endPosition = @startPosition + len(@searchPatternStart) + len(@replaceStr) + len(@searchPatternEnd)
		END
	END

	SET @textKey = 
	    Replace(
	    @textKey,
	    ' ', '')

	SET @textKey = LEFT(@textKey, 400)
	if RIGHT ( @textKey , 1 ) = '*'
	BEGIN
		SET @textKey = @textKey + ''''
	END

	RETURN @textKey
END

            ";
            command.ExecuteNonQuery();
        }

        public bool ColumnExistInTable(string tableName, string columnName = "TextKey")
        {
            var command = new SqlCommand();
            command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;

            command.CommandText = @"
select column_name, TABLE_CATALOG
from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = @tableName
and COLUMN_NAME = @columnName";
            command.Parameters.Clear();
            command.Parameters.Add( new SqlParameter("@tableName", System.Data.SqlDbType.VarChar, 100)
            {
                Value = tableName
            });
            command.Parameters.Add(new SqlParameter("@database", System.Data.SqlDbType.VarChar, 100)
            {
                Value = this.DataBase
            });
            command.Parameters.Add(new SqlParameter("@columnName", System.Data.SqlDbType.VarChar, 100)
            {
                Value = columnName
            });
            command.Prepare();
            var reader = command.ExecuteReader();

            var isExist = reader.HasRows;
            reader.Close();
            return isExist;
        }

		public void CreateMinuteAndSecondColumn()
		{
			var command = new SqlCommand();
			command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;
			command.CommandText = string.Format(@"
BEGIN TRANSACTION
ALTER TABLE [dbo].[{0}] ADD
	[Second01] int NULL,
	[Second05] int NULL,
	[Second10] int NULL,
	[Munute01] int NULL,
	[Munute02] int NULL,
	[Munute03] int NULL,
	[Munute04] int NULL,
	[Munute05] int NULL
ALTER TABLE [dbo].[{0}] SET (LOCK_ESCALATION = TABLE)
COMMIT
", this.TableName);
			command.ExecuteNonQuery();
		}

		public void FillMinuteAndSecondColumn()
		{
			var command = new SqlCommand();
			command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;
			command.CommandText = string.Format(@"
declare @minStartDate datetime
select @minStartDate = min([StartTime])
from [dbo].[{0}]
where EventClass in (10, 12)

update [dbo].[{0}]
set [Second01] = datediff(ss, @minStartDate, [StartTime]),
    [Munute01] = datediff(mi, @minStartDate, [StartTime])
where [EventClass] in (10, 12)

update [dbo].[{0}]
set [Second05] =  5 * ROUND([Second01] /  5, 0),
	[Second10] = 10 * ROUND([Second01] / 10, 0),
    [Munute02] =  2 * ROUND([Munute01] /  2, 0),
    [Munute03] =  3 * ROUND([Munute01] /  3, 0),
    [Munute04] =  4 * ROUND([Munute01] /  4, 0),
    [Munute05] =  5 * ROUND([Munute01] /  5, 0)
where [EventClass] in (10, 12)
", this.TableName);
			command.ExecuteNonQuery();
		}

		string GetTextData(string databaseName, string textKey, string fildName)
		{
			string result = null;
			var command = new SqlCommand();
			command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;
			command.CommandText = string.Format(@"
SELECT [{0}]
  FROM [dbo].[{1}]
 WHERE [DatabaseName] = @databaseName AND [TextKey-key] = @textKey
", fildName, this.TableNameDetail);
			command.Parameters.Clear();
			command.Parameters.Add(new SqlParameter("@databaseName", System.Data.SqlDbType.VarChar, 100)
			{
				Value = databaseName
			});
			command.Parameters.Add(new SqlParameter("@textKey", System.Data.SqlDbType.VarChar, 100)
			{
				Value = textKey
			});
			command.Prepare();
			SqlDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				if (reader.Read())
				{
					result = reader.GetStringOrNull(0);
				}
			}
			reader.Close();
			TrySaveText(databaseName, textKey, fildName, result);
			return result;
		}

		void TrySaveText(string databaseName, string textKey, string field, string text)
		{
			try
			{
				if (!System.IO.Directory.Exists(databaseName))
				{
					System.IO.Directory.CreateDirectory(databaseName);
				}
				string safeTextKey = textKey;
				foreach (char ch in System.IO.Path.GetInvalidPathChars())
				{
					safeTextKey = safeTextKey.Replace(ch, '_');
				}
				char[] expectedChars = { '\\', '/', ':', '*', '?', '<', '>', '|' };
				foreach (char ch in expectedChars)
				{
					safeTextKey = safeTextKey.Replace(ch, '_');
				}

				string path = databaseName + "\\" + safeTextKey;
				if (!System.IO.Directory.Exists(path))
				{
					System.IO.Directory.CreateDirectory(path);
				}
				System.IO.File.WriteAllText(databaseName + "\\" + safeTextKey + "\\" + field + ".sql", text);
			}
			catch
			{
			}
		}

		public Model.DetailStat FillDetailStat(Model.DetailStat node)
		{
			node.TextDataMinDuration = node.TextDataMinDuration ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-min(Duration)");
			node.TextDataMaxDuration = node.TextDataMaxDuration ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-max(Duration)");
			node.TextDataMinReads = node.TextDataMinReads ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-min(Reads)");
			node.TextDataMaxReads = node.TextDataMaxReads ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-max(Reads)");
			node.TextDataMinCPU = node.TextDataMinCPU ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-min(CPU)");
			node.TextDataMaxCPU = node.TextDataMaxCPU ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-max(CPU)");
			node.TextDataMinWrites = node.TextDataMinWrites ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-min(Writes)");
			node.TextDataMaxWrites = node.TextDataMaxWrites ?? GetTextData(node.DatabaseName, node.TextKeyKey, "TextData-max(Writes)");
			return node;
		}

		public ReadOnlyCollection<Model.DetailStat> GetDetailStat()
		{
			ReadOnlyCollection<Model.DetailStat> result = null;

			var command = new SqlCommand();
			command.Connection = this.Connection;
			command.CommandTimeout = 60 * 60;
			command.CommandText = string.Format(@"
SELECT [DatabaseName]
      ,[TextKey-key]
      ,[avg(CPU)-key]
      ,[avg(Duration)-key]
      ,[% Duration-key]
      ,[avg(Reads)-key]
      ,[Count-key]
      ,[TextKey]
      ,[min(CPU)]
      ,[avg(CPU)]
      ,[max(CPU)]
      ,[sum(CPU)]
      ,[% CPU]
      ,[min(Duration)]
      ,[avg(Duration)]
      ,[max(Duration)]
      ,[sum(Duration)]
      ,[% Duration]
      ,[min(Reads)]
      ,[avg(Reads)]
      ,[max(Reads)]
      ,[sum(Reads)]
      ,[% Reads]
      ,[min(Writes)]
      ,[avg(Writes)]
      ,[max(Writes)]
      ,[sum(Writes)]
      ,[% Writes]
      ,[Count]
      ,[% Count]
      --,[TextData-min(Duration)]
      --,[TextData-max(Duration)]
      --,[TextData-min(Reads)]
      --,[TextData-max(Reads)]
      --,[TextData-min(CPU)]
      --,[TextData-max(CPU)]
      --,[TextData-min(Writes)]
      --,[TextData-max(Writes)]
      ,[min(Duration)raw]
      ,[max(Duration)raw]
  FROM [dbo].[{0}]
", this.TableNameDetail);
			SqlDataReader reader = command.ExecuteReader();
			if (reader.HasRows)
			{
				List<Model.DetailStat> listData = new List<Model.DetailStat>();
				while (reader.Read())
				{
					Model.DetailStat data = new Model.DetailStat();
					int index = 0;
					data.DatabaseName = reader.GetStringOrNull(index); index++;
					data.TextKeyKey = reader.GetStringOrNull(index); index++;
					data.AvgCPUKey = reader.GetIntOrNull(index); index++;
					data.AvgDurationKey = reader.GetLongOrNull(index); index++;
					data.PercentDurationKey = reader.GetDoubleOrNull(index); index++;
					data.AvgReadsKey = reader.GetLongOrNull(index); index++;
					data.CountKey = reader.GetIntOrNull(index); index++;
					data.TextKey = reader.GetStringOrNull(index); index++;

					data.MinCPU = reader.GetIntOrNull(index); index++;
					data.AvgCPU = reader.GetIntOrNull(index); index++;
					data.MaxCPU = reader.GetIntOrNull(index); index++;
					data.SumCPU = reader.GetIntOrNull(index); index++;
					data.PercentCPU = reader.GetDoubleOrNull(index); index++;

					data.MinDuration = reader.GetLongOrNull(index); index++;
					data.AvgDuration = reader.GetLongOrNull(index); index++;
					data.MaxDuration = reader.GetLongOrNull(index); index++;
					data.SumDuration = reader.GetLongOrNull(index); index++;
					data.PercentDuration = reader.GetDoubleOrNull(index); index++;

					data.MinReads = reader.GetLongOrNull(index); index++;
					data.AvgReads = reader.GetLongOrNull(index); index++;
					data.MaxReads = reader.GetLongOrNull(index); index++;
					data.SumReads = reader.GetLongOrNull(index); index++;
					data.PercentReads = reader.GetDoubleOrNull(index); index++;

					data.MinWrites = reader.GetLongOrNull(index); index++;
					data.AvgWrites = reader.GetLongOrNull(index); index++;
					data.MaxWrites = reader.GetLongOrNull(index); index++;
					data.SumWrites = reader.GetLongOrNull(index); index++;
					data.PercentWrites = reader.GetDoubleOrNull(index); index++;

					data.Count = reader.GetIntOrNull(index); index++;
					data.PercentCount = reader.GetDoubleOrNull(index); index++;

					data.TextDataMinDuration = null; // reader.GetStringOrNull(index); index++;
					data.TextDataMaxDuration = null; // reader.GetStringOrNull(index); index++;

					data.TextDataMinReads = null; // reader.GetStringOrNull(index); index++;
					data.TextDataMaxReads = null; // reader.GetStringOrNull(index); index++;

					data.TextDataMinCPU = null; // reader.GetStringOrNull(index); index++;
					data.TextDataMaxCPU = null; // reader.GetStringOrNull(index); index++;

					data.TextDataMinWrites = null; // reader.GetStringOrNull(index); index++;
					data.TextDataMaxWrites = null; // reader.GetStringOrNull(index); index++;

					data.MinDurationRaw = reader.GetLongOrNull(index); index++;
					data.MaxDurationRaw = reader.GetLongOrNull(index); index++;

					listData.Add(data);
				}
				result = listData.AsReadOnly();
			}
			reader.Close();
			return result;
		}

        /// <summary>
        /// Проверка наличия в базе данных функции с указанным именем.
        /// </summary>
        /// <param name="functionName">Имя функции</param>
        /// <returns>В случае наличия соединения с базой данных и наличия в ней функции возвращается true. Иначе возвращается false.</returns>
        /// <seealso cref="http://stackoverflow.com/questions/15420235/sql-list-of-all-the-user-defined-functions-in-a-database"/>
        public bool FunctionExists(string functionName)
        {
            bool isExist = false;

            if (this.Connection.State == System.Data.ConnectionState.Open)
            {
                var command = new SqlCommand();
                command.Connection = this.Connection;
                command.CommandTimeout = 60;
                command.CommandText = @"select o.name, m.definition, o.type_desc
FROM sys.sql_modules m 
INNER JOIN sys.objects o ON m.object_id=o.object_id
WHERE o.type = 'FN' and name=@functionName";
                command.Parameters.Add(new SqlParameter("@functionName", System.Data.SqlDbType.NVarChar, 128)
                    {
                        Value = functionName
                    }
                    );
                command.Prepare();
                var reader = command.ExecuteReader();
                isExist = reader.HasRows;
                reader.Close();
            }
            return isExist;
        }


    }
}
