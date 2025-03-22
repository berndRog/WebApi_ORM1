using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class PeopleRepository(
   DataContext dataContext
) : IPeopleRepository {
   
   private readonly IDataContext _dataContext = dataContext;
   private readonly DbSet<Person> _dbSetPeople = dataContext.People; // => Set<Person>

   public IEnumerable<Person> SelectAll() {
      var people = _dbSetPeople.ToList();
      dataContext.LogChangeTracker("Person: SelectAll");
      return people;
   }

   public Person? FindById(Guid id) {
      var person = _dbSetPeople.FirstOrDefault(person => person.Id == id);
      dataContext.LogChangeTracker("Person: FindById");
      return person;
   }
   
   public void Add(Person person) =>
      _dbSetPeople.Add(person);

   public void AddRange(IEnumerable<Person> people) =>
      _dbSetPeople.AddRange(people);

   public void Update(Person updPerson) {
      var retrievedItem = _dbSetPeople.Find(updPerson.Id);
      if (retrievedItem == null)
         throw new ApplicationException($"Update failed, person with given id not found");
      dataContext.Entry(retrievedItem).CurrentValues.SetValues(updPerson);
      dataContext.Entry(retrievedItem).State = EntityState.Modified; 
   }

   public void Remove(Person person) {
      var pFound = _dbSetPeople.FirstOrDefault(p => p.Id == person.Id);
      if (pFound == null) throw new Exception("Person to be removed not found");
      _dbSetPeople.Remove(pFound);
   }
   
   public IEnumerable<Person> SelectByName(string namePattern) {
      if (string.IsNullOrWhiteSpace(namePattern))
         return Enumerable.Empty<Person>();
      // var tokens = namePattern.Trim().Split(" ");
      // var firstName = string.Join(" ", tokens.Take(tokens.Length - 1));
      // var lastName = tokens.Last();
      // var people = _dbSetPeople.Where(person =>
      //       EF.Functions.Like(person.FirstName, $"%{firstName}%") || 
      //       EF.Functions.Like(person.LastName, $"%{lastName}%"))
      //    .ToList();
      var people = _dbSetPeople
         .Where(person => EF.Functions.Like(person.LastName, $"%{namePattern}%"))
         .ToList();
      dataContext.LogChangeTracker("Person: FindByNamePattern");
      return people;
   }

}