using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopBlazor.Wpf
{
    internal sealed class App : Application
    {
        public IResourceHandlerFactory ResourceManager { get; }
        public string Url { get; }

        private App(IResourceHandlerFactory resourceManager, string url)
        {
            ResourceManager = resourceManager;
            Url = url;
        }

        public static App Create()
        {
            SetupCef();

            var resourceManager = new ResourceManager(
                "webapp/index.html",
                new IHttpApi[]
                {
                    new WebAppApi(System.IO.Path.GetDirectoryName(typeof(ResourceManager).Assembly.Location) + "\\dist", new MimeTypeResolver()),
                    new FileApi(),
                }
            );

            var url = "http://webapp/";

            return new App(resourceManager, url);
        }

        private static void SetupCef()
        {
            Cef.EnableHighDPISupport();

            var cefSettings = new CefSettings();
            cefSettings.JavascriptFlags = "--expose-wasm";
            cefSettings.RemoteDebuggingPort = 8000;
            Cef.Initialize(cefSettings);
        }
    }
}
