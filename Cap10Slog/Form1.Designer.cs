namespace Cap10Slog
{
    partial class Form1
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
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.splitter = new System.Windows.Forms.Splitter();
            this.logTimelineView = new Cap10Slog.View.LogTimelineView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.logTimelineView);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.splitter);
            this.splitContainer.Size = new System.Drawing.Size(882, 674);
            this.splitContainer.SplitterDistance = 337;
            this.splitContainer.TabIndex = 3;
            // 
            // splitter
            // 
            this.splitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter.Location = new System.Drawing.Point(0, 0);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(882, 3);
            this.splitter.TabIndex = 0;
            this.splitter.TabStop = false;
            // 
            // logTimelineView
            // 
            this.logTimelineView.AllowDrop = true;
            this.logTimelineView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTimelineView.Location = new System.Drawing.Point(0, 0);
            this.logTimelineView.LogFileCollection = null;
            this.logTimelineView.Name = "logTimelineView";
            this.logTimelineView.Size = new System.Drawing.Size(882, 337);
            this.logTimelineView.TabIndex = 0;
            this.logTimelineView.DragDrop += new System.Windows.Forms.DragEventHandler(this.logTimelineView1_DragDrop);
            this.logTimelineView.DragEnter += new System.Windows.Forms.DragEventHandler(this.logTimelineView1_DragEnter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 674);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Splitter splitter;
        private View.LogTimelineView logTimelineView;
    }
}

