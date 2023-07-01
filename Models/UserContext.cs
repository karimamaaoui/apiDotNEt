using Microsoft.EntityFrameworkCore;
using CoolApi.Models;
namespace CoolApi.Models
{
    public class UserContext : DbContext
    {
        
      public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }

        

    }

}
   