using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DocumentTranslatorWeb.Pages
{
    public class FormPostModel : PageModel
    {
        
        public IActionResult OnGet()
        {
            System.Diagnostics.Debug.WriteLine("!!! OnGet");
            return Page();
        }

        public IActionResult OnPost()
        {
            System.Diagnostics.Debug.WriteLine("!!! OnPost");
            return Page();
        }
    }
}