using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Models
{
    public class PizzaDD
    {
        public int Id { get; set; }
        public string? Nom {  get; set; }
        public string? Description { get; set; }
    }

    class PizzaDDDB : DbContext
    {
        public PizzaDDDB(DbContextOptions options) : base(options) { }
        public DbSet<PizzaDD> Pizzas { get; set; } = null;
    }
}
