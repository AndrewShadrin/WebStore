using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class EmployeeMapper
    {
        public static EmployeesViewModel ToView(this Employee employee) => new EmployeesViewModel
        {
            Id = employee.Id,
            FirstName = employee.Name,
            LastName = employee.Surname,
            Patronymic = employee.Patronymic,
            Age = employee.Age,
            EmploymentDate = employee.EmploymentDate
        };

        public static IEnumerable<EmployeesViewModel> ToView(this IEnumerable<Employee> employees) => employees.Select(ToView);

        public static Employee FromView(this EmployeesViewModel model) => new Employee
        {
            Id = model.Id,
            Name = model.FirstName,
            Surname = model.LastName,
            Patronymic = model.Patronymic,
            Age = model.Age,
            EmploymentDate = model.EmploymentDate
        };
    }
}
