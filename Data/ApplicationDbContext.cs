using FPTBOOK.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FPTBOOK.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }
    public DbSet<Customer> Customers { get; set; }
    //public DbSet<Product> Products { get; set; }
    //public DbSet<Category> Categories { get; set; }
}
