using Microsoft.AspNetCore.Hosting;
using System.Text;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace devfish
{
    public static class ExtensionMethods
    {
        public static string DebugString( this IHostingEnvironment env )
        {
            StringBuilder sb = new StringBuilder(1024);
            sb.Append("IHostingEnvironment: ");
            sb.Append($"{nameof(env.EnvironmentName)}: {env.EnvironmentName}; ");
            sb.Append($"IsDevelopment(): {env.IsDevelopment()}; ");
            sb.Append($"IsProduction(): {env.IsProduction()}; ");
            sb.Append($"IsStaging(): {env.IsStaging()}; ");
            sb.Append($"{nameof(env.ContentRootPath)}: {env.ContentRootPath}; ");
            sb.Append($"{nameof(env.WebRootPath)}: {env.WebRootPath}; ");

            return sb.ToString();
        }
    }
}
