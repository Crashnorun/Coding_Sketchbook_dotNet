using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Example.Models
{
    public class Customer : INotifyPropertyChanged
    {

        #region ---- PROPERTEIS ----
        
        private string _Name;

        public string Name
        {
            get { return _Name; }
             set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        #endregion


        #region ---- CONSTRUCTORS ----

        public Customer(string CustomerName)
        {
            Name = CustomerName;
        }

        #endregion


        #region ---- METHODS ----

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion


        #region ---- EVENTS -----

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        
    }
}
