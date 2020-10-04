using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    /// <summary>
    /// API управления сотрудниками
    /// </summary>
    //[Route("api/employees")]
    [Route(WebApi.Employees)]
    [Produces("application/json")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData employeesData;

        public EmployeesApiController(IEmployeesData employeesData)
        {
            this.employeesData = employeesData;
        }

        /// <summary>
        /// Получить всех доступных сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employeesData.Get();
        }

        /// <summary>
        /// Найти сотрудника по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор искомого сотрудника</param>
        /// <returns>Найденный сотрудник</returns>
        [HttpGet("{id}")]
        public Employee GetById(int id)
        {
            return employeesData.GetById(id);
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="employee">Новый сотрудник</param>
        /// <returns>Идентификатор добавленного сотрудника</returns>
        [HttpPost]
        public int Add(Employee employee)
        {
            var id = employeesData.Add(employee);
            SaveChanges();
            return id;
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        /// <param name="employee">Сотрудник для редактирования</param>
        [HttpPut]
        public void Edit(Employee employee)
        {
            employeesData.Edit(employee);
            SaveChanges();
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Идентификатор удаляемого сотрудника</param>
        /// <returns>Истина, если удаление успешно</returns>
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = employeesData.Delete(id);
            SaveChanges();
            return result;
        }

        [NonAction]
        public void SaveChanges()
        {
            employeesData.SaveChanges();
        }
    }
}
