﻿namespace _163lyric
{
    partial class FormSearchResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSearchResult));
            this.lvResult = new System.Windows.Forms.ListView();
            this.musicId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicAlbum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicPhotos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicCover = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblResultState = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lvResult
            // 
            resources.ApplyResources(this.lvResult, "lvResult");
            this.lvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.musicId,
            this.musicName,
            this.musicArtist,
            this.musicAlbum,
            this.musicPhotos,
            this.musicCover,
            this.musicCompany});
            this.lvResult.FullRowSelect = true;
            this.lvResult.GridLines = true;
            this.lvResult.MultiSelect = false;
            this.lvResult.Name = "lvResult";
            this.lvResult.ShowItemToolTips = true;
            this.lvResult.UseCompatibleStateImageBehavior = false;
            this.lvResult.View = System.Windows.Forms.View.Details;
            this.lvResult.VirtualMode = true;
            this.lvResult.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvResult_RetrieveVirtualItem);
            this.lvResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvResult_MouseDoubleClick);
            // 
            // musicId
            // 
            resources.ApplyResources(this.musicId, "musicId");
            // 
            // musicName
            // 
            resources.ApplyResources(this.musicName, "musicName");
            // 
            // musicArtist
            // 
            resources.ApplyResources(this.musicArtist, "musicArtist");
            // 
            // musicAlbum
            // 
            resources.ApplyResources(this.musicAlbum, "musicAlbum");
            // 
            // musicPhotos
            // 
            resources.ApplyResources(this.musicPhotos, "musicPhotos");
            // 
            // musicCover
            // 
            resources.ApplyResources(this.musicCover, "musicCover");
            // 
            // musicCompany
            // 
            resources.ApplyResources(this.musicCompany, "musicCompany");
            // 
            // lblResultState
            // 
            resources.ApplyResources(this.lblResultState, "lblResultState");
            this.lblResultState.Name = "lblResultState";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // FormSearchResult
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblResultState);
            this.Controls.Add(this.lvResult);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSearchResult";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FormSearchResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResult;
        private System.Windows.Forms.ColumnHeader musicId;
        private System.Windows.Forms.ColumnHeader musicName;
        private System.Windows.Forms.ColumnHeader musicArtist;
        private System.Windows.Forms.ColumnHeader musicAlbum;
        private System.Windows.Forms.ColumnHeader musicPhotos;
        private System.Windows.Forms.ColumnHeader musicCover;
        private System.Windows.Forms.ColumnHeader musicCompany;
        private System.Windows.Forms.Label lblResultState;
        private System.Windows.Forms.Button btnOk;
    }
}