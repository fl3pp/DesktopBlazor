using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesktopBlazor.Contracts
{

    public class FilesRequestResponse
    {
        public File[] Files { get; set; }
        public string ErrorMessage { get; set; }

        public static FilesRequestResponse FromJson(string json)
        {
            return JsonUtil.Deserialize<FilesRequestResponse>(json);
        }

        public string ToJson()
        {
            return JsonUtil.Serialize(this);
        }
    }
}
