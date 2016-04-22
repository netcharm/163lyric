namespace _163lyric
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
            this.No = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicArtist = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicAlbum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicCompany = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicCover = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.musicPhotos = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblResultState = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.queryWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lvResult
            // 
            resources.ApplyResources(this.lvResult, "lvResult");
            this.lvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.musicId,
            this.musicTitle,
            this.musicArtist,
            this.musicAlbum,
            this.musicCompany,
            this.musicCover,
            this.musicPhotos});
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
            // No
            // 
            resources.ApplyResources(this.No, "No");
            // 
            // musicId
            // 
            resources.ApplyResources(this.musicId, "musicId");
            // 
            // musicTitle
            // 
            resources.ApplyResources(this.musicTitle, "musicTitle");
            // 
            // musicArtist
            // 
            resources.ApplyResources(this.musicArtist, "musicArtist");
            // 
            // musicAlbum
            // 
            resources.ApplyResources(this.musicAlbum, "musicAlbum");
            // 
            // musicCompany
            // 
            resources.ApplyResources(this.musicCompany, "musicCompany");
            // 
            // musicCover
            // 
            resources.ApplyResources(this.musicCover, "musicCover");
            // 
            // musicPhotos
            // 
            resources.ApplyResources(this.musicPhotos, "musicPhotos");
            // 
            // lblResultState
            // 
            resources.ApplyResources(this.lblResultState, "lblResultState");
            this.lblResultState.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultState.Name = "lblResultState";
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // queryWorker
            // 
            this.queryWorker.WorkerReportsProgress = true;
            this.queryWorker.WorkerSupportsCancellation = true;
            this.queryWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.queryWorker_DoWork);
            this.queryWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.queryWorker_ProgressChanged);
            this.queryWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.queryWorker_RunWorkerCompleted);
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // FormSearchResult
            // 
            this.AcceptButton = this.btnOk;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblResultState);
            this.Controls.Add(this.lvResult);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormSearchResult";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSearchResult_FormClosing);
            this.Load += new System.EventHandler(this.FormSearchResult_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvResult;
        private System.Windows.Forms.ColumnHeader musicId;
        private System.Windows.Forms.ColumnHeader musicTitle;
        private System.Windows.Forms.ColumnHeader musicArtist;
        private System.Windows.Forms.ColumnHeader musicAlbum;
        private System.Windows.Forms.ColumnHeader musicPhotos;
        private System.Windows.Forms.ColumnHeader musicCover;
        private System.Windows.Forms.ColumnHeader musicCompany;
        private System.Windows.Forms.Label lblResultState;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ColumnHeader No;
        private System.ComponentModel.BackgroundWorker queryWorker;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}