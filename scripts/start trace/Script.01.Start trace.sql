-- Параметры скрипта:
--     @traceDuration, Int, "Длительность"
--         Значение берётся из профиля нагрузочного теста, при старте тестирования.
--     @fileName, nvarchar(256), "Сохранять трейс в файл"
--         Значение формируется на основе папки профайлинга, названия нагрузочного теста и текущего времени
--         C#: string.Format(@"{0}\{1}.Trace.StartOn ", profilingPath, loadTestName) + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss")
--     @db1, nvarchar(256), фильтровать по имени базы данных
--     @db2, nvarchar(256), фильтровать по имени базы данных
--     @db3, nvarchar(256), фильтровать по имени базы данных
--     @db4, nvarchar(256), фильтровать по имени базы данных

-- Create a Queue
declare @rc int
declare @TraceID int
declare @maxfilesize bigint
declare @stopTraceTime datetime
declare @traceFileName nvarchar(256)
declare @traceOptions int

-- Create 100 MBytes files
set @maxfilesize = 100
-- Duration of trace
set @stopTraceTime = DATEADD(second, @traceDuration, SYSDATETIME())
-- Trace filename
set @traceFileName = @fileName 
-- TRACE_FILE_ROLLOVER
set @traceOptions = 2

exec @rc = sp_trace_create @TraceID output, @traceOptions, @traceFileName, @maxfilesize, @stopTraceTime
if (@rc != 0) goto error

-- Set the events
declare @on bit
set @on = 1

-- 162. User Error Message. Displays error messages that users see in the case of an error or exception.
exec sp_trace_setevent @TraceID, 162, 1, @on  -- TextData. ntext.
exec sp_trace_setevent @TraceID, 162, 4, @on  -- TransactionID. bigint.
exec sp_trace_setevent @TraceID, 162, 9, @on  -- ClientProcessID. int.
exec sp_trace_setevent @TraceID, 162, 10, @on -- ApplicationName. nvarchar.
exec sp_trace_setevent @TraceID, 162, 11, @on -- LoginName. nvarchar.
exec sp_trace_setevent @TraceID, 162, 12, @on -- SPID. int.
exec sp_trace_setevent @TraceID, 162, 20, @on -- Severity.
exec sp_trace_setevent @TraceID, 162, 14, @on -- StartTime. datetime.
exec sp_trace_setevent @TraceID, 162, 31, @on -- Error. int.
exec sp_trace_setevent @TraceID, 162, 35, @on -- DatabaseName. nvarchar.
exec sp_trace_setevent @TraceID, 162, 49, @on -- RequestID. int.
exec sp_trace_setevent @TraceID, 162, 50, @on -- XactSequence. bigint.

-- 148. Deadlock Graph. Occurs when an attempt to acquire a lock is canceled because the attempt was part of a deadlock and was chosen as the deadlock victim. Provides an XML description of a deadlock.
exec sp_trace_setevent @TraceID, 148, 1, @on  -- TextData. ntext.
exec sp_trace_setevent @TraceID, 148, 4, @on  -- TransactionID. bigint. Not used.
exec sp_trace_setevent @TraceID, 148, 11, @on -- LoginName. nvarchar.
exec sp_trace_setevent @TraceID, 148, 12, @on -- SPID. int.
exec sp_trace_setevent @TraceID, 148, 14, @on -- StartTime. datetime.

-- 10. RPC:Completed. Occurs when a remote procedure call (RPC) has completed.
exec sp_trace_setevent @TraceID, 10, 1, @on   -- TextData. ntext.
exec sp_trace_setevent @TraceID, 10, 4, @on   -- TransactionID. bigint.
exec sp_trace_setevent @TraceID, 10, 9, @on   -- ClientProcessID. int.
exec sp_trace_setevent @TraceID, 10, 10, @on  -- ApplicationName. nvarchar.
exec sp_trace_setevent @TraceID, 10, 11, @on  -- LoginName. nvarchar.
exec sp_trace_setevent @TraceID, 10, 12, @on  -- SPID. int.
exec sp_trace_setevent @TraceID, 10, 13, @on  -- Duration. bigint.
exec sp_trace_setevent @TraceID, 10, 14, @on  -- StartTime. datetime.
exec sp_trace_setevent @TraceID, 10, 15, @on  -- EndTime. datetime.
exec sp_trace_setevent @TraceID, 10, 16, @on  -- Reads. bigint.
exec sp_trace_setevent @TraceID, 10, 17, @on  -- Writes. bigint.
exec sp_trace_setevent @TraceID, 10, 18, @on  -- CPU. int.
exec sp_trace_setevent @TraceID, 10, 31, @on  -- Error. int.
exec sp_trace_setevent @TraceID, 10, 34, @on  -- ObjectName. nvarchar.
exec sp_trace_setevent @TraceID, 10, 35, @on  -- DatabaseName. nvarchar.
exec sp_trace_setevent @TraceID, 10, 48, @on  -- RowCounts. bigint.
exec sp_trace_setevent @TraceID, 10, 49, @on  -- RequestID. int.
exec sp_trace_setevent @TraceID, 10, 50, @on  -- XactSequence. bigint.

-- 12. SQL:BatchCompleted. Occurs when a Transact-SQL batch has completed.
exec sp_trace_setevent @TraceID, 12, 1, @on   -- TextData. ntext.
exec sp_trace_setevent @TraceID, 12, 4, @on   -- TransactionID. bigint.
exec sp_trace_setevent @TraceID, 12, 9, @on   -- ClientProcessID. int.
exec sp_trace_setevent @TraceID, 12, 11, @on  -- LoginName. nvarchar.
exec sp_trace_setevent @TraceID, 12, 10, @on  -- ApplicationName. nvarchar.
exec sp_trace_setevent @TraceID, 12, 12, @on  -- SPID. int.
exec sp_trace_setevent @TraceID, 12, 13, @on  -- Duration. bigint.
exec sp_trace_setevent @TraceID, 12, 14, @on  -- StartTime. datetime.
exec sp_trace_setevent @TraceID, 12, 15, @on  -- EndTime. datetime.
exec sp_trace_setevent @TraceID, 12, 16, @on  -- Reads. bigint.
exec sp_trace_setevent @TraceID, 12, 17, @on  -- Writes. bigint.
exec sp_trace_setevent @TraceID, 12, 18, @on  -- CPU. int.
exec sp_trace_setevent @TraceID, 12, 31, @on  -- Error. int.
exec sp_trace_setevent @TraceID, 12, 35, @on  -- DatabaseName. nvarchar.
exec sp_trace_setevent @TraceID, 12, 48, @on  -- RowCounts. bigint.
exec sp_trace_setevent @TraceID, 12, 49, @on  -- RequestID. int.
exec sp_trace_setevent @TraceID, 12, 50, @on  -- XactSequence. bigint.


-- Set the Filters
exec sp_trace_setfilter @TraceID, 35, 0, 6, @db1
exec sp_trace_setfilter @TraceID, 35, 1, 6, @db2
exec sp_trace_setfilter @TraceID, 35, 1, 6, @db3
exec sp_trace_setfilter @TraceID, 35, 1, 6, @db4
-- Set the trace status to start
exec sp_trace_setstatus @TraceID, 1

-- display trace id for future references
select TraceID=@TraceID
goto finish

error: 
select ErrorCode=@rc

finish: 