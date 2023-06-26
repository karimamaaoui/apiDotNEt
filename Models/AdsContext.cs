using System;
using Microsoft.EntityFrameworkCore;

namespace CoolApi.Models
{
    public class AdsContext : DbContext
    {
        public AdsContext(DbContextOptions<AdsContext> options) : base(options)
        {
        }
    
        public DbSet<Ads> Adss { get; set; }
    }
}