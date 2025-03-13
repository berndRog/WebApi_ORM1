using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApiOrm.Core;
using WebApiOrm.Data;
using WebApiOrm.Data.Repositories;
namespace WebApiOrmTest.Di;

public static class DiTestData {

   public static void AddDataTest(
      this IServiceCollection services
   ) {

      // Configuration
      // Nuget:  Microsoft.Extensions.Configuration
      //       + Microsoft.Extensions.Configuration.Json
      var configuration = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettingsTest.json", false)
         .Build();
      services.AddSingleton<IConfiguration>(configuration);
      
      // Logging
      // Nuget:  Microsoft.Extensions.Logging
      //       + Microsoft.Extensions.Logging.Configuration
      //       + Microsoft.Extensions.Logging.Debug
      var logging = configuration.GetSection("Logging");
      services.AddLogging(builder => {
         builder.ClearProviders();
         builder.AddConfiguration(logging);
         builder.AddDebug();
      });
      
      // UseCases ...
      // services.AddCore();
      
      // Repository, Database ...
      services.AddSingleton<IPeopleRepository, PeopleRepository>();
      
      // Add DbContext (Database) to DI-Container
      var (useDatabase, dataSource) = DataContext.EvalDatabaseConfiguration(configuration);
      
      switch (useDatabase) {
         case "Sqlite":
            services.AddDbContext<IDataContext, DataContext>(options => {
                  options.UseSqlite(dataSource);
                  Console.WriteLine($"....: UseSqlite {dataSource}");
                  //options.UseSqlite("DataSource=:memory:");
                  //Console.WriteLine($"....: UseSqlite in Memory Database");
               }
            );
            break;
         default:
            throw new Exception("appsettings.json UseDatabase not available");
      }
   }
}