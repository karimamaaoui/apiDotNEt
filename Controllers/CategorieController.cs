
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

        public CategorieController(CategorieContext context)
        {
            _cxt = context;
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