using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController  : ControllerBase
    {
           private readonly CountriesContext _cxt;

        public CountryController(CountriesContext context)
        {
            _cxt = context;
        }

        [HttpPost]
        [Route("addCountry")]

        public IActionResult CreateCountry(Country country)
        {
            _cxt.Countries.Add(country);
            _cxt.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("getCountry")]
        public async Task<IEnumerable<Country>> GetAll()
        {
            var country = await _cxt.Countries.ToListAsync();
            return country;
        }


    }
}