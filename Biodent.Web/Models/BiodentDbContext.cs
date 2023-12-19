using Microsoft.EntityFrameworkCore;

namespace Biodent.Web.Models
{
    public class BiodentDbContext:DbContext
    {
        public BiodentDbContext(DbContextOptions<BiodentDbContext> options) : base(options) { }
    }
}
