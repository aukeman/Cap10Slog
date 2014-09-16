using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Cap10Slog.View.LogRecordIcon;

namespace Cap10Slog.Model
{
    public class LogThread
    {
        private static ILogRecordIcon filteredIcon = new Dot(Color.LightGray);

        public LogThread()
        {
            this.icon = LogRecordIconFactory.GetNext();
            this.Filtered = false;
        }
        
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

        private ILogRecordIcon icon;
        public ILogRecordIcon Icon
        {
            get
            {
                return (this.Filtered ? filteredIcon : icon);
            }
        }

        public bool Filtered
        {
            get;
            set;
        }
    }
}
