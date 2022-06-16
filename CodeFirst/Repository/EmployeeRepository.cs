using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return context.Employees.Include(e => e.Departments).ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return context.Employees.Include(e => e.Departments).First();
        }

        public void NewEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
        }

        public void DeleteEmployee(int id)
        {
            var employee = context.Employees.Include(e=>e.Departments).First();
            if (employee != null) context.Employees.Remove(employee);
            context.SaveChanges();

        }

    }
}
