using Microsoft.EntityFrameworkCore;
using WebApiOrm.Core;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Data.Repositories;

public class PeopleRepository(
   DataContext dataContext
) : IPeopleRepository {
   
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

   public  void Update(Person person) {
      var retrieved = _dbSetPeople.Find(person.Id);
      if (retrieved == null)
         throw new ApplicationException($"Update failed, entity with given id not found");
      var entry = dataContext.Entry(retrieved);
      if (entry.State == EntityState.Detached) _dbSetPeople.Attach(person);
      entry.State = EntityState.Modified;
   }

   public void Remove(Person person) {
      var retrieved = _dbSetPeople.Find(person.Id);
      if (retrieved == null)
         throw new ApplicationException($"Remove failed, entity with given id not found");
      var entry = dataContext.Entry(retrieved);
      if (entry.State == EntityState.Detached) _dbSetPeople.Attach(person);
      entry.State = EntityState.Deleted;
   }
   
   public IEnumerable<Person> SelectByName(string namePattern) {
      if (string.IsNullOrWhiteSpace(namePattern))
         return [];
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