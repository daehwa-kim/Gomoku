using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Title_Flash : Form
    {
        public Title_Flash()
        {
            InitializeComponent();
        }

        private void Title_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.Bounds = Screen.PrimaryScreen.Bounds;

            //this.Width = 1024;
            //this.Height = 768;
            
            File.WriteAllBytes(Path.GetTempPath() + "TitleScreen.swf", Properties.Resources.TitleScreen);
            File.WriteAllBytes(Path.GetTempPath() + "TitleScreen.html", Properties.Resources.TitleScreen1);
            WebBrowser title = new WebBrowser();
            title.ScrollBarsEnabled = false;
            title.IsWebBrowserContextMenuEnabled = false;
            title.ScriptErrorsSuppressed = false;
            title.WebBrowserShortcutsEnabled = false;
            title.Navigate("file:///" + Path.GetTempPath().Replace("\\", "/") + "TitleScreen.html");
            this.Controls.Add(title);
            //title.Bounds = this.Bounds;
            //title.Top = 0;
            //title.Left = 0;
            title.Dock = DockStyle.Fill;
            title.Navigated += title_Navigated;
        }

        private void title_Navigated(object sender, EventArgs e)
        {
            string url = ((WebBrowser)sender).Url.OriginalString;
            string command = url.Substring(url.IndexOf("#") + 1);

            if (command == "AiPlay")
            {
                Match match = new Match();
                match.turnPlayer = match.homePlayer;
                match.awayPlayer.DisplayName = "CPU";

                match.Show();
                this.Close();
            }
            else if (command == "NetPlay")
            {
                Network network = new Network();
                network.Owner = this;
                network.ShowDialog();
            }
            else if (command == "Close")
            {
                this.Close();
            }
        }

        private void Title_Resize(object sender, EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Maximized)
            //{
            //    this.FormBorderStyle = FormBorderStyle.None;
            //    this.WindowState = FormWindowState.Normal;
            //    this.Bounds = Screen.PrimaryScreen.Bounds;
            //    this.TopMost = true;
            //}
        }

        private void Title_Flash_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.Exit();
        }
    }
}
