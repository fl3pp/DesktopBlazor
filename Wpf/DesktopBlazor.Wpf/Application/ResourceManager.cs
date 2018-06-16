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
            var requestUrl = RequestUrl.FromString(request.Url);
            
            var api = GetApi(requestUrl.Api);
            var mimeType = mimeResolver.GetMimeType(Path.GetFileName(requestUrl.Resource));

            return ResourceHandler.FromByteArray(api.ProcessRequest(requestUrl), mimeType);
        }

        private IHttpApi GetApi(string apiName)
        {
            var api = apis.SingleOrDefault(a => a.Name.Equals(apiName, StringComparison.OrdinalIgnoreCase));

            if (api == null) throw new InvalidOperationException($"Api {apiName} could not be found");

            return api;
        }
    }
}
