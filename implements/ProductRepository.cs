using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolApi.Interfaces;
using CoolApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoolApi.implements
{
    public class ProductRepository : IProductInterface
    {

        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }
        public Task Add(Product model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }


        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product model)
        {
            throw new NotImplementedException();
        }
    }
}