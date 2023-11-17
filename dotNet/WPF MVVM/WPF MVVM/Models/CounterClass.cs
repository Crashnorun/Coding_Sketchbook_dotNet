using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPF_MVVM.Models
{
    public class CounterClass
    {


        #region ---- PROPERTIES ----

        private string _windowTitle;

        public string WindowTitle
        {
            get { return _windowTitle; }
            set { _windowTitle = value; }
        }

        private int _counterValue;

        public int CounterValue
        {
            get { return _counterValue; }
            set { _counterValue = value; }
        }

        #endregion


        #region ---- CONSTRUCTORS ----

        public CounterClass()
        {
            _counterValue = 0;
            CounterValue = 0;
            _windowTitle = "Crashnorun";
            WindowTitle = "Crashnorun";
        }
        #endregion


        public void Count(int max)
        {
            for (int i = 0; i < max; i++)
            {
                CounterValue = i;
                Thread.Sleep(25);
            }
        }
    }
}
