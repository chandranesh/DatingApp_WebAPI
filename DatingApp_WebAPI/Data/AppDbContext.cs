using DatingApp_WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_WebAPI.Data
{
    public class AppDbContext(DbContextOptions options):DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
