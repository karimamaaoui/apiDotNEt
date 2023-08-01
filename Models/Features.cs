using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolApi.Models
{
      public class Features
    {
        [Key]
        public int IdF { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Unit { get; set; }
        public int Active { get; set; }

        public int idCategory { get; set; }
        [ForeignKey("idCategory")]
        public Categorie? Categorie { get; set; }
    }

}