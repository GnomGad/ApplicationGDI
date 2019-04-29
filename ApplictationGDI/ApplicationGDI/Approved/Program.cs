using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using App = ApplicationGDI.Source.App;
using ApplicationGDI.Source.Forms;
namespace ApplicationGDI
{
    static class Program
    {
        public const string PathImages = @"..\..\Source\Images";
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //App app = new App();
            //app.GetPicterBox(app.GetPathFile());
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CardsGame());
        }
    }
}
