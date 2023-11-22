using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MVVM_Demo_2.Models;
using MVVM_Demo_2.Commands;
using System.Collections.ObjectModel;

namespace MVVM_Demo_2.ViewModels
{
    public class EmployeeVM : INotifyPropertyChanged
    {

        #region ---- PROPERTIES ----

        EmployeeService employeeService;

        /// <summary>
        /// ObservableCollection is a generic dynamic data collection that provides 
        /// notifications when items get added, removed, or when the whole list is refreshed.
        /// </summary>
        private ObservableCollection<Employee> _employees;
        public ObservableCollection<Employee> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChange("Employees"); }
        }


        /// <summary>
        /// CurrentEmployee is bound to the UI textboxes
        /// </summary>
        private Employee currentEmployee;
        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChange("CurrentEmployee"); }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChange("Message"); }
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
            CurrentEmployee = new Employee();
            
            // the save method cannot take any parameters and cannot return any value
            _saveCommand = new RelayCommand(Save);
            _searchCommand = new RelayCommand(SearchEmployee);
            _updateCommand = new RelayCommand(UpdateEmployee);
            _deleteCommand = new RelayCommand(DeleteEmployee);
        }

        #endregion



        #region ---- SEARCH EMPLOYEE ----


        private RelayCommand _searchCommand;

        public RelayCommand SearchCommand
        {
            get { return _searchCommand; }
        }

        public void SearchEmployee()
        {
            try
            {
                Employee employee = employeeService.GetEmployeeById(CurrentEmployee.Id);
                if (employee != null)
                {
                    // need to assign the values to the CurrentEmployee object
                    // otherwise the UI will not be updated correctly
                    CurrentEmployee.Name = employee.Name;
                    CurrentEmployee.Age = employee.Age;
                    CurrentEmployee.Id = employee.Id;
                    Message = "Employee found";
                }
                else
                {
                    Message = "Employee not found";
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        #endregion


        #region ---- SAVE EMPLOYEE ----


        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get { return _saveCommand; }
        }

        /// <summary>
        /// Save new employee to list of employees
        /// This method cannot take any inputs or return any values since it's a RelayCommand
        /// </summary>
        public void Save()
        {
            try
            {
                var IsSaved = employeeService.AddEmployee(CurrentEmployee);
                GetEmployees();
                if (IsSaved)
                    Message = "Employee added successfully";
                else
                    Message = "Employee not added";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }


        #endregion



        #region ---- UPDATE EMPLOYEE ----

        private RelayCommand _updateCommand;
        public RelayCommand UpdateCommand
        {
            get { return _updateCommand; }
        }

        public void UpdateEmployee()
        {
            try
            {
                var IsUpdated = employeeService.UpdateEmployeeList(CurrentEmployee);
                GetEmployees();
                if (IsUpdated)
                    Message = "Employee updated successfully";
                else
                    Message = "Employee not updated";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }

        #endregion


        #region ---- DELETE EMPLOYEE ----

        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        public void DeleteEmployee()
        {
            try
            {
                var IsDeleted = employeeService.DeleteEmployee(CurrentEmployee.Id);
                GetEmployees();
                if (IsDeleted)
                    Message = "Employee deleted successfully";
                else
                    Message = "Employee not deleted";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
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
