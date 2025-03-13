using System.Threading.Tasks;
using WebApiOrm.Core.DomainModel.Entities;
using WebApiOrmTest.Persistence.Repositories;
using WebOrmTest.Data.Repositories;
using Xunit;
namespace OrmTest.Data.Repositories;

[Collection(nameof(SystemTestCollectionDefinition))]
public class PeopleRepositoryUt : BaseRepositoryUt {

   [Fact]
   public void FindByIdUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var actual = _peopleRepository.FindById(_seed.Person1.Id);
      // Assert
      Assert.Equal(_seed.Person1, actual);
   }

   [Fact]
   public void FindByNameUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
      _dataContext.SaveAllChanges();
      _dataContext.ClearChangeTracker();
      // Act
      var name = _seed.Person2.FirstName + " " + _seed.Person2.LastName;
      var actual = _peopleRepository.FindByName(name);
      // Assert
      Assert.Equal(_seed.Person2, actual);
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
      Assert.Equal(expected, actual);
   }


   [Fact]
   public void UpdateUt() {
      // Arrange
      _peopleRepository.AddRange(_seed.People);
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
      _peopleRepository.UpdateAsync(updPerson);
      _dataContext.SaveAllChanges();
      // Assert
      var actual = _peopleRepository.FindById(updPerson.Id);
      Assert.Equal(updPerson, actual);
   }
}

//    
//    #region with accounts
//    [Fact]
//    public async Task FindByIdJoinAsyncUt() {
//       // Arrange
//       
//       
//       await _arrangeTest.OwnersWithAccountsAsync(_seed);
//       // Act  with tracking
//       var actual = await _peopleRepository.FindByIdJoinAsync(_seed.Person1.Id, true);
//       // Assert
//       actual.Should()
//          .NotBeNull().And
//          .BeEquivalentTo(_seed.Person1, options => options.IgnoringCyclicReferences());
//    }
//    [Fact]
//    public async Task FindByJoinAsyncUt() {
//       // Arrange
//       await _arrangeTest.OwnersWithAccountsAsync(_seed);
//       // Act  with tracking
//       var actual = await _peopleRepository.FindByJoinAsync(o => o.Email == _seed.Owner5.Email, true);
//       // Assert
//       actual.Should()
//          .NotBeNull().And
//          .BeEquivalentTo(_seed.Owner5, options => options.IgnoringCyclicReferences());
//    }
//    #endregion
// }