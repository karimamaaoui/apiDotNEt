using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; } = "";

        public decimal price { get; set; }
        public int qty { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public byte[] imagePrinciple { get; set; }
    }
}
