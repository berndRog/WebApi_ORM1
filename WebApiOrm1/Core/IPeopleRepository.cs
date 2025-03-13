using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrm.Core;

public interface IPeopleRepository {
   Person? FindById(Guid id);
   IEnumerable<Person> SelectAll();
   
   void Add(Person person);
   void AddRange(IEnumerable<Person> people);
   void UpdateAsync(Person updPerson);
   void Remove(Person person); 
   
   Person? FindByName(string name);
   
}