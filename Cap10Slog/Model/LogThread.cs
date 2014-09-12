using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Slog.Model
{
    public class LogThread
    {
        public string ThreadID
        {
            get;
            set;
        }

        public List<LogRecord> LogRecords
        {
            get;
            set;
        }
    }
}
