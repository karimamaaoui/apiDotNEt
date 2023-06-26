using System;
using System.ComponentModel.DataAnnotations;
namespace CoolApi.Models
{
    public class Ads
    {    
        [Key]
        public int IdAds { get; set; }

        public string Title { get; set;}="";
        public string Description { get; set; }=""; 
        public string details{ get; set; }="";
        public float Price { get; set; }
        public int IdPricesDelevery { get; set; }
        public DateTime DatePublication{ get; set; }
        public string ImagePrinciple{ get; set; }="";
        public string VideoName{ get; set; }="";
        public int IdCateg { get; set; }

        public int IdUser{ get; set; }
        public int IdPays{ get; set; }
        public int IdCity{ get; set; }
        public string Locations{ get; set; }="";
        public int IdBoost{ get; set; }
        public int Active{ get; set; }
    }

}