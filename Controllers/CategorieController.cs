
using Microsoft.AspNetCore.Mvc;
using CoolApi.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategorieController : ControllerBase
    {
        private readonly CategorieContext _cxt;
        public static IWebHostEnvironment _webHostEnivronment;

        public CategorieController(CategorieContext context, IWebHostEnvironment webHostEnvironment)
        {
            _cxt = context;
            _webHostEnivronment = webHostEnvironment;
        }

        [HttpPost]
        [Route("addCategorie")]
        [Authorize]

        public IActionResult CreateCategorie(Categorie categorie)
        {
            _cxt.Categories.Add(categorie);
            _cxt.SaveChanges();

            return Ok();
        }



        [HttpPost]
        [Route("AddNewCategorie")]
        public async Task<string> CreateCategorieFile([FromForm] Categorie categorieFile)
        {
            try
            {
                if (categorieFile.ImageFile != null && categorieFile.ImageFile.Length > 0)
                {
                    string imagePath = _webHostEnivronment.WebRootPath + "\\images\\";

                    if (!Directory.Exists(imagePath))
                    {
                        Directory.CreateDirectory(imagePath);
                    }

                  
                    string uniqueImageFileName = Guid.NewGuid().ToString() + "_" + categorieFile.ImageFile.FileName;

                    string imageFilePath = Path.Combine(imagePath, uniqueImageFileName);

                    using (FileStream imageFileStream = System.IO.File.Create(imageFilePath))
                    {
                        await categorieFile.ImageFile.CopyToAsync(imageFileStream);
                        await imageFileStream.FlushAsync();
                    }

                    byte[] imageData;
                    using (MemoryStream imageMemoryStream = new MemoryStream())
                    {
                        await categorieFile.ImageFile.CopyToAsync(imageMemoryStream);
                        imageData = imageMemoryStream.ToArray();
                    }

                  
                    // Assign the image and video data to the respective properties
                    Categorie categorie = new Categorie
                    {
                        ImageCat = imageData,
                        Title = categorieFile.Title, 
                Description = categorieFile.Description
                    };

                    _cxt.Categories.Add(categorie);
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
        [Route("GetCategorieAll")]
        public async Task<IActionResult> GetAllProduct()
        {

            var categories = await _cxt.Categories.ToListAsync();

            var responseList = new List<object>();

            foreach (var categorie in categories)
            {
                var response = new
                {
             
                    Title = categorie.Title,
                    Description = categorie.Description,
             
                    ImageBase64 = Convert.ToBase64String(categorie.ImageCat),
                };

                responseList.Add(response);
            }

            return Ok(responseList);
        }

     



        [HttpGet]
        [Route("getCategorie")]
        [Authorize]

        public async Task<IEnumerable<Categorie>> GetAll()
        {
            var categorie = await _cxt.Categories.ToListAsync();
            return categorie;
        }



    }
}