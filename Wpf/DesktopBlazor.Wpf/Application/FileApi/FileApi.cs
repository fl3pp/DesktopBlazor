using CefSharp;
using DesktopBlazor.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    public class FileApi : IHttpApi
    {
        public string Name => "FileApi";

        public IResourceHandler GetResourceHandler(string path)
        {
            var file = new File { Kind = FileKind.File, Path = "asdf" };
            var json = JsonConvert.SerializeObject(new[] { file });
            return ResourceHandler.FromString(json, mimeType: "application/javascript");
        }
    }
}
