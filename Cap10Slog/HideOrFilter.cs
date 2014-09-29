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
        public EventHandler HideOrFilterChanged;

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

        private void SelectedOptionsChanged()
        {
            this.filterDataGridView.Refresh();
            this.okButton.Enabled = true;
            this.applyButton.Enabled = true;
        }

        private void filterAllButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Filter = true;
            }

            SelectedOptionsChanged();
        }

        private void hideAllButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Hide = true;
            }

            SelectedOptionsChanged();
        }

        private void filterNoneButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Filter = false;
            }

            SelectedOptionsChanged();
        }

        private void hideNoneButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.Hide = false;
            }

            SelectedOptionsChanged();
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

            SelectedOptionsChanged();
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

            SelectedOptionsChanged();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.ApplyChanges();
            }

            this.okButton.Enabled = false;
            this.applyButton.Enabled = false;

            this.HideOrFilterChanged(this, EventArgs.Empty);

            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            foreach (LogThreadAdapter lta in (this.filterDataGridView.DataSource as BindingSource).DataSource as IEnumerable<LogThreadAdapter>)
            {
                lta.ApplyChanges();
            }

            this.okButton.Enabled = false;
            this.applyButton.Enabled = false;

            this.HideOrFilterChanged(this, EventArgs.Empty);
        }

    }

    class LogThreadAdapter
    {
        private LogThread logThread = null;

        public LogThreadAdapter(LogThread logThread)
        {
            this.logThread = logThread;
        }

        private bool hideOverrideFlag = false;
        private bool hideOverride = false;
        public bool Hide
        {
            get
            {
                if (this.hideOverrideFlag)
                {
                    return this.hideOverride;
                }
                else
                {
                    return this.logThread.Hidden;
                }
            }

            set
            {
                this.hideOverrideFlag = true;
                this.hideOverride = value;
            }
        }

        private bool filterOverrideFlag = false;
        private bool filterOverride = false;
        public bool Filter
        {
            get
            {
                if (this.filterOverrideFlag)
                {
                    return this.filterOverride;
                }
                else
                {
                    return this.logThread.Filtered;
                }
            }

            set
            {
                this.filterOverrideFlag = true;
                this.filterOverride = value;
            }
        }

        public void ApplyChanges()
        {
            if ( this.hideOverrideFlag )
            {
                this.logThread.Hidden = this.hideOverride;
                this.hideOverrideFlag = false;
            }

            if (this.filterOverrideFlag)
            {
                this.logThread.Filtered = this.filterOverride;
                this.filterOverrideFlag = false;
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
