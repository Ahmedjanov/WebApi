namespace WebApi.Helpers;

using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models.Products;

public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ProductA> ProductAs { get; set; }
    public DbSet<ProductB> ProductBs { get; set; }
    
    private readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres database
        options.UseNpgsql(Configuration.GetConnectionString("TestApi"));
    }
}