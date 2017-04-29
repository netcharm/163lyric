namespace _163music
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnConvert = new System.Windows.Forms.Button();
            this.btnPandocHelp = new System.Windows.Forms.Button();
            this.btnLogger = new System.Windows.Forms.Button();
            this.chkFixFilename = new System.Windows.Forms.CheckBox();
            this.chkTopmost = new System.Windows.Forms.CheckBox();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            resources.ApplyResources(this.btnConvert, "btnConvert");
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // btnPandocHelp
            // 
            resources.ApplyResources(this.btnPandocHelp, "btnPandocHelp");
            this.btnPandocHelp.Image = global::_163music.Properties.Resources.HelpApplication_16x;
            this.btnPandocHelp.Name = "btnPandocHelp";
            this.btnPandocHelp.UseVisualStyleBackColor = true;
            this.btnPandocHelp.Click += new System.EventHandler(this.btnPandocHelp_Click);
            // 
            // btnLogger
            // 
            resources.ApplyResources(this.btnLogger, "btnLogger");
            this.btnLogger.Image = global::_163music.Properties.Resources.Log_16x;
            this.btnLogger.Name = "btnLogger";
            this.btnLogger.UseVisualStyleBackColor = true;
            this.btnLogger.Click += new System.EventHandler(this.btnLogger_Click);
            // 
            // chkFixFilename
            // 
            resources.ApplyResources(this.chkFixFilename, "chkFixFilename");
            this.chkFixFilename.Checked = true;
            this.chkFixFilename.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFixFilename.Name = "chkFixFilename";
            this.chkFixFilename.UseVisualStyleBackColor = true;
            // 
            // chkTopmost
            // 
            resources.ApplyResources(this.chkTopmost, "chkTopmost");
            this.chkTopmost.Name = "chkTopmost";
            this.chkTopmost.UseVisualStyleBackColor = true;
            this.chkTopmost.CheckedChanged += new System.EventHandler(this.chkTopmost_CheckedChanged);
            // 
            // dlgOpen
            // 
            this.dlgOpen.DefaultExt = "md";
            this.dlgOpen.FileName = "openFileDialog1";
            resources.ApplyResources(this.dlgOpen, "dlgOpen");
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkTopmost);
            this.Controls.Add(this.chkFixFilename);
            this.Controls.Add(this.btnPandocHelp);
            this.Controls.Add(this.btnLogger);
            this.Controls.Add(this.btnConvert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Button btnLogger;
        private System.Windows.Forms.Button btnPandocHelp;
        private System.Windows.Forms.CheckBox chkFixFilename;
        private System.Windows.Forms.CheckBox chkTopmost;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
    }
}

