using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SweetAndSavory.Models
{
  public class SweetAndSavoryContextSweetAndSavory : IDesignTimeDbContextSweetAndSavory<SweetAndSavoryContext>
  {

    SweetAndSavoryContext IDesignTimeDbContextSweetAndSavory<SweetAndSavoryContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build(); 

      var builder = new DbContextOptionsBuilder<SweetAndSavoryContext>(); 

      builder.UseMySql(configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(configuration["ConnectionStrings:DefaultConnection"])); 

      return new SweetAndSavoryContext(builder.Options); 
    }
  }
}