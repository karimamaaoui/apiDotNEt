using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolApi.Models;

namespace CoolApi.Services
{
    public class ProductService
    {   
        List<Product> _productList;
        public ProductService(){
            _productList=new List<Product>();

        }

        public List<Product>GetProducts(){
            return _productList;
        }

        public void AddProducts(Product p){
            _productList.Add(p);
        }
    }
}