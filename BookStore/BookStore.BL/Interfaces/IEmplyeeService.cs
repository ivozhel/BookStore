using BookStore.Models.Models.Users;

namespace BookStore.BL.Interfaces
{
    public interface IEmplyeeService
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetByID(int id);
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> DeleteEmployee(int id);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<bool> CheckEmployee(int id);
    }
}
