
using Microsoft.AspNetCore.Mvc;
using CoolApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;



namespace CoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ControllerBase
    {

        private readonly AdsContext _cxt;
        private readonly CategorieContext _context;
        private readonly IWebHostEnvironment _env;
        public AdsController(AdsContext context, IWebHostEnvironment environment)
        {
            _cxt = context;
            _env = environment;
        }

        private string SaveImageAndGetUrl(IFormFile imageFile)
        {
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        [HttpGet("NbrAdsByUser")]
        public IActionResult NbrAdsByUser(int iduser)
        {
            try
            {
                int numberOfAds = _cxt.Adss.Count(a => a.IdUser == iduser);
                return Ok(numberOfAds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching number of ads: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("addnewAds")]
        public async Task<Ads> CreateAdd(Ads ad)
        {
            /*     if (ad.ImagePrinciple != null)
    {
        string imagePathOrUrl = SaveImageToStorage(ad.ImagePrinciple);
        ad.ImagePrinciple = imagePathOrUrl;
    }*/

            // Create a new instance of Ads to store in the database
            var newAd = new Ads
            {
                Title = ad.Title,
                Description = ad.Description,
                details = ad.details,
                Price = ad.Price,
                IdUser = ad.IdUser,
                IdPricesDelevery = ad.IdPricesDelevery,
                VideoName = ad.VideoName,
                IdCateg = ad.IdCateg,
                IdPays = ad.IdPays,
                IdCity = ad.IdCity,
                Locations = ad.Locations,
                IdBoost = ad.IdBoost,
                Active = ad.Active,
                ImagePrinciple = ad.ImagePrinciple,
                DatePublication = ad.DatePublication,
            };


            _cxt.Adss.Add(newAd);
            await _cxt.SaveChangesAsync();

            return newAd;
        }

        [HttpPost]
        [Route("addnewAdsWith imag")]
        public async Task<Ads> CreateAddWithImage([FromForm] Ads ad, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                string imagePathOrUrl = SaveImageAndGetUrl(imageFile);
                ad.ImagePrinciple = imagePathOrUrl;
            }

            var newAd = new Ads
            {
                Title = ad.Title,
                Description = ad.Description,
                details = ad.details,
                Price = ad.Price,
                IdUser = ad.IdUser,
                IdPricesDelevery = ad.IdPricesDelevery,
                VideoName = ad.VideoName,
                IdCateg = ad.IdCateg,
                IdPays = ad.IdPays,
                IdCity = ad.IdCity,
                Locations = ad.Locations,
                IdBoost = ad.IdBoost,
                Active = ad.Active,
                ImagePrinciple = ad.ImagePrinciple,
                DatePublication = ad.DatePublication,
            };


            _cxt.Adss.Add(newAd);
            await _cxt.SaveChangesAsync();

            return newAd;
        }




        [HttpPost]
        [Route("addAds")]
        public IActionResult CreateAds(Ads ads)


        {
            // Check if the required properties are provided
            if (ads.IdCateg == 0 || ads.IdUser == 0 || ads.IdPays == 0 || ads.IdCity == 0)
            {
                return BadRequest("Invalid input. Required properties are missing.");
            }

            _cxt.Adss.Add(ads);
            _cxt.SaveChanges();

            return Ok();
        }


        [HttpGet]
        [Route("getAds")]
        public async Task<IEnumerable<Ads>> GetAll()
        {
            var ads = await _cxt.Adss
                /*  .Select(a => new Ads
                  {
                      IdAds=a.IdAds,
                      Title = a.Title,
                      Description = a.Description,
                      IdCateg = a.IdCateg,
                      IdUser = a.IdUser,
                      IdCity = a.IdCity,
                      IdPays = a.IdPays,
                      Price = Convert.ToSingle(a.Price),
                      details=a.details,
                      ImagePrinciple = a.ImagePrinciple,
                      DatePublication = a.DatePublication,
                      Active=a.Active,
                      IdPricesDelevery=a.IdPricesDelevery,
                      VideoName=a.VideoName,
                      Locations=a.Locations,
                      IdBoost=a.IdBoost





                  })*/
                .ToListAsync();

            return ads;
        }

        [HttpGet]
        [Route("getAdsByUser")]
        public List<Ads> ShowMoreByIdUser(int iduser, int page)
        {
            List<Ads> ads = _cxt.Adss.Where(a => a.IdUser == iduser).ToList();
            return ads;
        }



    }
}