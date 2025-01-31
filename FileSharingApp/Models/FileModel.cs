using System;
using System.ComponentModel.DataAnnotations;

namespace FileSharingApp.Models
{
    public class FileModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        [Display(Name = "Upload Date")]
        public DateTime UploadDate { get; set; }
    }
}