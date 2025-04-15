using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace VeterinariaPAW.Models
{
    public class VeterinariaContextFactory : IDesignTimeDbContextFactory<VeterinariaContext>
    {
        public VeterinariaContext CreateDbContext(string[] args)
        {
            // Buscar la raíz donde está el archivo appsettings.json
            var basePath = Directory.GetCurrentDirectory();

            // Verifica si estás en una subcarpeta como bin/Debug
            if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
            {
                basePath = Path.Combine(basePath, "..", "..", "..");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<VeterinariaContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new VeterinariaContext(optionsBuilder.Options);
        }
    }
}
