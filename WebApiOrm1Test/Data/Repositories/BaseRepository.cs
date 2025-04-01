using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApiOrm.Core;
using WebApiOrm.Data;
using WebApiOrmTest;
using WebApiOrmTest.Di;
using WebApiOrmTest.Persistence.Repositories;
using Xunit;
namespace WebOrmTest.Data.Repositories;
[Collection(nameof(SystemTestCollectionDefinition))]
public abstract class BaseRepository {
   
   protected readonly IPeopleRepository _peopleRepository;
   protected readonly IDataContext _dataContext;
   protected readonly Seed _seed;

   protected BaseRepository() {
      
      // Test DI-Container
      IServiceCollection services = new ServiceCollection();
      // Add Core, UseCases, Mapper, ...
      //services.AddCore();
      // Add Repositories, Test Databases, ...
      var(useDatabase, dataSource) = services.AddDataTest();
      // Build ServiceProvider,
      // and use Dependency Injection or Service Locator Pattern
      var serviceProvider = services.BuildServiceProvider()
         ?? throw new Exception("Failed to create an instance of ServiceProvider");

      //-- Service Locator 
      var dbContext = serviceProvider.GetRequiredService<DataContext>()
         ?? throw new Exception("Failed to create DbContext");
      // In-Memory
      if (useDatabase == "SqliteInMemory") {
         dbContext.Database.OpenConnection();
      } else if (useDatabase == "Sqlite") {
         // File based
         dbContext.Database.EnsureDeleted();
         
      } else {
         dbContext.Database.EnsureDeleted();
      }
      dbContext.Database.EnsureCreated();
      
      _dataContext = serviceProvider.GetRequiredService<IDataContext>()
         ?? throw new Exception("Failed to create an instance of IDataContext");

      _peopleRepository = serviceProvider.GetRequiredService<IPeopleRepository>()
         ?? throw new Exception("Failed create an instance of IPeopleRepository");
      
      _seed = new Seed();
   }
}