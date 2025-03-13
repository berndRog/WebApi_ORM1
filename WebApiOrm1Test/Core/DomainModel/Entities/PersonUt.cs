using WebApiOrm.Core.DomainModel.Entities;
using Xunit;

namespace WebApiOrmTest.Core.DomainModel.Entities;
public class PersonUt {

   private readonly Seed _seed = new();


   [Fact]
   public void CtorUt() {
      // Arrange
      Person actual = new Person(
         id: _seed.Person1.Id,
         firstName: _seed.Person1.FirstName,
         lastName: _seed.Person1.LastName,
         email:  _seed.Person1.Email,
         phone: _seed.Person1.Phone
      );
      // Assert
      Assert.Equal(actual.Id, _seed.Person1.Id);
      Assert.Equal(actual.FirstName, _seed.Person1.FirstName);
      Assert.Equal(actual.LastName,_seed.Person1.LastName);
      Assert.Equal(actual.Email,_seed.Person1.Email);
      Assert.Equal(actual.Phone,_seed.Person1.Phone);
   }
   
   [Fact]
   public void GetterUt() {
      // Arrange
      var actual = _seed.Person1;
      // Act
      var actualId = actual.Id;
      var actualFirstName = actual.FirstName;
      var actualLastName = actual.LastName;
      var actualEmail = actual.Email;
      var actualPhone = actual.Phone;
      // Assert
      Assert.Equal(actualId,_seed.Person1.Id);
      Assert.Equal(actualFirstName,_seed.Person1.FirstName);
      Assert.Equal(actualLastName,_seed.Person1.LastName);
      Assert.Equal(actualEmail,_seed.Person1.Email);
      Assert.Equal(actualPhone,_seed.Person1.Phone);
   }

   [Fact]
   public void SetUt() {
      // Arrange
      Person actual = new Person(
         id: _seed.Person1.Id,
         firstName: _seed.Person1.FirstName,
         lastName: _seed.Person1.LastName,
         email:  null,
         phone: null
      );
      // Act
      actual.Set(email: _seed.Person1.Email, phone: _seed.Person1.Phone);
      // Assert
      Assert.Equal(actual.Id,_seed.Person1.Id);
      Assert.Equal(actual.FirstName,_seed.Person1.FirstName);
      Assert.Equal(actual.LastName,_seed.Person1.LastName);
      Assert.Equal(actual.Email,_seed.Person1.Email);
      Assert.Equal(actual.Phone,_seed.Person1.Phone);
   }

   [Fact]
   public void UpdateUt() {
      // Arrange
      Person actual = _seed.Person1;
      // Act
      actual.Update(_seed.Person2);
      // Assert
      Assert.Equal(actual.FirstName,_seed.Person2.FirstName);
      Assert.Equal(actual.LastName,_seed.Person2.LastName);
      Assert.Equal(actual.Email,_seed.Person2.Email);
      Assert.Equal(actual.Phone,_seed.Person2.Phone);
   }
   
}