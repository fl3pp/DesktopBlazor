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
        private readonly IMimeTypeResolver mimeResolver;
        private readonly string webAppDirectory;

        public WebAppApi(
            string webAppDirectory,
            IMimeTypeResolver mimeResolver)
        {
            this.webAppDirectory = webAppDirectory;
            this.mimeResolver = mimeResolver;
        }

        public IResourceHandler GetResourceHandler(string path)
        {
            var requestedLocalFile = path.Substring(7).Trim('/').Replace('/', '\\');
            if (requestedLocalFile == string.Empty) requestedLocalFile = "index.html";
            var bytes = File.ReadAllBytes(Path.Combine(webAppDirectory, requestedLocalFile));
            return ResourceHandler.FromByteArray(bytes, mimeResolver.GetMimeType(Path.GetExtension(requestedLocalFile)));
        }
    }
}
