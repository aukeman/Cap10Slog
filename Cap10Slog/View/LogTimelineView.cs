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
    public partial class LogTimelineView : UserControl
    {

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
                            this.LogFileCollection.LatestTime.Hour, this.LogFileCollection.LatestTime.Minute, this.LogFileCollection.LatestTime.Second + 1);

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

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            if (this.logFileCollection != null &&
                 0 < this.logFileCollection.LogRecords.Length)
            {
                Brush brush = new SolidBrush(panel.ForeColor);
                Pen pen = new Pen(brush);
                Font font = panel.Font;

                this.UpdateLabelSizes(e.Graphics, font);
 
                DateTime time = this.EarliestTimeRendered;

                float x = 0.0f;
                float y = 0.0f;
                while ( y < panel.Height && time <= this.LatestTimeRendered)
                {
                    e.Graphics.DrawString(time.ToString("yyyy-MM-dd HH:mm:ss.fff"), font, brush, x, y);
                    time = time.AddSeconds( this.SecondsPerLabel );

                    y += this.TimeLabelVerticalSpacing*this.TimeLabelSize.Height;
                }

                e.Graphics.DrawLine(pen, this.TimeLabelSize.Width, 0, this.TimeLabelSize.Width, panel.Height);

                e.Graphics.RotateTransform(90);

                Rectangle r = new Rectangle();

                y = -(this.TimeLabelSize.Width+(this.ThreadLabelSize.Height*this.ThreadLabelHorizontalSpacing));
                int threadIdx = 0;
                foreach (LogThread logThread in this.logFileCollection.LogThreads)
                {
                    if (this.hScrollBar.Value <= threadIdx)
                    {
                        e.Graphics.DrawString(logThread.ThreadID, font, brush, x, y);
                        y -= (this.ThreadLabelSize.Height*this.ThreadLabelHorizontalSpacing);

                        foreach (LogRecord logRecord in logThread.LogRecords)
                        {
                            if ( this.EarliestTimeRendered <= logRecord.Time )
                            {
                                r.X = (int)Math.Floor(YCoordinateFromTime(logRecord.Time) - 10);
                                r.Y = (int)(Math.Floor(y - 10));
                                r.Width = 10;
                                r.Height = 10;

                                e.Graphics.FillEllipse(brush, r);
                            }

                            if (this.LatestTimeRendered < logRecord.Time )
                            {
                                break;
                            }
                        }
                    }

                    if ( this.panel.Width < -y )
                    {
                        break;
                    }
                    ++threadIdx;
                }
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

    }
}
