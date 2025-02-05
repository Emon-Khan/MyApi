using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApi.Data;

namespace MyApi.Extensions
{
    public static class MigrationExtensions
    {
        public static void ApplyMigrations(this IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate(); // Automatically applies migrations
        }
    }
}