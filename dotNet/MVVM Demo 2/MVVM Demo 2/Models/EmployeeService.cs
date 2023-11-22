using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Demo_2.Models
{
    public class EmployeeService
    {
        #region ---- PROPERTIES ----

        private static ObservableCollection<Employee> _employees = new ObservableCollection<Employee>();

        #endregion


        #region ---- CONSTRUCTORS ----

        public EmployeeService()
        {
            _employees = new ObservableCollection<Employee>();
            _employees.Add(new Employee() { Id = 1, Name = "John", Age = 25 });
            _employees.Add(new Employee() { Id = 2, Name = "Mary", Age = 35 });
            _employees.Add(new Employee() { Id = 3, Name = "Mike", Age = 45 });
            _employees.Add(new Employee() { Id = 4, Name = "Steve", Age = 55 });
            _employees.Add(new Employee() { Id = 5, Name = "Nancy", Age = 65 });
        }

        #endregion


        #region ---- METHODS ----

        public ObservableCollection<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                if (employee.Age < 21 || employee.Age > 60)
                    throw new Exception("Employee age must be between 21 and 60");

                // created a temp employee object to add to the collection
                // if I use the employee object directly, it will be a reference
                // and any changes made to the employee object will be reflected in the UI  
                Employee tempEmployee = new Employee()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                    Id = employee.Id
                };
                _employees.Add(tempEmployee);
                return true;
            }
            catch (Exception) { return false; }
        }


        public bool UpdateEmployeeList(Employee employee)
        {
            bool IsUpdated = false;

            if (employee != null)
            {
                var ee = _employees.FirstOrDefault(e => e.Id == employee.Id);
                if (ee != null)
                {
                    ee.Name = employee.Name;
                    ee.Age = employee.Age;
                    IsUpdated = true;
                }
            }

            return IsUpdated;
        }


        public bool DeleteEmployee(int id)
        {
            try
            {
                var ee = _employees.FirstOrDefault(e => e.Id == id);
                if (ee != null)
                {
                    _employees.Remove(ee);
                    return true;
                }
                else
                    throw new Exception("Employee not found");
            }
            catch (Exception) { return false; }
        }
      
        
        public Employee GetEmployeeById(int id)
        {
            return _employees.FirstOrDefault(e => e.Id == id);
        }

        #endregion
    }
}
