using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopBlazor.Wpf
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = App.Create();

            var window = new MainWindow(app.ResourceManager, app.Url);

            app.Run(window);
        }

    
    }
}
