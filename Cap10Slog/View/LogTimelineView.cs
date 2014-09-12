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
        public LogTimelineView()
        {
            InitializeComponent();

            System.Reflection.PropertyInfo aProp =
            typeof(System.Windows.Forms.Control).GetProperty(
              "DoubleBuffered",
              System.Reflection.BindingFlags.NonPublic |
              System.Reflection.BindingFlags.Instance);

            aProp.SetValue(this.panel, true, null);
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
                    this.vScrollBar.Maximum = (int)((this.logFileCollection.LatestTime.Ticks- this.logFileCollection.EarliestTime.Ticks)/TimeSpan.TicksPerSecond);

                    this.vScrollBar.SmallChange = 1;
                }
                else
                {
                    this.hScrollBar.Minimum = 0;
                    this.hScrollBar.Maximum = 0;

                    this.vScrollBar.Minimum = 0;
                    this.vScrollBar.Maximum = 0;
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

                SizeF timeLabelSize = e.Graphics.MeasureString("0000-00-00 00:00:00.000", font);
                SizeF threadLabelSize = e.Graphics.MeasureString("0000", font);

                DateTime time = new DateTime(
                    this.LogFileCollection.EarliestTime.Year, this.LogFileCollection.EarliestTime.Month, this.LogFileCollection.EarliestTime.Day,
                    this.LogFileCollection.EarliestTime.Hour, this.LogFileCollection.EarliestTime.Minute, this.LogFileCollection.EarliestTime.Second );

                time = time.AddSeconds(1.0 * this.vScrollBar.Value);

                int numberOfSecondsRendered = 0;
                float x = 0.0f;
                float y = 0.0f;
                while ( y < panel.Height )
                {
                    e.Graphics.DrawString(time.ToString("yyyy-MM-dd HH:mm:ss.fff"), font, brush, x, y);
                    time = time.AddSeconds(1.0);

                    y += 2*timeLabelSize.Height;
                    ++numberOfSecondsRendered;
                }

                e.Graphics.DrawLine(pen, timeLabelSize.Width, 0, timeLabelSize.Width, panel.Height);

                e.Graphics.RotateTransform(90);

                y = -(timeLabelSize.Width+threadLabelSize.Height+2);
                int threadIdx = 0;
                foreach (LogThread logThread in this.logFileCollection.LogThreads)
                {
                    if (this.hScrollBar.Value <= threadIdx)
                    {
                        e.Graphics.DrawString(logThread.ThreadID, font, brush, x, y);
                        y -= threadLabelSize.Height;
                    }

                    if ( this.panel.Width < -y )
                    {
                        break;
                    }

                    ++threadIdx;
                }


                this.vScrollBar.LargeChange = numberOfSecondsRendered;

            }
        }

        //private void splitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        //{
        //    if (this.logFileCollection != null &&
        //         0 < this.logFileCollection.LogRecords.Length)
        //    {
        //        Brush brush = new SolidBrush(splitContainer.Panel1.ForeColor);
        //        Font font = splitContainer.Panel1.Font;

        //    }
        //}

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            this.Refresh();
        }


    }
}
