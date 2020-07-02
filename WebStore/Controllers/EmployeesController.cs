﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("Users")]
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Иван",
                Surname = "Иванов",
                Patronymic = "Иванович",
                Age = 39
            },
            new Employee
            {
                Id = 2,
                Name = "Пётр",
                Surname = "Петров",
                Patronymic = "Петрович",
                Age = 28
            },
            new Employee
            {
                Id = 3,
                Name = "Сидор",
                Surname = "Сидоров",
                Patronymic = "Сидорович",
                Age = 18
            },
        };

        //[Route("User-{id}")]
        public IActionResult Details(int id)
        {
            var employee = employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        
        //[Route("All")]
        public IActionResult Index()
        {
            return View(employees);
        }
    }
}
