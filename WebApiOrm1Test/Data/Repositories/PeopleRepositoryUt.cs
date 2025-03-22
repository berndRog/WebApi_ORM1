using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrmTest.Persistence.Repositories;
using WebOrmTest.Data.Repositories;
using Xunit;
namespace OrmTest.Data.Repositories;

[Collection(nameof(SystemTestCollectionDefinition))]
public class PeopleRepositoryUt : BaseRepository {

   [Fact]
   public void FindByIdUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var actual = _peopleRepository.FindById(_seed.Person1.Id);
      // Assert
      Assert.Equivalent(_seed.Person1, actual);
   }

   [Fact]
   public async Task AddUt() {
      // Arrange
      var person = _seed.Person1;
      // Act
      _peopleRepository.Add(person);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(person.Id);
      Assert.Equal(person, actual);
   }

   [Fact]
   public async Task AddRangeUt() {
      // Arrange
      var expected = _seed.People;
      // Act
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Assert
      var actual = _peopleRepository.SelectAll();
      Assert.Equivalent(expected, actual);
   }


   [Fact]
   public void UpdateUt() {
      // Arrange
      _peopleRepository.Add(_seed.Person1);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var updPerson = new Person(
         id: _seed.Person1.Id,
         firstName: "Erika",
         lastName: "Meier",
         email: _seed.Person1.Email,
         phone: _seed.Person1.Phone
      );
      _peopleRepository.Update(updPerson);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(updPerson.Id);
      Assert.Equivalent(updPerson, actual);
   }
   
   [Fact]
   public void RemoveUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      _peopleRepository.Remove(_seed.Person1);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(_seed.Person1.Id);
      Assert.Null(actual);
   }
   
   [Fact]
   public void SelectByNameUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      var expected = new List<Person> { _seed.Person1, _seed.Person2 };
      
      // Act
      var actual = _peopleRepository.SelectByName("Muster"); 
      
      // Assert
      Assert.Equivalent(expected, actual);
   }
}