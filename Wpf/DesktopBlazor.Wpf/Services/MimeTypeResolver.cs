using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    public interface IMimeTypeResolver
    {
        string GetMimeType(string file);
    }

    internal sealed class MimeTypeResolver : IMimeTypeResolver
    {
        public string GetMimeType(string file)
        {
            if (file == string.Empty) return "text/html";

            var fileExtensionStartIndex = file
                .Select((c, i) => new { Character = c, Index = i })
                .Last(c => c.Character == '.')
                .Index;
            var extension = file.Substring(fileExtensionStartIndex);

            switch (extension.ToUpper())
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
                    throw new NotImplementedException();
            }
        }
    }
}
