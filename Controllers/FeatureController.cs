using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoolApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeatureController : ControllerBase
    {
         private readonly FeaturesContext _cxt;

           public FeatureController(FeaturesContext context)
        {
            _cxt = context;
        }

       
    }
}