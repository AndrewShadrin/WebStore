using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    public class User : NamedEntity
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        public DateTime EmploymentDate { get; set; }

        public bool IsEmployee { get => EmploymentDate.Ticks != 0; }
    }
}
