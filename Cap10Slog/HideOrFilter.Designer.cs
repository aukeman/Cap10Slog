namespace Cap10Slog
{
    partial class HideOrFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filterDataGridView = new System.Windows.Forms.DataGridView();
            this.hide = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.filter = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.thread = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hideAllButton = new System.Windows.Forms.Button();
            this.hideNoneButton = new System.Windows.Forms.Button();
            this.filterAllButton = new System.Windows.Forms.Button();
            this.filterNoneButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.searchTermTextBox = new System.Windows.Forms.TextBox();
            this.searchTermFilterButton = new System.Windows.Forms.Button();
            this.searchTermHideButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.filterDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterDataGridView
            // 
            this.filterDataGridView.AllowUserToAddRows = false;
            this.filterDataGridView.AllowUserToDeleteRows = false;
            this.filterDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.filterDataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.filterDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.filterDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.hide,
            this.filter,
            this.thread});
            this.filterDataGridView.Location = new System.Drawing.Point(12, 12);
            this.filterDataGridView.MultiSelect = false;
            this.filterDataGridView.Name = "filterDataGridView";
            this.filterDataGridView.RowHeadersVisible = false;
            this.filterDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.filterDataGridView.Size = new System.Drawing.Size(460, 451);
            this.filterDataGridView.TabIndex = 0;
            // 
            // hide
            // 
            this.hide.DataPropertyName = "Hide";
            this.hide.HeaderText = "Hide From Text View";
            this.hide.Name = "hide";
            // 
            // filter
            // 
            this.filter.DataPropertyName = "Filter";
            this.filter.HeaderText = "Filter From Timeline";
            this.filter.Name = "filter";
            // 
            // thread
            // 
            this.thread.DataPropertyName = "ThreadID";
            this.thread.HeaderText = "Thread ID";
            this.thread.Name = "thread";
            this.thread.ReadOnly = true;
            // 
            // hideAllButton
            // 
            this.hideAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hideAllButton.Location = new System.Drawing.Point(268, 469);
            this.hideAllButton.Name = "hideAllButton";
            this.hideAllButton.Size = new System.Drawing.Size(75, 23);
            this.hideAllButton.TabIndex = 1;
            this.hideAllButton.Text = "Hide All";
            this.hideAllButton.UseVisualStyleBackColor = true;
            this.hideAllButton.Click += new System.EventHandler(this.hideAllButton_Click);
            // 
            // hideNoneButton
            // 
            this.hideNoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hideNoneButton.Location = new System.Drawing.Point(396, 469);
            this.hideNoneButton.Name = "hideNoneButton";
            this.hideNoneButton.Size = new System.Drawing.Size(75, 23);
            this.hideNoneButton.TabIndex = 2;
            this.hideNoneButton.Text = "Hide None";
            this.hideNoneButton.UseVisualStyleBackColor = true;
            this.hideNoneButton.Click += new System.EventHandler(this.hideNoneButton_Click);
            // 
            // filterAllButton
            // 
            this.filterAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filterAllButton.Location = new System.Drawing.Point(12, 469);
            this.filterAllButton.Name = "filterAllButton";
            this.filterAllButton.Size = new System.Drawing.Size(75, 23);
            this.filterAllButton.TabIndex = 3;
            this.filterAllButton.Text = "Filter All";
            this.filterAllButton.UseVisualStyleBackColor = true;
            this.filterAllButton.Click += new System.EventHandler(this.filterAllButton_Click);
            // 
            // filterNoneButton
            // 
            this.filterNoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.filterNoneButton.Location = new System.Drawing.Point(140, 469);
            this.filterNoneButton.Name = "filterNoneButton";
            this.filterNoneButton.Size = new System.Drawing.Size(75, 23);
            this.filterNoneButton.TabIndex = 4;
            this.filterNoneButton.Text = "Filter None";
            this.filterNoneButton.UseVisualStyleBackColor = true;
            this.filterNoneButton.Click += new System.EventHandler(this.filterNoneButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(316, 565);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Enabled = false;
            this.okButton.Location = new System.Drawing.Point(235, 565);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.searchTermTextBox);
            this.groupBox1.Controls.Add(this.searchTermFilterButton);
            this.groupBox1.Controls.Add(this.searchTermHideButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 498);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hide or Filter Based on Search Term";
            // 
            // searchTermTextBox
            // 
            this.searchTermTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTermTextBox.Location = new System.Drawing.Point(8, 21);
            this.searchTermTextBox.Name = "searchTermTextBox";
            this.searchTermTextBox.Size = new System.Drawing.Size(257, 20);
            this.searchTermTextBox.TabIndex = 10;
            this.searchTermTextBox.TextChanged += new System.EventHandler(this.searchTermTextBox_TextChanged);
            // 
            // searchTermFilterButton
            // 
            this.searchTermFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTermFilterButton.Enabled = false;
            this.searchTermFilterButton.Location = new System.Drawing.Point(363, 19);
            this.searchTermFilterButton.Name = "searchTermFilterButton";
            this.searchTermFilterButton.Size = new System.Drawing.Size(75, 23);
            this.searchTermFilterButton.TabIndex = 9;
            this.searchTermFilterButton.Text = "Filter";
            this.searchTermFilterButton.UseVisualStyleBackColor = true;
            this.searchTermFilterButton.Click += new System.EventHandler(this.searchTermFilterButton_Click);
            // 
            // searchTermHideButton
            // 
            this.searchTermHideButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTermHideButton.Enabled = false;
            this.searchTermHideButton.Location = new System.Drawing.Point(276, 19);
            this.searchTermHideButton.Name = "searchTermHideButton";
            this.searchTermHideButton.Size = new System.Drawing.Size(75, 23);
            this.searchTermHideButton.TabIndex = 8;
            this.searchTermHideButton.Text = "Hide";
            this.searchTermHideButton.UseVisualStyleBackColor = true;
            this.searchTermHideButton.Click += new System.EventHandler(this.searchTermHideButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.Enabled = false;
            this.applyButton.Location = new System.Drawing.Point(397, 565);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 8;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // HideOrFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 600);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.filterNoneButton);
            this.Controls.Add(this.filterAllButton);
            this.Controls.Add(this.hideNoneButton);
            this.Controls.Add(this.hideAllButton);
            this.Controls.Add(this.filterDataGridView);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "HideOrFilter";
            this.Text = "Hide Or Filter Threads";
            ((System.ComponentModel.ISupportInitialize)(this.filterDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView filterDataGridView;
        private System.Windows.Forms.Button hideAllButton;
        private System.Windows.Forms.Button hideNoneButton;
        private System.Windows.Forms.Button filterAllButton;
        private System.Windows.Forms.Button filterNoneButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button searchTermHideButton;
        private System.Windows.Forms.Button searchTermFilterButton;
        private System.Windows.Forms.TextBox searchTermTextBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hide;
        private System.Windows.Forms.DataGridViewCheckBoxColumn filter;
        private System.Windows.Forms.DataGridViewTextBoxColumn thread;
    }
}