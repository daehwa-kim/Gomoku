using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Network : Form
    {
        public Network()
        {
            InitializeComponent();
        }

        private void Network_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry! Network Play is in development.");
            this.Close();
            //txtLocalIP.Text = Match.GetLocalIPAddress();
        }

        private void btnOpenServer_Click(object sender, EventArgs e)
        {
            Match match = new Match();
            match.mode = PlayMode.Network;
            match.localIP = txtLocalIP.Text;
            match.localPort = int.Parse(txtLocalPort.Text);

            // Open Server
            Match.AsyncServer.ParentForm = match;
            Match.AsyncServer.Address = txtLocalIP.Text;
            Match.AsyncServer.Port = int.Parse(txtLocalPort.Text);
            match.bgwListener.RunWorkerAsync();

            // User Setup
            match.homePlayer.DisplayName = txtNickname.Text;
            match.awayPlayer.DisplayName = "Waiting...";

            // Open Match
            match.Show();
            this.Owner.Close();
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Match match = new Match();
            match.mode = PlayMode.Network;
            match.localIP = txtLocalIP.Text;
            match.localPort = int.Parse(txtLocalPort.Text);

            // Open Server
            Match.AsyncServer.ParentForm = match;
            Match.AsyncServer.Address = txtLocalIP.Text;
            Match.AsyncServer.Port = int.Parse(txtLocalPort.Text);
            match.bgwListener.RunWorkerAsync();

            // User Setup
            Random random = new Random();
            match.homePlayer.Color = random.Next(1, 3);
            match.homePlayer.DisplayName = txtNickname.Text;
            if (match.homePlayer.Color == 1) match.turnPlayer = match.homePlayer;
            else if (match.homePlayer.Color == 2) match.turnPlayer = match.awayPlayer;

            // Start Client
            Match.AsyncClient.ParentForm = match;
            Match.AsyncClient.Address = txtRemoteIP.Text;
            Match.AsyncClient.Port = int.Parse(txtRemotePort.Text);
            match.bgwClient.RunWorkerAsync();

            // Open Match
            match.Show();
            this.Owner.Close();
            this.Close();
        }
    }
}
