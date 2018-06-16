using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DesktopBlazor
{
    public class RequestUrl
    {
        private const string protocol = "http://";

        public string Api { get; }
        public string Resource { get; }
        public RequestParameter[] Parameters { get; }

        public RequestUrl(
            string api,
            string resource,
            RequestParameter[] parameters)
        {
            Api = api;
            Resource = resource;
            Parameters = parameters;
        }


        public static RequestUrl FromString(string str)
        {
            var withoutProtocol = str.SubstringOrEmpty(protocol.Length);

            var api = new string(withoutProtocol.TakeWhile(c => c != '/').ToArray());
            var withoutApi = withoutProtocol.SubstringOrEmpty(api.Length + 1);

            var path = new string(withoutApi.TakeWhile(c => c != '?').ToArray());
            var withoutPath = withoutApi.SubstringOrEmpty(path.Length + 1);

            var parameters = withoutPath
                .Split('&')
                .Select(p => p.Split('='))
                .Where(p => p.Count() == 2)
                .Select(p => new RequestParameter(p[0], HttpUtility.HtmlDecode(p[1])))
                .ToArray();

            return new RequestUrl(api, path, parameters);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append(protocol);
            builder.Append(Api);
            builder.Append('/');
            builder.Append(Resource);

            if (Parameters.Any())
            {
                builder.Append('?');
                builder.Append(string.Join("&", 
                    Parameters.Select(p => $"{p.Key}={HttpUtility.HtmlEncode(p.Value)}")));
            }

            return builder.ToString();
        }
    }

    public class RequestParameter
    {
        public string Key { get; }
        public string Value { get; }

        public RequestParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
