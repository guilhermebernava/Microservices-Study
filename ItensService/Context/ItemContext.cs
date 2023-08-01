using ItensService.Context.Configurations;
using ItensService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItensService.Context;

public class ItemContext : DbContext
{
    public DbSet<Item> Itens;
    public ItemContext(DbContextOptions<ItemContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
