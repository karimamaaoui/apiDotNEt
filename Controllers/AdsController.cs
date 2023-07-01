
using Microsoft.AspNetCore.Mvc;
using CoolApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace CoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ControllerBase
    {

        private readonly AdsContext _cxt;
        private readonly CategorieContext _context;

        public AdsController(AdsContext context)
        {
            _cxt = context;
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
        [Authorize]
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





    }
}