using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Repository
{
    public interface IEmployeeRepository 
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
        void NewEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
