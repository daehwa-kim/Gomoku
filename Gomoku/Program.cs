using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //File.WriteAllBytes(@"Lato_Regular.ttf", Properties.Resources.Lato_Regular);

            //PrivateFontCollection modernFont = new PrivateFontCollection();
            //modernFont.AddFontFile(@"Lato_Regular.ttf");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Title_Flash title_flash = new Title_Flash();
            //title_flash.Show();
            Match match = new Match();
            match.turnPlayer = match.homePlayer;
            match.awayPlayer.DisplayName = "CPU";
            match.Show();
            Application.Run();
        }
    }
}
