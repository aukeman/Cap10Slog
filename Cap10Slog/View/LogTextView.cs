using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cap10Slog.Model;

namespace Cap10Slog.View
{
    public partial class LogTextView : UserControl
    {
        public LogTextView()
        {
            InitializeComponent();
        }

        public LogTimelineView TimelineView
        {
            get;
            set;
        }

        private LogFileCollection logFileCollection;
        public LogFileCollection LogFileCollection
        {
            get { return this.logFileCollection; }
            set
            {
                this.logFileCollection = value;

                if (this.logFileCollection != null &&
                    0 < this.logFileCollection.LogRecords.Length)
                {
//                    this.richTextBox.Text = String.Join("", this.logFileCollection.LogRecords.Select<LogRecord, string>(lr => lr.Raw).ToArray());


                    

                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource =
                        this.logFileCollection.LogRecords.Where<LogRecord>(lr => !this.LogFileCollection.GetLogThread(lr.ThreadID).Filtered).Select<LogRecord, LogRecordAdapter>(lr => new LogRecordAdapter(lr));

                    this.dataGridView.DataSource = bindingSource;

//                    this.dataGridView.DataSource = 
//                        this.logFileCollection.LogRecords.Where<LogRecord>(lr => !this.LogFileCollection.GetLogThread(lr.ThreadID).Filtered).Select<LogRecord, string>(lr => lr.Raw).ToArray();

                    //foreach (LogRecord lr in this.logFileCollection.LogRecords.Where<LogRecord>(lr => !this.LogFileCollection.GetLogThread(lr.ThreadID).Filtered))
                    //{
                    //    this.dataGridView.Rows.Add(lr.Raw);
                    //}

                    //this.dataGridView.Rows.AddRange()
                    //    .Select<LogRecord, string>(lr => lr.Raw).ToArray());

                }
                else
                {
                    this.dataGridView.DataSource = new object[0];
                }

                

                this.Refresh();
            }
        }

        private class LogRecordAdapter
        {
            private LogRecord logRecord = null;

            public LogRecordAdapter(LogRecord logRecord)
            {
                this.logRecord = logRecord;
            }

            public string Time
            {
                get { return logRecord.Time.ToString(ViewUtilities.DateTimeFormat);  }
            }

            public string Thread
            {
                get { return logRecord.ThreadID; }
            }

            public string Data
            {
                get { return logRecord.Data; }
            }
        }

    }
}
;