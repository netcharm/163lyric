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
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.IDLable = new System.Windows.Forms.Label();
            this.edID = new System.Windows.Forms.TextBox();
            this.edLyric = new System.Windows.Forms.TextBox();
            this.Lyriclabel = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.cbLrcMultiLang = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IDLable
            // 
            resources.ApplyResources(this.IDLable, "IDLable");
            this.IDLable.Name = "IDLable";
            // 
            // edID
            // 
            resources.ApplyResources(this.edID, "edID");
            this.edID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.edID.Name = "edID";
            this.edID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edID_KeyPress);
            // 
            // edLyric
            // 
            this.edLyric.AcceptsReturn = true;
            this.edLyric.AcceptsTab = true;
            resources.ApplyResources(this.edLyric, "edLyric");
            this.edLyric.HideSelection = false;
            this.edLyric.Name = "edLyric";
            this.edLyric.ReadOnly = true;
            this.edLyric.KeyUp += new System.Windows.Forms.KeyEventHandler(this.edLyric_KeyUp);
            // 
            // Lyriclabel
            // 
            resources.ApplyResources(this.Lyriclabel, "Lyriclabel");
            this.Lyriclabel.Name = "Lyriclabel";
            // 
            // btnGet
            // 
            resources.ApplyResources(this.btnGet, "btnGet");
            this.btnGet.Name = "btnGet";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cbLrcMultiLang
            // 
            resources.ApplyResources(this.cbLrcMultiLang, "cbLrcMultiLang");
            this.cbLrcMultiLang.Checked = true;
            this.cbLrcMultiLang.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLrcMultiLang.Name = "cbLrcMultiLang";
            this.cbLrcMultiLang.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            resources.ApplyResources(this.lblTitle, "lblTitle");
            this.lblTitle.Name = "lblTitle";
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "lrc";
            resources.ApplyResources(this.dlgSave, "dlgSave");
            this.dlgSave.RestoreDirectory = true;
            this.dlgSave.SupportMultiDottedExtensions = true;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExit
            // 
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.Name = "btnExit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbLrcMultiLang);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.Lyriclabel);
            this.Controls.Add(this.edLyric);
            this.Controls.Add(this.edID);
            this.Controls.Add(this.IDLable);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IDLable;
        private System.Windows.Forms.TextBox edID;
        private System.Windows.Forms.TextBox edLyric;
        private System.Windows.Forms.Label Lyriclabel;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox cbLrcMultiLang;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
    }
}

