using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    public interface IHttpApi
    {
        string Name { get; }
        IResourceHandler GetResourceHandler(string path);
    }
}
