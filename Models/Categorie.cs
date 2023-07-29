using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolApi.Models
{
    public class Categorie
    {
        [Key]
        public int IdCateg { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public byte[] ImageCat { get; set; } 

        public int Active { get; set; }
    }
}