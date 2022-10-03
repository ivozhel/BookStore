using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPersonService
    {
        public IEnumerable<Person> GetAllUsers();
        public Person? GetByID(int id);
        public Person? AddUser(Person user);
        public Person? DeleteUser(int id);
        public Person? UpdateUser(Person person);

    }
}
