using CadastroWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroWebApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> op ): base( op )
    {
        
    }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Category>? Categories { get; set; } 
}
