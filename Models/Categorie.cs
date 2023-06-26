using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoolApi.Models
{
    public class Categorie
    {
        [Key]
        public int IdCateg { get; set; }

        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string ImageCat { get; set; } = "";

        public int Active { get; set; }
    }
}