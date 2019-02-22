using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Gomoku
{
    public partial class Match : Form
    {
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.HighQualityBicubic;
        // Constants
        public const int LENGTH = 15;
        // Players
        public Player homePlayer = new Player();
        public Player awayPlayer = new Player();
        public Player turnPlayer = null;
        // Stones
        Stone[,] stones;
        // Gameplay
        public int mode = PlayMode.AI;
        public bool debug = true;
        // UI
        public int uiStoneMarginBase = 1;
        public int uiStoneSizeBase = 36;
        public int uiBoardSizeBase = 585;
        public int uiBoardPaddingBase = 15;
        public int uiIndicatorSizeBase = 24;
        public ImageList imageList = new ImageList();
        public int uiBoardSizeLast = 0;
        // Network
        public string localIP = "";
        public int localPort = 0;
        public Match()
        {
            InitializeComponent();
        }

        // Initialize Form
        private void Match_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            int uiBoardSize = (this.ClientSize.Width > this.ClientSize.Height - pnlPlayers.Height ? this.ClientSize.Height - pnlPlayers.Height : this.ClientSize.Width);

            this.BackColor = Color.FromArgb(198, 186, 172);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImage = Properties.Resources.Background_Filter;

            stones = CreateStones(LENGTH, LENGTH);
            DrawBoard(uiBoardSize); //uiBoardSizeBase
            //this.ClientSize = new Size(uiBoardSizeBase, uiBoardSizeBase + 90);

            pnlPlayers.BackgroundImage = Properties.Resources.Header;
            pnlHeader.BackgroundImage = Properties.Resources.Header_Transparent;
            pnlHeader.Dock = DockStyle.Top;

            // For debug
            homePlayer.PlayerId = 1;
            homePlayer.Type = PlayerType.Local;
            homePlayer.Color = 1;
            if (homePlayer.DisplayName == "") homePlayer.DisplayName = "Tester";

            awayPlayer.PlayerId = 2;
            awayPlayer.Type = PlayerType.Ai;
            awayPlayer.Color = 2;
            awayPlayer.Ai = new Ai();
            awayPlayer.Ai.Stones = stones;
            awayPlayer.Ai.DebugMode = chkDebug.Checked;

            DisplayUsers();

            btnTest.Enabled = mode == PlayMode.AI;
            btnAuto.Enabled = mode == PlayMode.AI;
            chkDebug.Enabled = mode == PlayMode.AI;
            txtChat.Enabled = mode == PlayMode.Network;
            btnSendChat.Enabled = mode == PlayMode.Network;
            
            imageList.ImageSize = new Size(uiIndicatorSizeBase, uiIndicatorSizeBase);
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.Images.Add(Properties.Resources.Indicator);
        }

        private void InitializeMatch()
        {

        }

        private void DrawBoard(int size)
        {
            pnlBoard.Width = size;
            pnlBoard.Height = size;
            pnlBoard.Left = (this.ClientSize.Width - pnlBoard.Width) / 2;
            pnlPlayers.Width = size;
            pnlPlayers.Left = pnlBoard.Left;
            pnlBoard.BackgroundImage = chkDebug.Checked ? Properties.Resources.BoardDebug : Properties.Resources.Board;
            pnlBoard.Visible = true;
            DrawStones();
        }

        private void DrawStones()
        {
            double uiBoardSize = pnlBoard.Width;
            double uiRatio = uiBoardSize / (double)uiBoardSizeBase;
            double uiBoardPadding = (double)uiBoardPaddingBase * uiRatio;
            double uiStoneMargin = (double)uiStoneMarginBase * uiRatio;
            //int uiStoneSize = (int)((pnlBoard.Width - (uiBoardPadding * 2)) / LENGTH - uiStoneMargin);
            double uiStoneSize = (double)uiStoneSizeBase * uiRatio;
            double uiIndicatorSize = (double)uiIndicatorSizeBase * uiRatio;
            //imageList.ImageSize = new Size((int)uiIndicatorSize, (int)uiIndicatorSize);
            //imageList.Images.Clear();
            //imageList.Images.Add(Properties.Resources.Indicator);

            pnlBoard.Visible = false;
            for (int row = 0; row < LENGTH; row++)
            {
                for (int col = 0; col < LENGTH; col++)
                {
                    stones[row, col].Width = (int)uiStoneSize;
                    stones[row, col].Height = (int)uiStoneSize;
                    stones[row, col].Top = (int)((uiStoneSize + uiStoneMargin) * row + uiBoardPadding);
                    stones[row, col].Left = (int)((uiStoneSize + uiStoneMargin) * col + uiBoardPadding);
                    stones[row, col].Font = new Font(stones[row, col].Font.FontFamily, ((int)(8 * uiRatio) > 0 ? (int)(8 * uiRatio) : 1), FontStyle.Bold);
                }
            }
            pnlBoard.Visible = true;
        }

        // Click a spot (Client)
        private void stones_Click(object sender, EventArgs e)
        {
            int row = ((Stone)sender).Row;
            int col = ((Stone)sender).Column;

            if (stones[row, col].IsEmpty && turnPlayer != null &&
                (turnPlayer == homePlayer || mode == PlayMode.One) &&
                stones[row, col].IsAllowedFor(turnPlayer.Color))
            {
                if (mode == PlayMode.AI)
                {
                    Put(row, col, turnPlayer);

                    Random timer = new Random();
                    tmrAi.Interval = timer.Next(500, 1000);
                    tmrAi.Start();
                }
                else if (mode == PlayMode.Network)
                {
                    // If Connected
                    AsyncClient.Initialize("Put:" + row.ToString() + "," + col.ToString());

                    Put(row, col, turnPlayer);
                }
                else
                {
                    Put(row, col, turnPlayer);
                }
            }
        }

        // Mouse Hover (Client)
        private void stones_MouseEnter(object sender, EventArgs e)
        {
            int row = ((Stone)sender).Row;
            int col = ((Stone)sender).Column;

            if (((Stone)sender).Color == 0 && (turnPlayer == homePlayer || mode == PlayMode.One))
            {
                if (homePlayer.Color == 1)
                {
                    stones[row, col].BackgroundImage = Properties.Resources.ImgStoneBlack_Transparent;
                }
                else if (homePlayer.Color == 2)
                {
                    stones[row, col].BackgroundImage = Properties.Resources.ImgStoneWhite_Transparent;
                }
            }
        }

        // Mouse Leave (Client)
        private void stones_MouseLeave(object sender, EventArgs e)
        {
            int row = ((Stone)sender).Row;
            int col = ((Stone)sender).Column;

            if (((Stone)sender).Color == 0 && (turnPlayer == homePlayer || mode == PlayMode.One))
            {
                stones[row, col].BackgroundImage = null;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (mode == PlayMode.AI)
            {
                ResetGame();

                if (homePlayer.Color == 1)
                {
                    homePlayer.Color = 2;
                    awayPlayer.Color = 1;

                    turnPlayer = awayPlayer;
                    tmrAi.Interval = 300;
                    tmrAi.Start();

                    //Put(7, 7, awayPlayer);
                    //turnPlayer = homePlayer;
                }
                else
                {
                    homePlayer.Color = 1;
                    awayPlayer.Color = 2;

                    turnPlayer = homePlayer;
                }
                DisplayUsers();
            }
            else if (mode == PlayMode.Network)
            {
                // If Connected
                AsyncClient.Initialize("ReplayRequest:");
            }
            else
            {
                ResetGame();
                turnPlayer = homePlayer;
            }
        }

        public void ResetGame()
        {
            foreach (Stone stone in stones)
            {
                stone.Erase();
            }
        }

        private void tmrAi_Tick(object sender, EventArgs e)
        {
            if (turnPlayer == awayPlayer)
            {
                Put(awayPlayer.Ai.Pick(awayPlayer.Color), awayPlayer);
                tmrAi.Stop();
                tmrAi.Dispose();
            }
        }

        private void bgwListener_DoWork(object sender, DoWorkEventArgs e)
        {
            AsyncServer.Initialize();
        }

        private void bgwClient_DoWork(object sender, DoWorkEventArgs e)
        {
            AsyncClient.Initialize("Setup:" + homePlayer.DisplayName + "," + homePlayer.Color.ToString() + "," + localIP + "," + localPort.ToString());
        }

        private void chkDebug_CheckedChanged(object sender, EventArgs e)
        {
            awayPlayer.Ai.DebugMode = ((CheckBox)sender).Checked;

            if (!((CheckBox)sender).Checked)
            {
                foreach (Stone stone in stones)
                {
                    stone.Text = "";
                }
                pnlBoard.BackgroundImage = Properties.Resources.Board;
            }
            else
            {
                pnlBoard.BackgroundImage = Properties.Resources.BoardDebug;
            }
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            if (mode == PlayMode.Network)
            {
                //If connected
                AsyncClient.Initialize("Chat:" + txtChat.Text);
                txtChat.Text = "";
            }
        }

        private void Match_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (mode == PlayMode.Network)
            {
                //If connected
                AsyncClient.Initialize("Out:");
            }
        }


        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (turnPlayer == homePlayer)
            {
                Put(awayPlayer.Ai.Pick(homePlayer.Color), homePlayer);

                Random timer = new Random();
                tmrAi.Interval = timer.Next(500, 1000);
                tmrAi.Start();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            turnPlayer = homePlayer;

            for (int i = 0; i < 225; i++)
            {
                Put(awayPlayer.Ai.Pick(turnPlayer.Color), turnPlayer);
                if (turnPlayer == null) break;
            }
        }

        private void Match_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.Exit();
        }

        private void Match_Resize(object sender, EventArgs e)
        {
            pnlBoard.Left = (this.ClientSize.Width - pnlBoard.Width) / 2;
            pnlPlayers.Left = pnlBoard.Left;

            int uiBoardSize = (this.ClientSize.Width > this.ClientSize.Height - pnlPlayers.Height ? this.ClientSize.Height - pnlPlayers.Height : this.ClientSize.Width);

            if (uiBoardSize != uiBoardSizeLast)
            {
                pnlBoard.Visible = false;
                uiBoardSizeLast = uiBoardSize;
                if (stones != null) DrawBoard(uiBoardSize);
            }

            //this.BackgroundImage = Properties.Resources.Background_Filter;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Title_Flash title_flash = new Title_Flash();
            title_flash.Show();
            this.Close();
        }

        private void lblAwayPlayer_SizeChanged(object sender, EventArgs e)
        {
            ((Label)sender).Left = pnlPlayers.Width - ((Label)sender).Width - 20;
            picAwayPlayer.Left = ((Label)sender).Left - picAwayPlayer.Width - 6;
        }

        private void btnToggleFullscreen_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.ClientSize = new Size(uiBoardSizeBase, uiBoardSizeBase + 90);
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
