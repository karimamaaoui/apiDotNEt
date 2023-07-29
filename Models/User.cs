using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

namespace CoolApi.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string email { get; set; } = "";
        public string password { get; set; } = "";
        public string firstname { get; set; } = "";
        public string lastname { get; set; } = "";
        public string phone { get; set; } = "";
        public string country { get; set; } = "";
        public string address { get; set; } = "";
        public string role { get; set; } = "User";
        public DateTime DateInscription { get; set; } = DateTime.Today;
        public int active { get; set; } = 0;
        public string RefreshToken { get; set; } ="";


    }
}
