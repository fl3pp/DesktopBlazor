using CefSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopBlazor.Wpf
{
    internal sealed class ResourceManager : IResourceHandlerFactory
    {
        public bool HasHandlers => true;
        private readonly string defaultPath;
        private readonly IHttpApi[] apis;
        private readonly IMimeTypeResolver mimeResolver;

        public ResourceManager(
            string defaultPath,
            IHttpApi[] apis,
            IMimeTypeResolver mimeResolver)
        {
            this.defaultPath = defaultPath;
            this.apis = apis;
            this.mimeResolver = mimeResolver;
        }

        public IResourceHandler GetResourceHandler(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
        {
            var requestUrl = request.Url.Substring(7);
            
            if (requestUrl == string.Empty) requestUrl = defaultPath;

            var api = GetApi(requestUrl);
            var mimeType = mimeResolver.GetMimeType(Path.GetFileName(requestUrl));

            return ResourceHandler.FromByteArray(api.ProcessRequest(requestUrl), mimeType);
        }

        private IHttpApi GetApi(string path)
        {
            var apiBasePath = new string(path.TakeWhile(c => c != '/').ToArray());

            var api = apis.SingleOrDefault(a => a.Name.Equals(apiBasePath, StringComparison.OrdinalIgnoreCase));

            if (api == null) throw new InvalidOperationException($"Api {apiBasePath} could not be found");

            return api;
        }
    }
}
