using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentTranslatorWeb.Utilities
{
    public static class StateHelper
    {
        public static IHostingEnvironment HostingEnvironment { get; set; } = null;
    }
}
