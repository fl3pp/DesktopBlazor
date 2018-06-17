using CefSharp;
using DesktopBlazor.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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

        public byte[] ProcessRequest(RequestUrl url)
        {
            if (url.Resource != "files.json")
                throw new InvalidOperationException("Api function unknown");

            var folderParameter = url.Parameters.SingleOrDefault(
                p => p.Key.Equals("directory", StringComparison.OrdinalIgnoreCase));

            if (folderParameter == null)
                throw new InvalidOperationException("Specify parameter directory");


            FilesRequestResponse response;
            try
            {
                response = new FilesRequestResponse { Files = fileSystem.GetFiles(folderParameter.Value).ToArray() };
            }
            catch (IOException exception)
            {
                response = new FilesRequestResponse { ErrorMessage = exception.Message };
            }

            return Encoding.UTF8.GetBytes(response.ToJson());
        }
    }
}
