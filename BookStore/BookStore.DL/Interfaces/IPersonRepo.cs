using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStore.DL.Interfaces
{
    public interface IPersonRepo
    {
        public IEnumerable< Person> GetAllUsers();
        public Person? GetByID(int id);
        public Person? AddUser(Person user);
        public Person? DeleteUser(int id);
        public Person? UpdateUser(Person person);

    }
}
