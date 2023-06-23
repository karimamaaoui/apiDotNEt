using System.ComponentModel.DataAnnotations;

namespace CoolApi.Models{

public class Product {
    public int Id { get; set; }
    public string? name { get; set; }
    public decimal price { get; set; }
    public int qty { get; set; }
    

}
}