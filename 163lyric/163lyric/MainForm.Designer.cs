﻿namespace _163lyric
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
            this.IDLable = new System.Windows.Forms.Label();
            this.IDtb = new System.Windows.Forms.TextBox();
            this.Lyrictb = new System.Windows.Forms.TextBox();
            this.Lyriclabel = new System.Windows.Forms.Label();
            this.Getbtn = new System.Windows.Forms.Button();
            this.Copybtn = new System.Windows.Forms.Button();
            this.cbLrcMultiLang = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // IDLable
            // 
            this.IDLable.AutoSize = true;
            this.IDLable.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.IDLable.Location = new System.Drawing.Point(14, 18);
            this.IDLable.Name = "IDLable";
            this.IDLable.Size = new System.Drawing.Size(24, 16);
            this.IDLable.TabIndex = 0;
            this.IDLable.Text = "ID";
            this.IDLable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // IDtb
            // 
            this.IDtb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IDtb.Cursor = System.Windows.Forms.Cursors.Default;
            this.IDtb.Location = new System.Drawing.Point(49, 16);
            this.IDtb.Name = "IDtb";
            this.IDtb.Size = new System.Drawing.Size(371, 21);
            this.IDtb.TabIndex = 1;
            // 
            // Lyrictb
            // 
            this.Lyrictb.AcceptsReturn = true;
            this.Lyrictb.AcceptsTab = true;
            this.Lyrictb.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lyrictb.HideSelection = false;
            this.Lyrictb.Location = new System.Drawing.Point(14, 76);
            this.Lyrictb.Multiline = true;
            this.Lyrictb.Name = "Lyrictb";
            this.Lyrictb.ReadOnly = true;
            this.Lyrictb.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Lyrictb.Size = new System.Drawing.Size(494, 302);
            this.Lyrictb.TabIndex = 2;
            // 
            // Lyriclabel
            // 
            this.Lyriclabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Lyriclabel.Location = new System.Drawing.Point(14, 51);
            this.Lyriclabel.Name = "Lyriclabel";
            this.Lyriclabel.Size = new System.Drawing.Size(48, 16);
            this.Lyriclabel.TabIndex = 3;
            this.Lyriclabel.Text = "Lyric";
            this.Lyriclabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Getbtn
            // 
            this.Getbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Getbtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Getbtn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Getbtn.Location = new System.Drawing.Point(433, 15);
            this.Getbtn.Name = "Getbtn";
            this.Getbtn.Size = new System.Drawing.Size(75, 23);
            this.Getbtn.TabIndex = 4;
            this.Getbtn.Text = "GET";
            this.Getbtn.UseVisualStyleBackColor = true;
            this.Getbtn.Click += new System.EventHandler(this.Getbtn_Click);
            // 
            // Copybtn
            // 
            this.Copybtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Copybtn.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Copybtn.Location = new System.Drawing.Point(101, 391);
            this.Copybtn.Name = "Copybtn";
            this.Copybtn.Size = new System.Drawing.Size(312, 23);
            this.Copybtn.TabIndex = 5;
            this.Copybtn.Text = "COPY";
            this.Copybtn.UseVisualStyleBackColor = true;
            this.Copybtn.Click += new System.EventHandler(this.Copybtn_Click);
            // 
            // cbLrcMultiLang
            // 
            this.cbLrcMultiLang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLrcMultiLang.Checked = true;
            this.cbLrcMultiLang.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbLrcMultiLang.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbLrcMultiLang.Location = new System.Drawing.Point(401, 51);
            this.cbLrcMultiLang.Name = "cbLrcMultiLang";
            this.cbLrcMultiLang.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbLrcMultiLang.Size = new System.Drawing.Size(107, 17);
            this.cbLrcMultiLang.TabIndex = 6;
            this.cbLrcMultiLang.Text = "Multi Language";
            this.cbLrcMultiLang.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.Location = new System.Drawing.Point(66, 51);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(329, 17);
            this.lblTitle.TabIndex = 7;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 426);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.cbLrcMultiLang);
            this.Controls.Add(this.Copybtn);
            this.Controls.Add(this.Getbtn);
            this.Controls.Add(this.Lyriclabel);
            this.Controls.Add(this.Lyrictb);
            this.Controls.Add(this.IDtb);
            this.Controls.Add(this.IDLable);
            this.Name = "MainForm";
            this.Text = "163Lyric";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IDLable;
        private System.Windows.Forms.TextBox IDtb;
        private System.Windows.Forms.TextBox Lyrictb;
        private System.Windows.Forms.Label Lyriclabel;
        private System.Windows.Forms.Button Getbtn;
        private System.Windows.Forms.Button Copybtn;
        private System.Windows.Forms.CheckBox cbLrcMultiLang;
        private System.Windows.Forms.Label lblTitle;
    }
}

