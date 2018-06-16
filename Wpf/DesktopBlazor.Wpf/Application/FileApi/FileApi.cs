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
        private readonly IFileSystem fileSystem;
        public string Name => "FileApi";

        public FileApi(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public byte[] ProcessRequest(string path)
        {
            return Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(fileSystem.GetFiles(path).ToArray()));
        }
    }
}
