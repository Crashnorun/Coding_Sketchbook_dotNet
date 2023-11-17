using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_MVVM.Models;

namespace WPF_MVVM.ModelViews
{
    public class CounterMV : INotifyPropertyChanged
    {
        #region ---- PROPERTIES ----

        //public string WindowTitle
        //{
        //    get { return _windowTitle; }
        //    set { _windowTitle = value; OnPropertyChanged("WindowTitle"); }
        //}
        //private string _windowTitle;


        //public int CounterValue
        //{
        //    get { return _counterValue; }
        //    set { _counterValue = value; OnPropertyChanged("CounterValue"); }
        //}
        //private int _counterValue;

        private CounterClass counter;
        public CounterClass Counter
        {
            get { return counter; }
            set { counter = value; OnPropertyChanged("Counter"); }
        }
        #endregion


        #region ---- CONSTRUCTORS ----

        public CounterMV()
        {
            Counter = new CounterClass();
            //WindowTitle = Counter.WindowTitle;
            //CounterValue = Counter.CounterValue;
        }

        #endregion


        #region ---- METHODS ----

        public void Count(int MaxNum)
        {
            Counter.Count(MaxNum);
        }

        #endregion


        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
