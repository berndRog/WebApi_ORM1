using System;
using System.Collections.Generic;
using WebApiOrm.Core.DomainModel.Entities;
namespace WebApiOrmTest;

public class Seed {

   public Person Person1{ get; }
   public Person Person2{ get; }
   public Person Person3{ get; }
   public Person Person4{ get; }
   
   public List<Person> People{ get; private set; }

   public Seed(){
      #region People
      Person1 = new Person(
         id: new Guid("10000000-0000-0000-0000-000000000000"),
         firstName: "Erika",
         lastName: "Mustermann",
         email: "erika.mustermann@t-online.de",
         phone: "05826 1234 5678"
      );
      Person2 = new Person (
         id: new Guid("20000000-0000-0000-0000-000000000000"),
         firstName: "Max",
         lastName: "Mustermann",
         email: "max.mustermann@gmail.com",
         phone: "05826 1234 5678"
      );
      Person3 = new Person (
         id: new Guid("30000000-0000-0000-0000-000000000000"),
         firstName: "Arno",
         lastName: "Arndt",
         email: "a.arndt@t-online.de",
         phone: "05826 1234 5678"
      );
      Person4 = new Person(
         id: new Guid("40000000-0000-0000-0000-000000000000"),
         firstName: "Benno",
         lastName: "Bauer",
         email: "b.bauer@gmail.com",
         phone: "05826 1234 5678"
      );
      
      People = new List<Person> { Person1, Person2, Person3, Person4 };
      #endregion
   }
}