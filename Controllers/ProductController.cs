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
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace CoolApi.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductController : ControllerBase
    {
        private readonly ProductContext _cxt;
        private readonly ILogger<ProductController> _logger;
        public static IWebHostEnvironment _webHostEnivronment;

        public ProductController(ProductContext context, IWebHostEnvironment webHostEnvironment)
        {
            _cxt = context;
            _webHostEnivronment = webHostEnvironment;

        }



        [HttpPost]
        [Route("AddProductWithFiles")]
        public async Task<string> CreateProductWithFiles([FromForm] Product productFiles)
        {
            try
            {
                if (productFiles.ImageFile != null && productFiles.ImageFile.Length > 0 && productFiles.VideoFile != null && productFiles.VideoFile.Length > 0)
                {
                    string imagePath = _webHostEnivronment.WebRootPath + "\\images\\";
                    string videoPath = _webHostEnivronment.WebRootPath + "\\videos\\";

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                    if (!Directory.Exists(videoPath))
                    {
                        Directory.CreateDirectory(videoPath);
                    }

                    string uniqueImageFileName = Guid.NewGuid().ToString() + "_" + productFiles.ImageFile.FileName;
                    string uniqueVideoFileName = Guid.NewGuid().ToString() + "_" + productFiles.VideoFile.FileName;

                    string imageFilePath = Path.Combine(imagePath, uniqueImageFileName);
                    string videoFilePath = Path.Combine(videoPath, uniqueVideoFileName);

                    using (FileStream imageFileStream = System.IO.File.Create(imageFilePath))
                    {
                        await productFiles.ImageFile.CopyToAsync(imageFileStream);
                        await imageFileStream.FlushAsync();
                    }

                    using (FileStream videoFileStream = System.IO.File.Create(videoFilePath))
                    {
                        await productFiles.VideoFile.CopyToAsync(videoFileStream);
                        await videoFileStream.FlushAsync();
                    }

                    // Convert the image file to a byte array
                    byte[] imageData;
                    using (MemoryStream imageMemoryStream = new MemoryStream())
                    {
                        await productFiles.ImageFile.CopyToAsync(imageMemoryStream);
                        imageData = imageMemoryStream.ToArray();
                    }

                    // Convert the video file to a byte array
                    byte[] videoData;
                    using (MemoryStream videoMemoryStream = new MemoryStream())
                    {
                        await productFiles.VideoFile.CopyToAsync(videoMemoryStream);
                        videoData = videoMemoryStream.ToArray();
                    }

                    // Assign the image and video data to the respective properties
                    Product product = new Product
                    {
                        // Assign other properties as needed
                        imagePrinciple = imageData,
                        VideoData = videoData
                    };

                    _cxt.Products.Add(product);
                    _cxt.SaveChanges();

                    return "Upload Done.";
                }
                else
                {
                    return "Failed: No file uploaded.";
                }
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}\nInner Exception: {e.InnerException}\nStack Trace: {e.StackTrace}";
            }
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
        [Route("AddProductimg")]
        public async Task<string> CreateProductWithImage([FromForm] Product product)
        {
            try
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string path = _webHostEnivronment.WebRootPath + "\\images\\";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(path, uniqueFileName);

                    using (FileStream fileStream = System.IO.File.Create(filePath))
                    {
                        await product.ImageFile.CopyToAsync(fileStream);
                        await fileStream.FlushAsync();
                    }

                    // Convert the image file to a byte array
                    byte[] imageData;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await product.ImageFile.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }

                    // Assign the image data to the imagePrinciple property
                    product.imagePrinciple = imageData;

                    _cxt.Products.Add(product);
                    _cxt.SaveChanges();

                    return "Upload Done.";
                }
                else
                {
                    return "Failed: No file uploaded.";
                }
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}\nInner Exception: {e.InnerException}\nStack Trace: {e.StackTrace}";
            }
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
            if (!string.IsNullOrEmpty(p.title))
            {
                product.title = p.title;
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

        [HttpGet]
        [Route("GetProduct")]
        public async Task<IActionResult> GetAllProducts(int page = 1, int pageSize = 3)
        {
            var skipCount = (page - 1) * pageSize;
            var products = await _cxt.Products.Skip(skipCount).Take(pageSize).ToListAsync();

            var responseList = new List<object>();

            foreach (var product in products)
            {
                // Convert product data to response format
                var response = new
                {
                    Id = product.Id,
                    Price = product.price,
                    ImageBase64 = Convert.ToBase64String(product.imagePrinciple),
                    VideoBase64 = Convert.ToBase64String(product.VideoData),
                    qty = product.qty,
                    Description = product.Description,
                    Details = product.Details,
                    title = product.title,
                    CodeBar = product.CodeBar,
                    Color = product.Color,
                    DatePublication = product.DatePublication
                };

                responseList.Add(response);
            }

            return Ok(responseList);
        }

        [HttpGet]
        [Route("GetProductAll")]
        [Authorize]

        public async Task<IActionResult> GetAllProduct()
        {

            var products = await _cxt.Products.ToListAsync();

            var responseList = new List<object>();

            foreach (var product in products)
            {
                // Convert product data to response format
                var response = new
                {
                    Id = product.Id,
                    Price = product.price,
                    ImageBase64 = Convert.ToBase64String(product.imagePrinciple),
                    VideoBase64 = Convert.ToBase64String(product.VideoData),
                    qty = product.qty,
                    Description = product.Description,
                    Details = product.Details,
                    title = product.title,
                    CodeBar = product.CodeBar,
                    Color = product.Color,
                    DatePublication = product.DatePublication
                };

                responseList.Add(response);
            }

            return Ok(responseList);
        }

        [HttpGet]
        [Route("GetRecentProducts")]
        public async Task<IActionResult> GetRecentProducts(int page = 1, int pageSize = 5)
        {

            var skipCount = (page - 1) * pageSize;
            var products = await _cxt.Products.Skip(skipCount).Take(pageSize)
                .OrderByDescending(p => p.DatePublication)
                .ToListAsync();

            var responseList = new List<object>();

            foreach (var product in products)
            {
                var response = new
                {
                    Id = product.Id,
                    Price = product.price,
                    ImageBase64 = Convert.ToBase64String(product.imagePrinciple),
                    VideoBase64 = Convert.ToBase64String(product.VideoData),
                    qty = product.qty,
                    Description = product.Description,
                    Details = product.Details,
                    title = product.title,
                    CodeBar = product.CodeBar,
                    Color = product.Color,
                    DatePublication = product.DatePublication
                };

                responseList.Add(response);
            }

            return Ok(responseList);
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }


        [HttpPost]
        [Route("filterproduct")]
        public IActionResult FilteredProducts(string keyword, int categoryId, decimal minPrice, decimal maxPrice)
        {
            var query = _cxt.Products.AsQueryable();

            // Apply filters based on provided criteria
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(p => p.title.Contains(keyword) || p.Description.Contains(keyword));
            }

            if (categoryId != 0)
            {
                query = query.Where(p => p.IdCateg == categoryId);
            }

            if (minPrice > 0)
            {
                query = query.Where(p => p.price >= minPrice);
            }
            if (maxPrice > 0)
            {
                query = query.Where(p => p.price <= maxPrice);
            }


            List<Product> filteredProducts = query.ToList();

            return Ok(filteredProducts);
        }


    }

}