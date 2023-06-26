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
    public class CityController : ControllerBase
    {
           private readonly CitiesContext _cxt;

        public CityController(CitiesContext context)
        {
            _cxt = context;
        }

        [HttpPost]
        [Route("addCity")]

        public IActionResult CreateCity(City city)
        {
            _cxt.Cities.Add(city);
            _cxt.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("getCity")]
        public async Task<IEnumerable<City>> GetAll()
        {
            var city = await _cxt.Cities.ToListAsync();
            return city;
        }


    }
}