using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_MVVM.Models
{
    public class CounterClass : INotifyPropertyChanged
    {
        #region ---- INotifyPropertyChanged ----

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        #endregion


        #region ---- PROPERTIES ----

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; OnPropertyChanged("WindowTitle"); }
        }

        private int _counterValue;

        public int CounterValue
        {
            get { return _counterValue; }
            set { _counterValue = value; OnPropertyChanged("CounterValue"); }
        }

        private double currentValue;

        public double CurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; OnPropertyChanged("CurrentValue"); }
        }


        #endregion


        #region ---- CONSTRUCTORS ----

        public CounterClass()
        {
            //_counterValue = 0;
           //CounterValue = 0;
            //_windowTitle = "Crashnorun";
            //WindowTitle = "Crashnorun";
        }
        #endregion


        public async Task Count(int max)
        {
            await Task.Run(() =>
            {
                CounterValue = max;
                for (int i = 0; i < max; i++)
                {
                    CurrentValue = i;
                    WindowTitle = "Crashnorun " + i;
                    Debug.WriteLine(CurrentValue);
                    Thread.Sleep(10);
                }
            });
        }
    }
}
