using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CoolApi.Models
{
    public class Country
    {
    [Key]
    public int  IdCountry{ get; set; }
	public string Title{ get; set; }="";
	public string Flag{ get; set; }="";

	public string Code{ get; set; }="";
	public string  PhoneCode{ get; set; }="";
	public int  Active{ get; set; }

    }
}