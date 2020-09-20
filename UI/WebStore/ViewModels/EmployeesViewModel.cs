using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeesViewModel //: IValidatableObject
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name ="Имя")]
        [Required(ErrorMessage = "Имя является обязательным")]
        [StringLength(200, ErrorMessage = "Длина строки имени должна быть от 3 до 200 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата. Только русские или латиница. Никаких цифр!")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия является обязательным")]
        [StringLength(200, ErrorMessage = "Длина строки фамилии должна быть от 3 до 200 символов")]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст является обязательным")]
        [Range(20, 80, ErrorMessage = "Возраст должен быть в пределах от 20 до 80 лет")]
        public int Age { get; set; }

        [Display(Name = "Дата приема на работу")]
        [DataType(DataType.DateTime)]
        public DateTime EmploymentDate { get; set;}
    }
}
