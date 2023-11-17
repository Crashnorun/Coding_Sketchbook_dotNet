using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Demo_2.Models
{
    public class EmployeeService
    {
        #region ---- PROPERTIES ----

        private static List<Employee> _employees = new List<Employee>();

        #endregion


        #region ---- CONSTRUCTORS ----

        public EmployeeService()
        {
            _employees = new List<Employee>();
            _employees.Add(new Employee() { Id = 1, Name = "John", Age = 25 });
            _employees.Add(new Employee() { Id = 2, Name = "Mary", Age = 35 });
            _employees.Add(new Employee() { Id = 3, Name = "Mike", Age = 45 });
            _employees.Add(new Employee() { Id = 4, Name = "Steve", Age = 55 });
            _employees.Add(new Employee() { Id = 5, Name = "Nancy", Age = 65 });
        }

        #endregion


        #region ---- METHODS ----

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public bool AddEmployee(Employee employee)
        {
            try
            {
                if (employee.Age < 21 || employee.Age > 60)
                    throw new Exception("Employee age must be between 21 and 60");

                _employees.Add(employee);
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
