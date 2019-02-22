using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Gomoku
{
    public partial class Match
    {
        int lastPutRow = 0;
        int lastPutColumn = 0;

        public Stone[,] CreateStones(int rowLength, int colLength)
        {
            Stone[,] stones = new Stone[rowLength, colLength];

            // Initialize stones
            for (int row = 0; row < rowLength; row++)
            {
                for (int col = 0; col < colLength; col++)
                {
                    stones[row, col] = new Stone();
                    //stones[row, col].Width = uiStoneSize;
                    //stones[row, col].Height = uiStoneSize;
                    //stones[row, col].Top = (uiStoneSize + uiStoneMargin) * row + uiBoardPadding;
                    //stones[row, col].Left = (uiStoneSize + uiStoneMargin) * col + uiBoardPadding;
                    stones[row, col].BackColor = Color.Transparent; //
                    stones[row, col].BackgroundImage = null;
                    stones[row, col].BackgroundImageLayout = ImageLayout.Zoom;
                    stones[row, col].ImageList = imageList;
                    stones[row, col].FlatStyle = FlatStyle.Flat;
                    stones[row, col].FlatAppearance.BorderSize = 0;
                    stones[row, col].FlatAppearance.MouseOverBackColor = Color.Transparent;
                    stones[row, col].FlatAppearance.MouseDownBackColor = Color.Transparent;
                    stones[row, col].Text = "";
                    stones[row, col].Row = row;
                    stones[row, col].Column = col;
                    stones[row, col].Click += stones_Click;
                    stones[row, col].MouseEnter += stones_MouseEnter;
                    stones[row, col].MouseLeave += stones_MouseLeave;
                    pnlBoard.Controls.Add(stones[row, col]);
                }
            }

            return stones;
        }

        // Put a stone by row and column
        public void Put(int row, int col, Player player, bool turnOver = true)
        {
            if (Stone.Exists(row, col))
            {
                stones[row, col].SetPlayer(player);
                stones[lastPutRow, lastPutColumn].Image = null;
                if (!chkDebug.Checked) stones[row, col].Image = Properties.Resources.Indicator;
                //if (!chkDebug.Checked) stones[row, col].ImageIndex = 0;

                lastPutRow = row;
                lastPutColumn = col;
                CheckWin(row, col, player);
                if (turnOver && turnPlayer != null) turnPlayer = (player == homePlayer) ? awayPlayer : homePlayer;
                if (turnPlayer == homePlayer)
                {
                    picHomePlayer.Image = Properties.Resources.Indicator;
                    picAwayPlayer.Image = null;
                }
                else if (turnPlayer == awayPlayer)
                {
                    picHomePlayer.Image = null;
                    picAwayPlayer.Image = Properties.Resources.Indicator;
                }
            }
        }

        // Put a stone by stone object
        public void Put(Stone stone, Player player, bool turnOver = true)
        {
            if (stone != null)
            {
                stone.SetPlayer(player);
                stones[lastPutRow, lastPutColumn].Image = null;
                if (!chkDebug.Checked) stone.Image = Properties.Resources.Indicator;
                //if (!chkDebug.Checked) stone.ImageIndex = 0;
                lastPutRow = stone.Row;
                lastPutColumn = stone.Column;
                CheckWin(stone.Row, stone.Column, player);
                if (turnOver && turnPlayer != null) turnPlayer = (player == homePlayer) ? awayPlayer : homePlayer;
                if (turnPlayer == homePlayer)
                {
                    picHomePlayer.Image = Properties.Resources.Indicator;
                    picAwayPlayer.Image = null;
                }
                else if (turnPlayer == awayPlayer)
                {
                    picHomePlayer.Image = null;
                    picAwayPlayer.Image = Properties.Resources.Indicator;
                }
            }
        }

        private void CheckWin(int headRow, int headCol, Player player)
        {
            bool win = false;
            int count = 5;
            int empty = 0;

            for (int direction = 0; direction <= 3; direction++)
            {
                int v = (direction != SubsetDirection.Horizontal) ? 1 : 0;
                int h = (direction != SubsetDirection.Vertical) ? ((direction == SubsetDirection.ReverseDiagonal) ? -1 : 1) : 0;

                for (int i = 0; i < count; i++)
                {
                    int row = headRow - (v * i);
                    int col = headCol - (h * i);

                    if (IsComplete(row, col, direction, player.Color))
                    {
                        win = true;
                        for (int j = 0; j < count; j++)
                        {
                            if (!chkDebug.Checked)
                            {
                                stones[row + (v * j), col + (h * j)].Text = "";
                                stones[row + (v * j), col + (h * j)].Image = Properties.Resources.Indicator2;
                            }
                        }
                        break;
                    }
                }
            }

            

            if (win)
            {
                if (mode == PlayMode.AI)
                {
                    tmrAi.Stop();
                    if (player == homePlayer) MessageBox.Show("You Win!");
                    else if (player == awayPlayer) MessageBox.Show("You Lose!");
                }
                else
                {
                    MessageBox.Show(player.DisplayName + " Wins!");
                }
                turnPlayer = null;
            }
            else
            {
                foreach (Stone stone in stones)
                {
                    if (stone.Color == 0) empty++;
                }

                if (empty == 0)
                {
                    tmrAi.Stop();
                    MessageBox.Show("Draw!");
                    turnPlayer = null;
                }
            }
        }

        private bool IsComplete(int headRow, int headCol, int direction, int color)
        {
            bool isComplete = true;
            int count = 5;
            int v = (direction != SubsetDirection.Horizontal) ? 1 : 0;
            int h = (direction != SubsetDirection.Vertical) ? ((direction == SubsetDirection.ReverseDiagonal) ? -1 : 1) : 0;

            for (int i = 0; i < count; i++)
            {
                int row = headRow + (v * i);
                int col = headCol + (h * i);

                if (!Stone.Exists(row, col) || stones[row, col].Color != color)
                {
                    isComplete = false;
                    break;
                }
            }

            return isComplete;
        }

        // Take turns (Client)
        private void TurnOver(bool client)
        {
            turnPlayer = (turnPlayer == homePlayer) ? awayPlayer : (turnPlayer != null) ? homePlayer : null;

            if (client)
            {
                //pnlBoard.Enabled = true;
            }
            else
            {
                //pnlBoard.Enabled = false;
            }
        }

        private void DisplayUsers()
        {
            lblHomePlayer.Text = homePlayer.DisplayName;
            picHomePlayer.BackgroundImage = StoneImage.FromColor(homePlayer.Color);
            lblAwayPlayer.Text = awayPlayer.DisplayName;
            picAwayPlayer.BackgroundImage = StoneImage.FromColor(awayPlayer.Color);
        }

        public static string GetLocalIPAddress()
        {
            string result = "";
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        result = ip.ToString();
                        break;
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception)
            {
               
            }

            return result;
        }
    }
}
