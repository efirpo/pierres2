using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Pierres2.Models
{
  public class Pierres2ContextFactory : IDesignTimeDbContextFactory<Pierres2Context>
  {

    Pierres2Context IDesignTimeDbContextFactory<Pierres2Context>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<Pierres2Context>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new Pierres2Context(builder.Options);
    }
  }
}