using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Slog.Model
{
    public class LogFileCollection
    {
        public LogFileCollection(LogFile[] logFiles)
        {
            var threads = new List<LogThread>();
            var records = new List<LogRecord>();

            foreach (LogFile logFile in logFiles)
            { 
                foreach (LogThread importedLogThread in logFile.Threads)
                {
                    LogThread logThread = threads.Find( lt => (lt.ThreadID == importedLogThread.ThreadID) );

                    if (logThread == null)
                    {
                        threads.Add(importedLogThread);
                    }
                    else
                    {
                        logThread.LogRecords.AddRange(importedLogThread.LogRecords);
                    }

                    records.AddRange(importedLogThread.LogRecords);
                }
            }

            this.logThreads = threads.ToArray();
            this.logRecords = records.ToArray();

            foreach (LogThread logThread in this.logThreads)
            {
                logThread.LogRecords.Sort( LogRecord.CompareByAscendingTime );
            }

            Array.Sort(this.logRecords, LogRecord.CompareByAscendingTime );
        }

        private LogThread[] logThreads = new LogThread[0];
        public LogThread[] LogThreads
        {
            get { return this.logThreads; }
        }

        private LogRecord[] logRecords = new LogRecord[0];
        public LogRecord[] LogRecords
        {
            get { return this.logRecords; }
        }

        public DateTime EarliestTime
        {
            get
            {
                if (0 < this.LogRecords.Length)
                {
                    return this.LogRecords[0].Time;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public DateTime LatestTime
        {
            get
            {
                if ( 0 < this.LogRecords.Length)
                {
                    return this.LogRecords[this.LogRecords.Length - 1].Time;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }

        public LogThread GetLogThread(string threadID)
        {
            return this.logThreads.Single<LogThread>(lr => lr.ThreadID == threadID);
        }
    }
}
 