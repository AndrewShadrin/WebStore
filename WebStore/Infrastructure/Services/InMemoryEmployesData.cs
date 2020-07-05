using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployesData : IEmployeesData
    {
        private readonly List<Employee> employees = TestData.Employees;

        public int Add(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (employees.Contains(employee))
            {
                return employee.Id;
            }

            employee.Id = employees.Count == 0 ? 1 : employees.Max(e => e.Id) + 1;
            employees.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id) => employees.RemoveAll(e => e.Id == id) > 0;

        public void Edit(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (employees.Contains(employee))
            {
                return;
            }

            var db_employee = GetById(employee.Id);
            if (db_employee is null)
            {
                return;
            }

            db_employee.Name = employee.Name;
            db_employee.Surname = employee.Surname;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;
        }

        public IEnumerable<Employee> Get() => employees;

        public Employee GetById(int id) => employees.FirstOrDefault(x => x.Id == id);

        public void SaveChanges() { }
    }
}
