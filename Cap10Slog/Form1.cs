using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cap10Slog.Parser;
using Cap10Slog.Model;

namespace Cap10Slog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.logTimelineView.ThreadFilterChanged += this.logTimelineView_FilterChanged;
            this.logTimelineView.LogRecordSelected += this.logTimelineView_RecordSelected;

            this.logTextView.TimelineView = this.logTimelineView;
        }

        private void logTimelineView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void logTimelineView1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                LogFile[] logFiles =
                    Array.ConvertAll<string, LogFile>(
                        e.Data.GetData(DataFormats.FileDrop) as string[],
                        filepath => MainOutputParser.Parse(filepath));

                logTimelineView.LogFileCollection = new LogFileCollection(logFiles);

                logTextView.LogFileCollection = logTimelineView.LogFileCollection;
            }
        }

        private void logTimelineView_FilterChanged(object sender, EventArgs e)
        {
            this.logTextView.LogFileCollection = logTimelineView.LogFileCollection;
        }

        private void logTimelineView_RecordSelected(object sender, EventArgs e)
        {
            if ( this.logTimelineView.LogRecordUnderCursor != null )
            {
                this.logTextView.SelectLogRecord(this.logTimelineView.LogRecordUnderCursor);
            }
        }

    }
}
