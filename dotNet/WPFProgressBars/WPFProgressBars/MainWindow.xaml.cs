using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WPFProgressBars
{
    #region ---- NOTES ----

    /*There are two examples of how to create a progress bar
     */

    #endregion



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region ---- PROPERTIES ----

        #region -- First Example --
        private BackgroundWorker bgWorker = new BackgroundWorker();

        public event PropertyChangedEventHandler PropertyChanged;

        private int workerState;

        public int WorkerState
        {
            get { return workerState; }
            set
            {
                workerState = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
            }
        }

        public string LabelValue
        {
            get { return labelvalue; }
            set
            {
                labelvalue = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LabelValue"));
            }
        }

        public string labelvalue;

        #endregion


        #region -- Second Example --

        private BackgroundWorker bgworker2 = new BackgroundWorker();

        public event PropertyChangedEventHandler PropertyChanged2;

        private int workerState2;

        public int WorkerState2
        {
            get { return workerState2; }
            set
            {
                workerState2 = value;
                if (PropertyChanged2 != null) PropertyChanged2(this, new PropertyChangedEventArgs("WorkerState2"));
            }
        }

        public string LabelValue2
        {
            get { return labelvalue2; }
            set
            {
                labelvalue2 = value;
                if (PropertyChanged2 != null) PropertyChanged2(this, new PropertyChangedEventArgs("LabelValue"));
            }
        }

        public string labelvalue2;

        #endregion


        #endregion


        public MainWindow()
        {
            InitializeComponent();


            SimpleExample ex1 = new SimpleExample();
            ex1.Show();
            ex1.pbar1.Value = 10;

        }



        #region ---- EVENTS ----

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            bgWorker.DoWork += (s, se) =>
            {
                for (int i = 0; i < 100; i++)
                {
                    System.Threading.Thread.Sleep(50);
                    WorkerState = i;
                    LabelValue = "Value: " + i;
                }

                MessageBox.Show("Work is done");

            };
            bgWorker.RunWorkerAsync();
        }


        private void btn_Click2(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            bgworker2.DoWork += (sender2, eventargs2) =>
            {
                lblMain2.Content = "Value: " + WorkerState2.ToString();
            };
            bgworker2.RunWorkerAsync();

            bgworker2.WorkerReportsProgress = true;
            bgworker2.DoWork += workerDoWork;
            bgworker2.ProgressChanged += updateUI;
            bgworker2.RunWorkerAsync();

        }

        private void workerDoWork(object sender, DoWorkEventArgs e)
        {
            int min = 0;
            int max = 100;

            for (int i = min; i < max; i++)
            {
                Thread.Sleep(50);
                bgworker2.ReportProgress(i, "Current status:");
            }
        }


        private void updateUI(object sender, ProgressChangedEventArgs e)
        {
            lblMain2.Content = e.ProgressPercentage;
            pbarMain2.Value = e.ProgressPercentage;
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimpleExample ex1 = new SimpleExample();
            ex1.Show();


            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);


            }



        }
    }
}
