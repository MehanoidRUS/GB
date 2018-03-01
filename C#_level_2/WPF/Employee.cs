using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Employee
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Salary { get; private set; }
        
        /// <summary>
        /// Задает Имя Фамилию и зарплату сотрудника
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="salary">Зарплата</param>
        public Employee(string name, string surname, int salary)
        {
            Name =name;
            Surname = surname;
            Salary = salary;
        }


        /// <summary>
        /// Перегрузка метода ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} {Surname} {Salary}";
        }
    }
}
