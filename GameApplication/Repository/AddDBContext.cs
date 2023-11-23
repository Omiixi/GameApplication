using GameApplication.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameApplication.Repository;

public class AddDBContext : DbContext
{
    //public DbSet<Game> Games { get; set; }
    public AddDBContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Game> Games { get; set; }
}