using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoolApi.Services;
using CoolApi.Models;
using CoolApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CoolApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ProductContext _cxt;

        public ProductController(ProductContext context)
        {
            _cxt = context;
        }
        [HttpGet]
        [Route("GetProducts")]
        [Authorize]
        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _cxt.Products.ToListAsync();
            return products;
        }

        [HttpGet]
        [Route("GetProductsByPagination")]
        [Authorize]
        public async Task<IActionResult> GetAll(int page = 1, int pageSize = 3)
        {
            // Calculate the number of items to skip based on the current page and page size
            int skipCount = (page - 1) * pageSize;

            // Retrieve the products with pagination
            var products = await _cxt.Products
                .Skip(skipCount)
                .Take(pageSize)
                .ToListAsync();

            // Retrieve the total count of products
            int totalCount = await _cxt.Products.CountAsync();

            // Create a pagination response object
            var response = new
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Products = products
            };

            return Ok(response);
        }


        [HttpPost]
        [Route("AddProduct")]

        public IActionResult CreateProduct(Product product)
        {
            _cxt.Products.Add(product);
            _cxt.SaveChanges();

            return Ok();
        }

        [HttpPut]
        [Route("updateProducts")]
        public async Task<ActionResult<Product>> UpdateProducts(Product p)
        {
            var product = await _cxt.Products.FindAsync(p.Id);
            if (product == null)
            {
                return BadRequest("No product found!");
            }

            // Update the properties only if they are not null or empty in the update request
            if (!string.IsNullOrEmpty(p.name))
            {
                product.name = p.name;
            }

            if (p.price != 0)
            {
                product.price = p.price;
            }

            if (p.qty != 0)
            {
                product.qty = p.qty;
            }

            try
            {
                await _cxt.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Failed to update product.");
            }

            return Ok(product);
        }

        [HttpDelete]
        [Route("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _cxt.Products.FindAsync(id);
            if (product == null)
            {
                return BadRequest("no product found !");
            }




            _cxt.Products.Remove(product);
            await _cxt.SaveChangesAsync();

            return Ok(product);
        }

        [HttpGet]
        [Route("GetProduct/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _cxt.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return BadRequest("no product found !");
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("searchproduct")]
        [Authorize]
        public IActionResult SearchUser(string search)
        {
            string query = $"SELECT * FROM products WHERE name LIKE '%{search}%' OR price LIKE '%{search}%' OR qty LIKE '%{search}%';";
            var searchResults = _cxt.Products.FromSqlRaw(query).ToList();

            // Return the search results
            return Ok(searchResults);
        }



        /*
        public async Task<ActionResult<Product>> UpdateProducts(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            _cxt.Entry(product).State = EntityState.Modified;
            try
            {
                await _cxt.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return product;

        }

        
         private List<Product> products = new List<Product>()
         {
             new Product()
             {
                 Id = 1,
                 Name = "karima"
             },
             new Product()
             {
                 Id = 2,
                 Name = "aaa"
             }
         };

             [HttpGet]
             [Route("GetProducts")]

             public async Task<ActionResult<Product>> GetProducts(){


                 return Ok(products);

             }

             [HttpGet]
             [Route("GetProduct")]

             public async Task<ActionResult<Product>> GetProduct(int id){

                 var product=products.Find(x=>x.Id==id);
                 if (product==null){
                     return BadRequest("no product found !");

                 }
                 return Ok(product);

             }

             [HttpPost]
             [Route("addProducts")]

             public async Task<ActionResult<Product>> AddProducts(Product p){

                 products.Add(p);

                 return Ok(products);

             }


             [HttpPut]
             [Route("updateProducts")]

           public async Task<ActionResult<Product>> UpdateProducts(Product p){
             var product=products.Find(x=>x.Id==p.Id);
                 if (product==null){
                     return BadRequest("no product found !");

                 }
                 product.Id=p.Id;
                 product.Name=p.Name;

                 return Ok(products);

             }

             [HttpDelete]
             [Route("updateProducts")]

             public async Task<ActionResult<Product>> DeleteProducts(int id){

             var product=products.Find(x=>x.Id==id);
                 if (product==null){
                     return BadRequest("no product found !");

                 }
                 products.Remove(product);

                 return Ok(products);

             }
     */


    }
}