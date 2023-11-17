using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MVVM_Demo_2.Models;

namespace MVVM_Demo_2.ViewModels
{
    public class EmployeeVM : INotifyPropertyChanged
    {

        #region ---- PROPERTIES ----

        EmployeeService employeeService;

        private List<Employee> _employees;
        public List<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChange("Employees"); }
        }
      
        #endregion


        #region ---- EVENTS ----

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        #region ---- CONSTRUCTOR ----

        public EmployeeVM()
        {
            employeeService = new EmployeeService();
            GetEmployees();
        }

        #endregion


        #region ---- METHODS ----

        private void GetEmployees()
        {
            Employees = employeeService.GetAllEmployees();
        }

        #endregion
    }
}
