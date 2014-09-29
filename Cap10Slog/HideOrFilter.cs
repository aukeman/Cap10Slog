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

        private void filterAllButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Filter = true;
            }

            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
        }

        private void hideAllButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Hide = true;
            }

            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
        }

        private void filterNoneButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Filter = false;
            }

            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
        }

        private void hideNoneButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Hide = false;
            }

            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
        }

        private void searchTermTextBox_TextChanged(object sender, EventArgs e)
        {
            this.searchTermFilterButton.Enabled = (0 < this.searchTermTextBox.Text.Length);
            this.searchTermHideButton.Enabled = (0 < this.searchTermTextBox.Text.Length);
        }

        private void searchTermHideButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                if ( lta.GetLogThread().LogRecords.Find( lr => lr.Data.Contains(this.searchTermTextBox.Text) ) != null )
                {
                    lta.Hide = false;
                }
            }

            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
        }

        private void searchTermFilterButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                if (lta.GetLogThread().LogRecords.Find(lr => lr.Data.Contains(this.searchTermTextBox.Text)) != null)
                {
                    lta.Filter = false;
                }
            }

            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
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

        public LogThread GetLogThread()
        {
            return this.logThread;
        }


    }
}
