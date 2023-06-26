using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoolApi.Models
{
    public class City
    {        
    [Key]
      public int IdCity { get; set; }
	public string Title { get; set; }="";
	public int IdCountry{ get; set; }
    }
}