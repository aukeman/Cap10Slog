using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Cap10Slog.Model;

namespace Cap10Slog
{
    public partial class HideOrFilter : Form
    {
        public HideOrFilter()
        {
            InitializeComponent();
        }

        private LogFileCollection logFileCollection = null;
        public LogFileCollection LogFileCollection
        {
            get
            {
                return this.logFileCollection;
            }

            set
            {
                this.logFileCollection = value;

                if ( this.logFileCollection != null )
                {
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource =
                        this.logFileCollection.LogThreads.Select<LogThread, LogThreadAdapter>(lt => new LogThreadAdapter(lt));

                    this.filterDataGridView.DataSource = bindingSource;
                }
                else
                {
                    this.filterDataGridView.DataSource = null;
                }
            }
        }
    }

    class LogThreadAdapter
    {
        private LogThread logThread = null;

        public LogThreadAdapter(LogThread logThread)
        {
            this.logThread = logThread;
        }

        public bool Hide
        {
            get
            {
                return this.logThread.Hidden;
            }

            set
            {
                this.logThread.Hidden = value;
            }
        }

        public bool Filter
        {
            get
            {
                return this.logThread.Filtered;
            }

            set
            {
                this.logThread.Filtered = value;
            }
        }

        public string ThreadID
        {
            get
            {
                return this.logThread.ThreadID;
            }
        }

    }
}
