using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Data;
using WebApiOrm.Data.Repositories;
namespace WebApiOrm;

public static class ServiceExtensions {
   
   public static IServiceCollection AddData(
      this IServiceCollection services,
      IConfiguration configuration
   ) {
      services.AddScoped<IPeopleRepository, PeopleRepository>();
      
      // Add DbContext (Database) to DI-Container
      var (useDatabase, dataSource) = DataContext.EvalDatabaseConfiguration(configuration);
      
      switch (useDatabase) {
         case "Sqlite": 
         case "SqliteInMemory":
            services.AddDbContext<IDataContext, DataContext>(options => 
               options.UseSqlite(dataSource)
            );
            break;
         default:
            throw new Exception("appsettings.json UseDatabase not available");
      }
      return services;
   }
   
}