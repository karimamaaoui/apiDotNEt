using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace CoolApi.Models
{
    public class CountriesContext : DbContext


{
    public CountriesContext(DbContextOptions<CountriesContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }


}}