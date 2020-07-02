﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee>
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


    }
}