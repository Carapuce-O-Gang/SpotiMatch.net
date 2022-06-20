using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SpotiMatch.Database.Entities
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
