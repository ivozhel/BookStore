using BookStore.Models.Models.Users;

namespace BookStore.DL.Interfaces
{
    public interface IEmployeeRepo
    {
        public Task<IEnumerable<Employee>> GetAllEmployees();
        public Task<Employee> GetByID(int id);
        public Task<Employee> AddEmployee(Employee employee);
        public Task<Employee> DeleteEmployee(int id);
        public Task<Employee> UpdateEmployee(Employee employee);
        public Task<bool> CheckEmployee(int id);
    }
}
