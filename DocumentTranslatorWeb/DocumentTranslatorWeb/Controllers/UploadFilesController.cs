using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentTranslatorWeb.Controllers
{
    public class UploadFilesController : Controller
    {
        private const string UPLOAD_DIR = "content";

        public IActionResult Index()
        {
            return View();
        }

#pragma warning disable SG0016 // Controller method is vulnerable to CSRF
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            long size = -1;
            string filePath = string.Empty;
            try
            {
                size = files.Sum(f => f.Length);

                // full path to file in temp location
                // var filePath = Path.GetTempFileName();

                filePath = string.Empty;

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        string fileName = Path.GetFileName(formFile.FileName);
                        filePath = Path.Combine(Utilities.StateHelper.HostingEnvironment.WebRootPath, UPLOAD_DIR, fileName );

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }

                // process uploaded files
                // Don't rely on or trust the FileName property without validation.

                return Ok(new { count = files.Count, size, filePath });
            }
            catch ( Exception ex )
            {
                return Ok(new { status = "exception caught" + ex.ToString(), size, filePath });
            }
        }
#pragma warning restore SG0016 // Controller method is vulnerable to CSRF
    }
}