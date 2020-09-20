using System;
using System.Collections.Generic;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InSQL
{
    public class SQLEmployeeData : IEmployeesData
    {
        private readonly WebStoreDB dB;

        public SQLEmployeeData(WebStoreDB dB)
        {
            this.dB = dB;
        }
        public int Add(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            dB.Add(employee);
            return employee.Id;
        }

        public bool Delete(int id)
        {
            var employee = GetById(id);
            if (employee is null)
            {
                return false;
            }
            dB.Remove(employee);

            return true;
        }

        public void Edit(Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }
            dB.Update(employee);
        }

        public IEnumerable<Employee> Get()
        {
            return dB.Employees;
        }

        public Employee GetById(int id)
        {
            return dB.Employees.Find(id);
        }

        public void SaveChanges()
        {
            dB.SaveChanges();
        }
    }
}
