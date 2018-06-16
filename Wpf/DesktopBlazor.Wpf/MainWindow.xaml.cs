using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopBlazor.Wpf
{
    public partial class MainWindow : Window
    {
        private readonly string url;

        public MainWindow(IResourceHandlerFactory resourceManager, string url)
        {
            this.url = url;
            InitializeComponent();
            Browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged;
            Browser.ResourceHandlerFactory = resourceManager;
        }

        private void Browser_IsBrowserInitializedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Browser.Load(url);
        }
    }
}
