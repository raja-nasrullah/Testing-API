using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AddDbContext : DbContext
    {
        public AddDbContext(DbContextOptions<AddDbContext> options): base(options) 
        {

        }

        public DbSet<Student> tbl_Students { get; set; }
    }
}
