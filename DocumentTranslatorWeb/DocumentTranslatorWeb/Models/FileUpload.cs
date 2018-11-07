using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DocumentTranslatorWeb.Models
{
    public class FileUpload
    {
        [Required]
        [Display(Name ="Title")]
        [StringLength(256, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Display(Name ="Document")]
        public IFormFile UploadDocument { get; set; }
    }
}
