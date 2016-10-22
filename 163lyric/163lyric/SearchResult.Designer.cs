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
            this.components = new System.ComponentModel.Container();
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
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDisplayCover = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisplayPhoto = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyArtist = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyAlbum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyTitle = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyPublisher = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyPhotoURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyCoverURL = new System.Windows.Forms.ToolStripMenuItem();
            this.lblResultState = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.queryWorker = new System.ComponentModel.BackgroundWorker();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.contextMenu.SuspendLayout();
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
            this.lvResult.ContextMenuStrip = this.contextMenu;
            this.lvResult.FullRowSelect = true;
            this.lvResult.GridLines = true;
            this.lvResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvResult.HideSelection = false;
            this.lvResult.MultiSelect = false;
            this.lvResult.Name = "lvResult";
            this.lvResult.ShowItemToolTips = true;
            this.lvResult.UseCompatibleStateImageBehavior = false;
            this.lvResult.View = System.Windows.Forms.View.Details;
            this.lvResult.VirtualMode = true;
            this.lvResult.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvResult_ItemSelectionChanged);
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
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDisplayCover,
            this.tsmiDisplayPhoto,
            this.toolStripMenuItem1,
            this.tsmiCopyArtist,
            this.tsmiCopyAlbum,
            this.tsmiCopyTitle,
            this.tsmiCopyPublisher,
            this.toolStripMenuItem2,
            this.tsmiCopyPhotoURL,
            this.tsmiCopyCoverURL});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            resources.ApplyResources(this.contextMenu, "contextMenu");
            this.contextMenu.Opened += new System.EventHandler(this.contextMenu_Opened);
            // 
            // tsmiDisplayCover
            // 
            this.tsmiDisplayCover.Name = "tsmiDisplayCover";
            resources.ApplyResources(this.tsmiDisplayCover, "tsmiDisplayCover");
            this.tsmiDisplayCover.Click += new System.EventHandler(this.tsmiDisplayCover_Click);
            // 
            // tsmiDisplayPhoto
            // 
            this.tsmiDisplayPhoto.Name = "tsmiDisplayPhoto";
            resources.ApplyResources(this.tsmiDisplayPhoto, "tsmiDisplayPhoto");
            this.tsmiDisplayPhoto.Click += new System.EventHandler(this.tsmiDisplayPhoto_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // tsmiCopyArtist
            // 
            this.tsmiCopyArtist.Name = "tsmiCopyArtist";
            resources.ApplyResources(this.tsmiCopyArtist, "tsmiCopyArtist");
            this.tsmiCopyArtist.Click += new System.EventHandler(this.tsmiCopyArtist_Click);
            // 
            // tsmiCopyAlbum
            // 
            this.tsmiCopyAlbum.Name = "tsmiCopyAlbum";
            resources.ApplyResources(this.tsmiCopyAlbum, "tsmiCopyAlbum");
            this.tsmiCopyAlbum.Click += new System.EventHandler(this.tsmiCopyAlbum_Click);
            // 
            // tsmiCopyTitle
            // 
            this.tsmiCopyTitle.Name = "tsmiCopyTitle";
            resources.ApplyResources(this.tsmiCopyTitle, "tsmiCopyTitle");
            this.tsmiCopyTitle.Click += new System.EventHandler(this.tsmiCopyTitle_Click);
            // 
            // tsmiCopyPublisher
            // 
            this.tsmiCopyPublisher.Name = "tsmiCopyPublisher";
            resources.ApplyResources(this.tsmiCopyPublisher, "tsmiCopyPublisher");
            this.tsmiCopyPublisher.Click += new System.EventHandler(this.tsmiCopyPublisher_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // tsmiCopyPhotoURL
            // 
            this.tsmiCopyPhotoURL.Name = "tsmiCopyPhotoURL";
            resources.ApplyResources(this.tsmiCopyPhotoURL, "tsmiCopyPhotoURL");
            this.tsmiCopyPhotoURL.Click += new System.EventHandler(this.tsmiCopyPhotoURL_Click);
            // 
            // tsmiCopyCoverURL
            // 
            this.tsmiCopyCoverURL.Name = "tsmiCopyCoverURL";
            resources.ApplyResources(this.tsmiCopyCoverURL, "tsmiCopyCoverURL");
            this.tsmiCopyCoverURL.Click += new System.EventHandler(this.tsmiCopyCoverURL_Click);
            // 
            // lblResultState
            // 
            resources.ApplyResources(this.lblResultState, "lblResultState");
            this.lblResultState.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblResultState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
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
            this.HelpButton = true;
            this.Name = "FormSearchResult";
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSearchResult_FormClosing);
            this.Load += new System.EventHandler(this.FormSearchResult_Load);
            this.contextMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyPhotoURL;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyCoverURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyArtist;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyAlbum;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyTitle;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyPublisher;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayPhoto;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayCover;
    }
}