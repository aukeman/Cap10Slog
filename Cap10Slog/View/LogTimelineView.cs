using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cap10Slog.View.LogRecordIcon;

using Cap10Slog.Model;

namespace Cap10Slog.View
{
    public partial class LogTimelineView : UserControl
    {

        public EventHandler ThreadFilterChanged;

        private string toolTipText = "";
        public string ToolTipText
        {
            get
            {
                return this.toolTipText;
            }
            set
            {
                if ( value != this.toolTipText )
                {
                    this.toolTipText = value;
                    this.toolTip.SetToolTip(this.panel, this.toolTipText);
                }
            }
        }

        public DateTime EarliestTimeAvailable
        {
            get;
            private set;
        }

        public DateTime LatestTimeAvailable
        {
            get;
            private set;
        }

        public DateTime EarliestTimeRendered
        {
            get;
            private set;
        }

        public DateTime LatestTimeRendered
        {
            get;
            private set;
        }

        public SizeF TimeLabelSize
        {
            get;
            private set;
        }

        public SizeF ThreadLabelSize
        {
            get;
            private set;
        }

        public float SecondsPerLabel
        {
            get { return 1.0f; }
        }

        public float TimeLabelVerticalSpacing
        {
            get { return 2.0f; }
        }

        public float ThreadLabelHorizontalSpacing
        {
            get { return 2.0f; }
        }

        public float VerticalPixelsPerSecond
        {
            get { return this.TimeLabelVerticalSpacing * this.TimeLabelSize.Height / this.SecondsPerLabel; }
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
                    this.hScrollBar.Minimum = 0;
                    this.hScrollBar.Maximum = this.logFileCollection.LogThreads.Length - 1;

                    this.vScrollBar.Minimum = 0;
                    this.vScrollBar.Maximum = (int)((this.logFileCollection.LatestTime.Ticks - this.logFileCollection.EarliestTime.Ticks) / TimeSpan.TicksPerSecond);

                    this.vScrollBar.SmallChange = 1;

                    this.EarliestTimeAvailable =
                        new DateTime(
                            this.LogFileCollection.EarliestTime.Year, this.LogFileCollection.EarliestTime.Month, this.LogFileCollection.EarliestTime.Day,
                            this.LogFileCollection.EarliestTime.Hour, this.LogFileCollection.EarliestTime.Minute, this.LogFileCollection.EarliestTime.Second);

                    this.LatestTimeAvailable =
                        new DateTime(
                            this.LogFileCollection.LatestTime.Year, this.LogFileCollection.LatestTime.Month, this.LogFileCollection.LatestTime.Day,
                            this.LogFileCollection.LatestTime.Hour, this.LogFileCollection.LatestTime.Minute, this.LogFileCollection.LatestTime.Second);
                    this.LatestTimeAvailable = this.LatestTimeAvailable.AddSeconds(1.0);

                    UpdateEarliestAndLatestRenderedTimes(); 

                }
                else
                {
                    this.hScrollBar.Minimum = 0;
                    this.hScrollBar.Maximum = 0;

                    this.vScrollBar.Minimum = 0;
                    this.vScrollBar.Maximum = 0;

                    this.EarliestTimeAvailable = DateTime.MinValue;
                    this.LatestTimeAvailable = DateTime.MinValue;

                    this.EarliestTimeRendered = DateTime.MinValue;
                    this.LatestTimeAvailable = DateTime.MinValue;
                }

                this.Refresh();
            }
        }

        public LogTimelineView()
        {
            InitializeComponent();

            System.Reflection.PropertyInfo aProp =
            typeof(System.Windows.Forms.Control).GetProperty(
              "DoubleBuffered",
              System.Reflection.BindingFlags.NonPublic |
              System.Reflection.BindingFlags.Instance);

            aProp.SetValue(this.panel, true, null);

            this.TimeLabelSize = SizeF.Empty;
            this.ThreadLabelSize = SizeF.Empty;

            this.panel.ContextMenu = this.contextMenu;
        }
        
        private void panel_Paint(object sender, PaintEventArgs e)
        {

            if (this.logFileCollection != null &&
                 0 < this.logFileCollection.LogRecords.Length)
            {
                Brush brush = new SolidBrush(panel.ForeColor);
                Brush darkColumnBrush = new SolidBrush(Color.FromArgb(0xF0, 0xF0, 0xF0));
                Pen pen = new Pen(brush);
                Font font = panel.Font;

                this.UpdateLabelSizes(e.Graphics, font);
 
                DateTime time = this.EarliestTimeRendered;

                float x = 0.0f;
                float y = 0.0f;
                while ( y < panel.Height && time <= this.LatestTimeRendered)
                {
                    e.Graphics.DrawString(time.ToString(ViewUtilities.DateTimeFormat), font, brush, x, y);
                    time = time.AddSeconds( this.SecondsPerLabel );

                    y += this.TimeLabelVerticalSpacing*this.TimeLabelSize.Height;
                }

                e.Graphics.RotateTransform(90);

                Rectangle r = new Rectangle();

                y = -(this.TimeLabelSize.Width+(this.ThreadLabelSize.Height*this.ThreadLabelHorizontalSpacing));
                int threadIdx = 0;
                foreach (LogThread logThread in this.logFileCollection.LogThreads)
                {
                    if (this.hScrollBar.Value <= threadIdx)
                    {
                        if ( threadIdx % 2 == 0 )
                        {
                            r.X = 0;
                            r.Y = (int)Math.Ceiling(y);

                            r.Width = this.panel.Height;
                            r.Height = (int)(Math.Ceiling(this.ThreadLabelSize.Height*this.ThreadLabelHorizontalSpacing));

                            e.Graphics.FillRectangle(darkColumnBrush, r);
                        }

                        e.Graphics.DrawString(logThread.ThreadID, font, brush, x, y);

                        DateTime lastLogRecordTimeRendered = DateTime.MinValue;

                        foreach (LogRecord logRecord in logThread.LogRecords)
                        {
                            if (this.EarliestTimeRendered <= logRecord.Time &&
                                    logRecord.Time != lastLogRecordTimeRendered)
                            {
                                r.X = (int)Math.Floor(YCoordinateFromTime(logRecord.Time) + 10);
                                r.Y = (int)(Math.Floor(y + 10));
                                r.Width = 10;
                                r.Height = 10;

                                logThread.Icon.Draw(e.Graphics, r);

                                lastLogRecordTimeRendered = logRecord.Time;
                            }

                            if (this.LatestTimeRendered < logRecord.Time)
                            {
                                break;
                            }
                        }

                        y -= (this.ThreadLabelSize.Height * this.ThreadLabelHorizontalSpacing);

                    }

                    if ( this.panel.Width < -y )
                    {
                        break;
                    }

                    ++threadIdx;
                }

                e.Graphics.DrawLine(pen, 0, -this.TimeLabelSize.Width+1, this.panel.Height, -this.TimeLabelSize.Width+1);
            }
        }

        private void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            if ( sender == this.vScrollBar )
            {
                this.vScrollBar.Value = e.NewValue;
                UpdateEarliestAndLatestRenderedTimes();
            }

            this.Refresh();
        }

        private void LogTimelineView_SizeChanged(object sender, EventArgs e)
        {
            UpdateEarliestAndLatestRenderedTimes();            
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            int threadIdx = (int)((e.X - this.TimeLabelSize.Width) / (this.ThreadLabelSize.Height*this.ThreadLabelHorizontalSpacing)) + this.hScrollBar.Value;

            if ( this.LogFileCollection != null &&
                 this.hScrollBar.Value <= threadIdx &&
                 threadIdx < this.LogFileCollection.LogThreads.Length )
            {
                LogThread logThread = this.LogFileCollection.LogThreads[threadIdx];

                this.contextMenu.MenuItems.Clear();
                this.contextMenu.MenuItems.Add("Hide This Thread", delegate(object s, EventArgs e2) { HideThread(logThread); });
                this.contextMenu.MenuItems.Add("Hide Other Threads", delegate(object s, EventArgs e2) { HideOtherThreads(logThread);  });
                this.contextMenu.MenuItems.Add("Hide All Threads", delegate(object s, EventArgs e2) { HideAllThreads();  });
                this.contextMenu.MenuItems.Add("Show This Thread", delegate(object s, EventArgs e2) { ShowThisThread(logThread);  });
                this.contextMenu.MenuItems.Add("Show All Threads", delegate(object s, EventArgs e2) { ShowAllThreads();  });

                string toolTipText = "Thread ID: " + logThread.ThreadID;


                foreach (LogRecord logRecord in logThread.LogRecords)
                {
                    if ( this.EarliestTimeRendered <= logRecord.Time )
                    {
                        float coord = YCoordinateFromTime(logRecord.Time) + 10;

                        if ( coord <= e.Y && e.Y <= coord+10 )
                        {
                            toolTipText += "\nTime: " + logRecord.Time.ToString(ViewUtilities.DateTimeFormat) +  "\nFile: " + logRecord.Filename + "\nLine: " + logRecord.LineNumber + "\n" + logRecord.Data;
                            break;
                        }

                        if ( this.LatestTimeRendered < logRecord.Time)
                        {
                            break;
                        }
                    }
                }

                this.ToolTipText = toolTipText;
            }
            else
            {
                this.ToolTipText = "";

                this.contextMenu.MenuItems.Clear();
            }
        }

        private float YCoordinateFromTime(DateTime t)
        {
            return this.VerticalPixelsPerSecond * (t.Ticks - this.EarliestTimeRendered.Ticks) / TimeSpan.TicksPerSecond;
        }

        private void UpdateEarliestAndLatestRenderedTimes()
        {
            if ( this.TimeLabelSize == SizeF.Empty )
            {
                UpdateLabelSizes(this.panel.CreateGraphics(), this.panel.Font);
            }
            
            this.EarliestTimeRendered = this.EarliestTimeAvailable.AddSeconds(1.0 * this.vScrollBar.Value);

            float numberOfSecondsRendered = this.panel.Height / this.VerticalPixelsPerSecond;

            this.LatestTimeRendered = this.EarliestTimeRendered.AddSeconds( Math.Ceiling(numberOfSecondsRendered) );

            if ( this.LatestTimeAvailable < this.LatestTimeRendered )
            {
                this.LatestTimeRendered = this.LatestTimeAvailable;
            }

            this.vScrollBar.LargeChange = (int)Math.Floor(numberOfSecondsRendered);
        }

        private void UpdateLabelSizes(Graphics g, Font f)
        {
            this.TimeLabelSize = g.MeasureString("0000-00-00 00:00:00.000", f);
            this.ThreadLabelSize = g.MeasureString("0000", f);
        }

        void HideThread(LogThread logThreadToHide)
        {
            logThreadToHide.Filtered = true;

            this.Refresh();
            this.ThreadFilterChanged(this, EventArgs.Empty);
        }

        void HideOtherThreads(LogThread logThreadToShow)
        {
            foreach (LogThread logThread in this.LogFileCollection.LogThreads)
            {
                logThread.Filtered = (logThread.ThreadID != logThreadToShow.ThreadID);
            }

            this.Refresh();
            this.ThreadFilterChanged(this, EventArgs.Empty);
        }

        void HideAllThreads()
        {
            foreach (LogThread logThread in this.LogFileCollection.LogThreads)
            {
                logThread.Filtered = true;
            }

            this.Refresh();
            this.ThreadFilterChanged(this, EventArgs.Empty);
        }

        void ShowThisThread(LogThread logThreadToShow)
        {
            logThreadToShow.Filtered = false;

            this.Refresh();
            this.ThreadFilterChanged(this, EventArgs.Empty);
        }

        void ShowAllThreads()
        {
            foreach (LogThread logThread in this.LogFileCollection.LogThreads)
            {
                logThread.Filtered = false;
            }

            this.Refresh();
            this.ThreadFilterChanged(this, EventArgs.Empty);
        }
    }
}
