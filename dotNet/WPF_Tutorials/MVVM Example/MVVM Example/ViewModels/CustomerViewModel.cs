using MVVM_Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MVVM_Example.Commands;

namespace MVVM_Example.ViewModels
{
    public class CustomerViewModel
    {

        #region ---- PROPERTIES ----

        private Customer _Customer;

        public Customer Customer
        {
            get { return _Customer; }
            set { _Customer = value; }
        }

        public ICommand UpdateCommand
        {
            get;
            set;
        }


        public bool CanUpdate
        {
            get
            {
                if (Customer == null) return false;
                return !string.IsNullOrWhiteSpace(Customer.Name);
            }
        }

        #endregion


        #region ---- CONSTRUCTORS ----

        public CustomerViewModel()
        {
            _Customer = new Customer("Charlie");
            UpdateCommand = new CustomerUpdateCommand(this);
        }

        #endregion


        #region ---- METHODS ----

        public void SaveChanges()
        {
            System.Diagnostics.Debug.Assert(false, string.Format("{0} was updated.", Customer.Name));
        }

        #endregion

    }
}
