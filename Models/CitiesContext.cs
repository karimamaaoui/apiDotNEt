using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoolApi.Models
{
    public class CitiesContext : DbContext


{
    public CitiesContext(DbContextOptions<CitiesContext> options) : base(options)
    {

    }

    public DbSet<City> Cities { get; set; }


}}