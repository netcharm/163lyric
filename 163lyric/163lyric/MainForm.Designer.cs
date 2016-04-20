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
            this.Getbtn = new System.Windows.Forms.Button();
            this.Copybtn = new System.Windows.Forms.Button();
            this.cbLrcMultiLang = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
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
            this.edID.Cursor = System.Windows.Forms.Cursors.Default;
            this.edID.Name = "edID";
            // 
            // edLyric
            // 
            this.edLyric.AcceptsReturn = true;
            this.edLyric.AcceptsTab = true;
            resources.ApplyResources(this.edLyric, "edLyric");
            this.edLyric.HideSelection = false;
            this.edLyric.Name = "edLyric";
            this.edLyric.ReadOnly = true;
            // 
            // Lyriclabel
            // 
            resources.ApplyResources(this.Lyriclabel, "Lyriclabel");
            this.Lyriclabel.Name = "Lyriclabel";
            // 
            // Getbtn
            // 
            resources.ApplyResources(this.Getbtn, "Getbtn");
            this.Getbtn.Name = "Getbtn";
            this.Getbtn.UseVisualStyleBackColor = true;
            this.Getbtn.Click += new System.EventHandler(this.Getbtn_Click);
            // 
            // Copybtn
            // 
            resources.ApplyResources(this.Copybtn, "Copybtn");
            this.Copybtn.Name = "Copybtn";
            this.Copybtn.UseVisualStyleBackColor = true;
            this.Copybtn.Click += new System.EventHandler(this.Copybtn_Click);
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
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbLrcMultiLang);
            this.Controls.Add(this.Copybtn);
            this.Controls.Add(this.Getbtn);
            this.Controls.Add(this.Lyriclabel);
            this.Controls.Add(this.edLyric);
            this.Controls.Add(this.edID);
            this.Controls.Add(this.IDLable);
            this.Name = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IDLable;
        private System.Windows.Forms.TextBox edID;
        private System.Windows.Forms.TextBox edLyric;
        private System.Windows.Forms.Label Lyriclabel;
        private System.Windows.Forms.Button Getbtn;
        private System.Windows.Forms.Button Copybtn;
        private System.Windows.Forms.CheckBox cbLrcMultiLang;
        private System.Windows.Forms.Label lblTitle;
    }
}

