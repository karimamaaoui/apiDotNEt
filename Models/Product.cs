using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string title { get; set; } = "";
        public string CodeBar { get; set; } = "";

        public decimal price { get; set; }
        public string Description { get; set; } = "";
        public string Details { get; set; } = "";
        public string Color { get; set; } = "";
        public int Active { get; set; }
        public DateTime DatePublication { get; set; }= DateTime.Now;

        public int qty { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public byte[] imagePrinciple { get; set; }

        [NotMapped]
        public IFormFile VideoFile {get; set;}

        public byte[] VideoData { get; set; }

        public int IdCateg { get; set; }

    }
}
