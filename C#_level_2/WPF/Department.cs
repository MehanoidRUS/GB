using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Department
    {
        string name;
        public ObservableCollection<Employee> Employees;

        /// <summary>
        /// Создает объект департамент с указанным названием
        /// </summary>
        /// <param name="name">Название департамента</param>
        public Department(string name)
        {
            this.name = name;
            Employees = new ObservableCollection<Employee>();
        }
        public void AddEmployee(string name, string surname, int salary)
        {
            Employees.Add(new Employee(name, surname, salary));
        }

        public override string ToString()
        {
            return name;
        }
    }
}
