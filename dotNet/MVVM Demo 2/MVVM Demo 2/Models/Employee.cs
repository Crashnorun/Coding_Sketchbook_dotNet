using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVVM_Demo_2.Models
{
    public class Employee : INotifyPropertyChanged
    {

        #region ---- PROPERTIES ----

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set { _age = value; OnPropertyChanged("Age"); }
        }

        #endregion


        #region ---- EVENTS ----

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion




        #region ---- CONSTRUCTORS ----

        public Employee() { }


        public Employee(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        #endregion

    }
}
