using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using zs.nn.NoshNavigatorServices.Domain.Entity.Recipe;

namespace zs.nn.NoshNavigatorServices.Persistence.MsSql
{
    public class RecipeContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<InstructionStep> InstructionSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the relationship between Recipe and Ingredient
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(i => i.Recipe)
                .HasForeignKey(i => i.RecipeId);

            // Define the relationship between Recipe and InstructionStep
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.InstructionSteps)
                .WithOne(s => s.Recipe)
                .HasForeignKey(s => s.RecipeId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Grab the ASP.NET Core environment variable
            var environmentName = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Configuration Builder to reference appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = builder.Build();

            // Check if the environment is Development
            if (environmentName == "Development")
            {
                var inMemoryConnectionString = config.GetConnectionString("InMemAppDb");
                optionsBuilder.UseInMemoryDatabase(inMemoryConnectionString);
            }
            else
            {
                var sqlConnectionString = config.GetConnectionString("AppDb");
                optionsBuilder.UseSqlServer(sqlConnectionString);
            }
        }
    }
}
