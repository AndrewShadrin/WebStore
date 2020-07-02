using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryEmployesData : IEmployeesData
    {
        public int Add(Employee employee)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Employee employee)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Employee> Get()
        {
            throw new NotImplementedException();
        }

        public Employee GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
