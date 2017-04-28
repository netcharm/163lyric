namespace netcharm.common
{
    partial class FormLogger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.edLog = new System.Windows.Forms.RichTextBox();
            this.cmLogger = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiLoggerClear = new System.Windows.Forms.ToolStripMenuItem();
            this.cmLogger.SuspendLayout();
            this.SuspendLayout();
            // 
            // edLog
            // 
            this.edLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.edLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edLog.ContextMenuStrip = this.cmLogger;
            this.edLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edLog.ForeColor = System.Drawing.Color.Silver;
            this.edLog.HideSelection = false;
            this.edLog.Location = new System.Drawing.Point(0, 0);
            this.edLog.Name = "edLog";
            this.edLog.ReadOnly = true;
            this.edLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.edLog.ShowSelectionMargin = true;
            this.edLog.Size = new System.Drawing.Size(632, 273);
            this.edLog.TabIndex = 0;
            this.edLog.Text = "";
            // 
            // cmLogger
            // 
            this.cmLogger.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiLoggerClear});
            this.cmLogger.Name = "cmLogger";
            this.cmLogger.Size = new System.Drawing.Size(101, 26);
            // 
            // cmiLoggerClear
            // 
            this.cmiLoggerClear.Image = global::_163music.Properties.Resources.ClearContent_32x;
            this.cmiLoggerClear.Name = "cmiLoggerClear";
            this.cmiLoggerClear.Size = new System.Drawing.Size(100, 22);
            this.cmiLoggerClear.Text = "Clear";
            this.cmiLoggerClear.Click += new System.EventHandler(this.cmiLoggerClear_Click);
            // 
            // FormLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 273);
            this.ContextMenuStrip = this.cmLogger;
            this.Controls.Add(this.edLog);
            this.Name = "FormLogger";
            this.ShowInTaskbar = false;
            this.Text = "Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLogger_FormClosing);
            this.Load += new System.EventHandler(this.FormLogger_Load);
            this.Shown += new System.EventHandler(this.FormLogger_Shown);
            this.cmLogger.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox edLog;
        private System.Windows.Forms.ContextMenuStrip cmLogger;
        private System.Windows.Forms.ToolStripMenuItem cmiLoggerClear;
    }
}