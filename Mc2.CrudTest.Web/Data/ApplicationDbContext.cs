using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mc2.CrudTest.Web.Data;

namespace Mc2.CrudTest.Web.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Mc2.CrudTest.Web.Data.Customer> Customer { get; set; } = default!;
    }
}
