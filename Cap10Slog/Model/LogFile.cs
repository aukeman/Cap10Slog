using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cap10Slog.Model
{
    public class LogFile
    {
        public string FilePath
        {
            get;
            set;
        }

        public List<LogThread> Threads
        {
            get;
            set;
        }
    }
}
