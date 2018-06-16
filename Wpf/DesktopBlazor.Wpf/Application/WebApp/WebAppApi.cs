using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    internal sealed class WebAppApi : IHttpApi
    {
        public string Name => "WebApp";
        private readonly string webAppDirectory;

        public WebAppApi(string webAppDirectory)
        {
            this.webAppDirectory = webAppDirectory;
        }

        public byte[] ProcessRequest(RequestUrl url)
        {
            var requestedLocalFile = url.Resource.Replace('/', '\\');
            if (requestedLocalFile == string.Empty) requestedLocalFile = "index.html";
            var bytes = File.ReadAllBytes(Path.Combine(webAppDirectory, requestedLocalFile));
            return bytes;
        }
    }
}
