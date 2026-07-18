using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace DotNetMVCEF.Models
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> 

    // public class ApplicationDbContext : DbContext  //number of DbContext = number of databases
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // must run first — Identity sets up its own tables here
        }
        //to initiallize constructor of base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }
        public DbSet<Entities.Product> Products { get; set; } //products is the name of table within Database
    }
}
