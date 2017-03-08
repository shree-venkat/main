using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model
{
    public class FileContent
    {
        [Key]
        public int Id { get; set; }

        public string SecurityCode { get; set; }

        public string FileName { get; set; }

        [NotMapped]
        public int FileSize { get; set; }

        public byte[] Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? UploadedOn { get; set; }
    }
}
