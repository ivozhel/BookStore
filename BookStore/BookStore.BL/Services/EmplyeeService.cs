using BookStore.BL.Interfaces;
using BookStore.DL.Interfaces;
using BookStore.Models.Models.Users;

namespace BookStore.BL.Services
{
    public class EmplyeeService : IEmplyeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        public EmplyeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            return await _employeeRepo.AddEmployee(employee);
        }

        public async Task<bool> CheckEmployee(int id)
        {
            return await _employeeRepo.CheckEmployee(id);
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            return await _employeeRepo.DeleteEmployee(id);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _employeeRepo.GetAllEmployees();
        }

        public async Task<Employee> GetByID(int id)
        {
            return await _employeeRepo.GetByID(id);
        }
        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            return await _employeeRepo.UpdateEmployee(employee);
        }
    }
}
