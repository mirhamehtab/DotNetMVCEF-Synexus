using Microsoft.EntityFrameworkCore;
namespace DotNetMVCEF.Models
{
    public class ApplicationDbContext : DbContext  //number of DbContext = number of databases
    {
        //to initiallize constructor of base class
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { }
        public DbSet<Entities.Product> Products { get; set; } //products is the name of table within Database
    }
}
