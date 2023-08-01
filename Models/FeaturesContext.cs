using Microsoft.EntityFrameworkCore;



namespace CoolApi.Models
{
    public class FeaturesContext: DbContext


{
    public FeaturesContext(DbContextOptions<FeaturesContext> options) : base(options)
    {
    }

    public DbSet<Features> Features { get; set; }


}}