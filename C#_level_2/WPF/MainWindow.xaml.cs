using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Department> departments;
        


        public MainWindow()
        {
            InitializeComponent();
            departments = new ObservableCollection<Department>()
            {
                new Department("Отдел Кадров"),
                new Department("Отдел Снабжения"),
                new Department("Отдел ИТО")
            };


            DefaultValue();
            
        }
        /// <summary>
        /// Заполняет ListBoxы и выставляет начальные значения
        /// </summary>
        void DefaultValue()
        {
            lbDepartment.ItemsSource = departments;
            lbDepartment.SelectedIndex = 0;
            lbDepartment.SelectionMode = SelectionMode.Single;
            cmbbxDepartments.ItemsSource = departments;
            cmbbxDepartments.SelectedIndex = 0;

            departments[0].AddEmployee("Светлана", "Кадровичка", 10000);
            departments[0].AddEmployee("Ольга", "Кадровичка", 1000);
            departments[0].AddEmployee("Лена", "Кадровичка", 1000);
            departments[1].AddEmployee("Михалыч", "Снабженец", 500);
            departments[1].AddEmployee("Потапыч", "Снабженец", 400);
            departments[2].AddEmployee("Вован", "ИТО", 8082);
        }

        /// <summary>
        /// Обработка события кнопки "Добавить департамент"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDepartment_Click(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrWhiteSpace(txtbAddDepartment.Text))
            //    departments.Add(new Department(txtbAddDepartment.Text));

            //foreach (var item in departments)
            //{
            //    Console.WriteLine(item);
            //}

            AddDepartment addDepartment = new AddDepartment()
            {
                
                Owner = this
            };
            addDepartment.Show();

        }

        private void lbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbEmployee.ItemsSource = departments[lbDepartment.SelectedIndex].Employees;
        }

        private void btnMoveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee tmp=departments[lbDepartment.SelectedIndex].Employees[lbEmployee.SelectedIndex];
            departments[cmbbxDepartments.SelectedIndex].AddEmployee(tmp.Name, tmp.Surname, tmp.Salary);
            departments[lbDepartment.SelectedIndex].Employees.RemoveAt(lbEmployee.SelectedIndex);

        }
    }
}
