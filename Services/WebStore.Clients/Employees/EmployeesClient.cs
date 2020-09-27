using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Employees
{
    public class EmployeesClient : BaseClient, IEmployeesData
    {
        public EmployeesClient(IConfiguration configuration) : base(configuration, WebApi.Employees){}

        public IEnumerable<Employee> Get() => Get<IEnumerable<Employee>>(serviceAddress);

        public Employee GetById(int id) => Get<Employee>($"{serviceAddress}/{id}");

        public int Add(Employee employee) => Post(serviceAddress, employee).Content.ReadAsAsync<int>().Result;

        public void Edit(Employee employee) => Put(serviceAddress, employee);

        public bool Delete(int id) => Delete($"{serviceAddress}/{id}").IsSuccessStatusCode;

        public void SaveChanges()
        {
        }
    }
}
