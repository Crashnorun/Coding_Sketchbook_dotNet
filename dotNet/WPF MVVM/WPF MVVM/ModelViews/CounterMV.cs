using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Commands;
using WPF_MVVM.Models;

namespace WPF_MVVM.ModelViews
{
    public class CounterMV : INotifyPropertyChanged
    {
        #region ---- PROPERTIES ----

         private CounterClass counter;
        public CounterClass Counter
        {
            get { return counter; }
            set { counter = value; OnPropertyChanged("Counter"); }
        }

        private int maxNum;

        public int MaxNum
        {
            get { return maxNum; }
            set { maxNum = value; OnPropertyChanged("MaxNum"); }
        }

        #endregion


        #region ---- COMMANDS ----

        private RelayCommand startCountingCommand;

        public RelayCommand StartCountingCommand
        {
            get { return startCountingCommand; }
        }

        public void Count()
        {
            Counter.Count(MaxNum);
        }

        #endregion


        #region ---- CONSTRUCTORS ----

        public CounterMV()
        {
            Counter = new CounterClass();
            MaxNum = 100;
            startCountingCommand = new RelayCommand(Count);
        }

        #endregion


        #region ---- METHODS ----

       

        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
