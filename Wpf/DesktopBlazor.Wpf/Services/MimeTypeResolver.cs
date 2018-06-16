using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    public interface IMimeTypeResolver
    {
        string GetMimeType(string fileExtension);
    }

    internal sealed class MimeTypeResolver : IMimeTypeResolver
    {
        public string GetMimeType(string fileExtension)
        {
            switch (fileExtension.ToUpper())
            {
                case ".HTML":
                    return "text/html";
                case ".CSS":
                    return "text/css";
                case ".WASM":
                    return "application/wasm";
                case ".JS":
                case ".JSON":
                    return "application/javascript";
                case ".DLL":
                    return "application/octet-stream";
                case ".WOFF":
                case ".WOFF2":
                    return "application/x-font-woff";
                default:
                    throw new NotSupportedException("extension not found");
            }
        }
    }
}
