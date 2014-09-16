using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Slog.Model
{
    public class LogRecord
    {
        public static Comparison<LogRecord> CompareByAscendingTime
        {
            get { return (a, b) => a.Time.CompareTo(b.Time); }
        }

        public LogRecord(string raw, DateTime time, int dataStartIdx, int dataLength, string filename, int lineNumber, string threadID)
        {
            this.Raw = raw;
            this.Time = time;
            this.DataStartIdx = dataStartIdx;
            this.DataLength = dataLength;
            this.Filename = filename;
            this.LineNumber = lineNumber;
            this.ThreadID = threadID;
        }

        public DateTime Time
        {
            get;
            private set;
        }

        public string Data
        {
            get
            {
                return this.Raw.Substring(this.DataStartIdx, this.DataLength);
            }
        }

        public string Raw
        {
            get;
            private set;
        }

        public string ThreadID
        {
            get;
            private set;
        }

        public string Filename
        {
            get;
            private set;
        }

        public int LineNumber
        {
            get;
            private set;
        }

        private int DataStartIdx
        {
            get;
            set;
        }

        private int DataLength
        {
            get;
            set;
        }
    }
}
