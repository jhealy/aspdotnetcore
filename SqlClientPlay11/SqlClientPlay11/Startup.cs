using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;
using System.Text;

namespace SqlClientPlay11
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello devfish!");
                await context.Response.WriteAsync("<div><strong>SQLCLIENT DIRECT SQL</strong><br/>");
                await context.Response.WriteAsync(CustomerTable());
                await context.Response.WriteAsync("</div>");
                await context.Response.WriteAsync("<div style='font:smaller'><pre>");
                await context.Response.WriteAsync(GetOsPlatformDescription());
                await context.Response.WriteAsync("</pre></div>");
            });
        }

        public string CustomerTable()
        {
            string retval;
            string header = @"<strong>SqlDbReader Fetch</strong>";
            string footer = @"done...";

            StringBuilder sbBody = new StringBuilder(1024);
            List<Customer> list = CustomersRepository.GetAllCustomers();
            foreach (Customer c in list)
            {
                sbBody.AppendFormat(@"{0}::{1}<br/>", c.CustomerId, c.CompanyName);
            }
            retval = $@"{header}<br/>{sbBody}<br/>{footer}";
            return retval;
        }

        public string GetOsPlatformDescription()
        {
            StringBuilder retval = new StringBuilder(1024);
            OSPlatform os = GetOSPlatform();
            retval.AppendLine($"OSPlatform: {os.ToString()}");
            retval.AppendLine($"RTI.FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
            retval.AppendLine($"RTI.OSArchitecture: {RuntimeInformation.OSArchitecture}");
            retval.AppendLine($"RTI.OSDescription: {RuntimeInformation.OSDescription}");

            return retval.ToString();
        }

        public OSPlatform GetOSPlatform()
        {
            OSPlatform osPlatform = OSPlatform.Create("Other Platform");
            // Check if it's windows
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            osPlatform = isWindows ? OSPlatform.Windows : osPlatform;
            // Check if it's osx
            bool isOSX = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            osPlatform = isOSX ? OSPlatform.OSX : osPlatform;
            // Check if it's Linux
            bool isLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            osPlatform = isLinux ? OSPlatform.Linux : osPlatform;
            return osPlatform;
        }
    }
}