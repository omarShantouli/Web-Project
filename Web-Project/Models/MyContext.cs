using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Web_Project.Models;

namespace khalil_testing.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<Student> students { set; get; }

        public DbSet<Assignments> assignments { set; get; }

        public DbSet<Section> sections { set; get; }

    }
}