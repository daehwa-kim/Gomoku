namespace Gomoku
{
    partial class Match
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
            this.components = new System.ComponentModel.Container();
            this.pnlBoard = new System.Windows.Forms.Panel();
            this.tmrAi = new System.Windows.Forms.Timer(this.components);
            this.lblHomePlayer = new System.Windows.Forms.Label();
            this.lblAwayPlayer = new System.Windows.Forms.Label();
            this.pnlPlayers = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSendChat = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnAuto = new System.Windows.Forms.Button();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.picHomePlayer = new System.Windows.Forms.PictureBox();
            this.picAwayPlayer = new System.Windows.Forms.PictureBox();
            this.bgwListener = new System.ComponentModel.BackgroundWorker();
            this.bgwClient = new System.ComponentModel.BackgroundWorker();
            this.tipChat = new System.Windows.Forms.ToolTip(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnToggleFullscreen = new System.Windows.Forms.Button();
            this.pnlPlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHomePlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAwayPlayer)).BeginInit();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBoard
            // 
            this.pnlBoard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlBoard.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlBoard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlBoard.Location = new System.Drawing.Point(0, 90);
            this.pnlBoard.Name = "pnlBoard";
            this.pnlBoard.Size = new System.Drawing.Size(585, 585);
            this.pnlBoard.TabIndex = 0;
            this.pnlBoard.Visible = false;
            // 
            // tmrAi
            // 
            this.tmrAi.Interval = 1000;
            this.tmrAi.Tick += new System.EventHandler(this.tmrAi_Tick);
            // 
            // lblHomePlayer
            // 
            this.lblHomePlayer.AutoEllipsis = true;
            this.lblHomePlayer.AutoSize = true;
            this.lblHomePlayer.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHomePlayer.Location = new System.Drawing.Point(65, 26);
            this.lblHomePlayer.MaximumSize = new System.Drawing.Size(125, 0);
            this.lblHomePlayer.Name = "lblHomePlayer";
            this.lblHomePlayer.Size = new System.Drawing.Size(64, 25);
            this.lblHomePlayer.TabIndex = 1;
            this.lblHomePlayer.Text = "Home";
            // 
            // lblAwayPlayer
            // 
            this.lblAwayPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAwayPlayer.AutoEllipsis = true;
            this.lblAwayPlayer.AutoSize = true;
            this.lblAwayPlayer.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAwayPlayer.Location = new System.Drawing.Point(503, 26);
            this.lblAwayPlayer.MaximumSize = new System.Drawing.Size(125, 0);
            this.lblAwayPlayer.Name = "lblAwayPlayer";
            this.lblAwayPlayer.Size = new System.Drawing.Size(59, 25);
            this.lblAwayPlayer.TabIndex = 1;
            this.lblAwayPlayer.Text = "Away";
            this.lblAwayPlayer.SizeChanged += new System.EventHandler(this.lblAwayPlayer_SizeChanged);
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.BackColor = System.Drawing.Color.Transparent;
            this.pnlPlayers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlPlayers.Controls.Add(this.btnBack);
            this.pnlPlayers.Controls.Add(this.txtChat);
            this.pnlPlayers.Controls.Add(this.btnSendChat);
            this.pnlPlayers.Controls.Add(this.btnTest);
            this.pnlPlayers.Controls.Add(this.lblMessage);
            this.pnlPlayers.Controls.Add(this.btnAuto);
            this.pnlPlayers.Controls.Add(this.chkDebug);
            this.pnlPlayers.Controls.Add(this.btnReset);
            this.pnlPlayers.Controls.Add(this.picHomePlayer);
            this.pnlPlayers.Controls.Add(this.picAwayPlayer);
            this.pnlPlayers.Controls.Add(this.lblHomePlayer);
            this.pnlPlayers.Controls.Add(this.lblAwayPlayer);
            this.pnlPlayers.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPlayers.Location = new System.Drawing.Point(0, 0);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(585, 90);
            this.pnlPlayers.TabIndex = 3;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBack.Location = new System.Drawing.Point(247, 7);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 25);
            this.btnBack.TabIndex = 17;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtChat
            // 
            this.txtChat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtChat.Location = new System.Drawing.Point(166, 36);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(216, 22);
            this.txtChat.TabIndex = 13;
            // 
            // btnSendChat
            // 
            this.btnSendChat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSendChat.Location = new System.Drawing.Point(388, 35);
            this.btnSendChat.Name = "btnSendChat";
            this.btnSendChat.Size = new System.Drawing.Size(54, 25);
            this.btnSendChat.TabIndex = 14;
            this.btnSendChat.Text = "Send";
            this.btnSendChat.UseVisualStyleBackColor = true;
            this.btnSendChat.Click += new System.EventHandler(this.btnSendChat_Click);
            // 
            // btnTest
            // 
            this.btnTest.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTest.Location = new System.Drawing.Point(328, 7);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(54, 25);
            this.btnTest.TabIndex = 16;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(163, 61);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(51, 14);
            this.lblMessage.TabIndex = 6;
            this.lblMessage.Text = "Message";
            // 
            // btnAuto
            // 
            this.btnAuto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAuto.Location = new System.Drawing.Point(388, 7);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(54, 25);
            this.btnAuto.TabIndex = 15;
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // chkDebug
            // 
            this.chkDebug.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chkDebug.AutoSize = true;
            this.chkDebug.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDebug.Location = new System.Drawing.Point(388, 60);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(57, 18);
            this.chkDebug.TabIndex = 12;
            this.chkDebug.Text = "Debug";
            this.chkDebug.UseVisualStyleBackColor = true;
            this.chkDebug.CheckedChanged += new System.EventHandler(this.chkDebug_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReset.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReset.Location = new System.Drawing.Point(166, 7);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 25);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Replay";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // picHomePlayer
            // 
            this.picHomePlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picHomePlayer.Location = new System.Drawing.Point(23, 22);
            this.picHomePlayer.Name = "picHomePlayer";
            this.picHomePlayer.Size = new System.Drawing.Size(36, 36);
            this.picHomePlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picHomePlayer.TabIndex = 2;
            this.picHomePlayer.TabStop = false;
            // 
            // picAwayPlayer
            // 
            this.picAwayPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picAwayPlayer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picAwayPlayer.Location = new System.Drawing.Point(461, 22);
            this.picAwayPlayer.Name = "picAwayPlayer";
            this.picAwayPlayer.Size = new System.Drawing.Size(36, 36);
            this.picAwayPlayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picAwayPlayer.TabIndex = 2;
            this.picAwayPlayer.TabStop = false;
            // 
            // bgwListener
            // 
            this.bgwListener.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwListener_DoWork);
            // 
            // bgwClient
            // 
            this.bgwClient.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwClient_DoWork);
            // 
            // tipChat
            // 
            this.tipChat.IsBalloon = true;
            this.tipChat.ShowAlways = true;
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.btnToggleFullscreen);
            this.pnlHeader.Location = new System.Drawing.Point(615, 32);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(231, 90);
            this.pnlHeader.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(130, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 35);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnToggleFullscreen
            // 
            this.btnToggleFullscreen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleFullscreen.Location = new System.Drawing.Point(13, 19);
            this.btnToggleFullscreen.Name = "btnToggleFullscreen";
            this.btnToggleFullscreen.Size = new System.Drawing.Size(111, 35);
            this.btnToggleFullscreen.TabIndex = 5;
            this.btnToggleFullscreen.Text = "Toggle Fullscreen";
            this.btnToggleFullscreen.UseVisualStyleBackColor = true;
            this.btnToggleFullscreen.Click += new System.EventHandler(this.btnToggleFullscreen_Click);
            // 
            // Match
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(887, 681);
            this.Controls.Add(this.pnlPlayers);
            this.Controls.Add(this.pnlBoard);
            this.Controls.Add(this.pnlHeader);
            this.DoubleBuffered = true;
            this.Name = "Match";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Match (Alpha 20180324)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Match_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Match_FormClosed);
            this.Load += new System.EventHandler(this.Match_Load);
            this.Resize += new System.EventHandler(this.Match_Resize);
            this.pnlPlayers.ResumeLayout(false);
            this.pnlPlayers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHomePlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAwayPlayer)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBoard;
        private System.Windows.Forms.Timer tmrAi;
        private System.Windows.Forms.Label lblHomePlayer;
        private System.Windows.Forms.Label lblAwayPlayer;
        private System.Windows.Forms.PictureBox picHomePlayer;
        private System.Windows.Forms.PictureBox picAwayPlayer;
        private System.Windows.Forms.Panel pnlPlayers;
        private System.Windows.Forms.Button btnReset;
        public System.ComponentModel.BackgroundWorker bgwListener;
        public System.ComponentModel.BackgroundWorker bgwClient;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Button btnSendChat;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.ToolTip tipChat;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnToggleFullscreen;
        private System.Windows.Forms.Button btnClose;
    }
}