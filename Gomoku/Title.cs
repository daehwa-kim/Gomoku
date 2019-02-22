using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace Gomoku
{
    public partial class Title : Form
    {
        Panel pnlBackground = new Panel();
        Panel pnlContents = new Panel();
        PictureBox picContentsAiPlay = new PictureBox();
        PictureBox picContentsNetPlay = new PictureBox();
        PictureBox picContentsOptions = new PictureBox();
        PictureBox picContentsReplay = new PictureBox();
        Label lblDescription = new Label();
        public Title()
        {
            InitializeComponent();
        }

        private void Title_Load(object sender, EventArgs e)
        {
            this.Width = 1024;
            this.Height = 768;

            this.Controls.Add(pnlBackground);
            pnlBackground.BackgroundImageLayout = ImageLayout.Stretch;
            pnlBackground.BackgroundImage = Properties.Resources.Background;

            pnlBackground.Controls.Add(pnlContents);
            pnlContents.BackColor = Color.Transparent;
            pnlContents.BackgroundImageLayout = ImageLayout.Zoom;
            pnlContents.BackgroundImage = Properties.Resources.Title;

            pnlContents.Controls.Add(lblDescription);

            pnlContents.Controls.Add(picContentsAiPlay);
            picContentsAiPlay.BackgroundImageLayout = ImageLayout.Zoom;
            picContentsAiPlay.BackgroundImage = Properties.Resources.Title_AiPlay;
            picContentsAiPlay.MouseEnter += Menu_MouseEnter;
            picContentsAiPlay.MouseLeave += Menu_MouseLeave;
            picContentsAiPlay.MouseDown += Menu_MouseDown;
            picContentsAiPlay.MouseUp += Menu_MouseUp;

            pnlContents.Controls.Add(picContentsNetPlay);
            picContentsNetPlay.BackgroundImageLayout = ImageLayout.Zoom;
            picContentsNetPlay.BackgroundImage = Properties.Resources.Title_NetPlay;
            picContentsNetPlay.MouseEnter += Menu_MouseEnter;
            picContentsNetPlay.MouseLeave += Menu_MouseLeave;
            picContentsNetPlay.MouseDown += Menu_MouseDown;
            picContentsNetPlay.MouseUp += Menu_MouseUp;

            pnlContents.Controls.Add(picContentsOptions);
            picContentsOptions.BackgroundImageLayout = ImageLayout.Zoom;
            picContentsOptions.BackgroundImage = Properties.Resources.Title_Options;
            picContentsOptions.MouseEnter += Menu_MouseEnter;
            picContentsOptions.MouseLeave += Menu_MouseLeave;
            picContentsOptions.MouseDown += Menu_MouseDown;
            picContentsOptions.MouseUp += Menu_MouseUp;

            pnlContents.Controls.Add(picContentsReplay);
            picContentsReplay.BackgroundImageLayout = ImageLayout.Zoom;
            picContentsReplay.BackgroundImage = Properties.Resources.Title_Replay;
            picContentsReplay.MouseEnter += Menu_MouseEnter;
            picContentsReplay.MouseLeave += Menu_MouseLeave;
            picContentsReplay.MouseDown += Menu_MouseDown;
            picContentsReplay.MouseUp += Menu_MouseUp;

            //File.WriteAllBytes(@"Lato_Regular.ttf", Properties.Resources.Lato_Regular);
            
            //PrivateFontCollection modernFont = new PrivateFontCollection();
            //modernFont.AddFontFile(@"Lato_Regular.ttf");

            //lblDescription.Font = new Font(modernFont.Families[0], 12);
            lblDescription.Font = new Font("Arial", 12);
        }

        private void Title_Resize(object sender, EventArgs e)
        {
            pnlBackground.Visible = false;

            int squareMax = (this.ClientSize.Width > this.ClientSize.Height) ? this.ClientSize.Width : this.ClientSize.Height;
            int squareMin = (this.ClientSize.Width < this.ClientSize.Height) ? this.ClientSize.Width : this.ClientSize.Height;

            pnlBackground.Width = squareMax;
            pnlBackground.Height = squareMax;

            pnlContents.Width = squareMin;
            pnlContents.Height = squareMin;
            pnlContents.Left = (squareMax - squareMin) / 2;

            lblDescription.Width = (int)(pnlContents.Height / 2);
            lblDescription.Height = (int)(pnlContents.Height / 6);
            lblDescription.Left = (int)(pnlContents.Height / 4);
            lblDescription.Top = (int)(pnlContents.Height * 11 / 20);
            lblDescription.BackColor = Color.Transparent;
            lblDescription.UseCompatibleTextRendering = true;

            picContentsAiPlay.Width = (int)(pnlContents.Height / 4.5);
            picContentsAiPlay.Height = (int)(pnlContents.Height / 4.5);
            picContentsAiPlay.Left = 0;
            picContentsAiPlay.Top = (int)(pnlContents.Height * 5 / 9);

            picContentsNetPlay.Width = (int)(pnlContents.Height / 4.5);
            picContentsNetPlay.Height = (int)(pnlContents.Height / 4.5);
            picContentsNetPlay.Left = (int)(pnlContents.Height / 6);
            picContentsNetPlay.Top = picContentsAiPlay.Top + (int)(picContentsAiPlay.Height * 0.9);

            picContentsOptions.Width = (int)(pnlContents.Height / 4.5);
            picContentsOptions.Height = (int)(pnlContents.Height / 4.5);
            picContentsOptions.Left = pnlContents.Width - picContentsOptions.Width;
            picContentsOptions.Top = (int)(pnlContents.Height * 5 / 9);

            picContentsReplay.Width = (int)(pnlContents.Height / 4.5);
            picContentsReplay.Height = (int)(pnlContents.Height / 4.5);
            picContentsReplay.Left = pnlContents.Width - picContentsNetPlay.Left - picContentsReplay.Width;
            picContentsReplay.Top = picContentsOptions.Top + (int)(picContentsOptions.Height * 0.9);

            if (this.Height > this.Width)
            {
                pnlContents.Left = 0;
                pnlContents.Top = (squareMax - squareMin) / 2;
            }
            else
            {
                pnlContents.Left = (squareMax - pnlContents.Width) / 2;
                pnlContents.Top = 0;
            }

            if (false) //this.WindowState == FormWindowState.Maximized
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.Bounds = Screen.PrimaryScreen.Bounds;
                this.TopMost = true;
            }

            pnlBackground.Visible = true;
        }

        private void Menu_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = Properties.Resources.Title_AiPlay_Over;
            lblDescription.Text = "Play against the computer!";
        }

        private void Menu_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = Properties.Resources.Title_AiPlay;
            lblDescription.Text = "";
        }

        private void Menu_MouseDown(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = Properties.Resources.Title_AiPlay_Down;
        }

        private void Menu_MouseUp(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackgroundImage = Properties.Resources.Title_AiPlay_Over;
            Match match = new Match();
            match.Show();
        }

        private void DrawImage(Image image, object square, PaintEventArgs e)
        {
            int width = image.Width;
            int height = image.Height;
            int squareMax = (((Panel)square).Width > ((Panel)square).Height) ? ((Panel)square).Width : ((Panel)square).Height;
            int squareMin = (((Panel)square).Width > ((Panel)square).Height) ? ((Panel)square).Width : ((Panel)square).Height;
            int margin = (squareMax - squareMin) / 2;

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImage(image,
                new Rectangle(0, 0, squareMax, squareMax),
                0, 0, width, height, GraphicsUnit.Pixel);
        }
    }
}
