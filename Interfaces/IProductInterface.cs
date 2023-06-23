using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolApi.Models;

namespace CoolApi.Interfaces
{
    public interface IProductInterface
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product model);
        Task Update(Product model);
        Task Delete(int id);
    }
}