using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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

// https://www.youtube.com/watch?v=snkDYT1Qz6g

namespace MaterialDesignGettingStarted_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region ---- PROPERTIES ----
      
        private BackgroundWorker worker = new BackgroundWorker();

        public int counterMax = 110;

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            worker.WorkerReportsProgress = true;                // worker reports progress
            worker.WorkerSupportsCancellation = true;           // worker can be cancled
            worker.DoWork += worker_DoWork;                     // main work to be done
            worker.ProgressChanged += worker_ProgressChanged;   // when progress has changed / worker has done some work
            worker.RunWorkerCompleted += workder_Completed;     // when worker is done

            this.pbar.Maximum = counterMax;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!worker.IsBusy)
            {
                worker.RunWorkerAsync();                        // starts the background worker
                btn.Content = "Stop";
            }
            else
            {
                worker.CancelAsync();                           // stops the background workder
                btn.Content = "Start";
            }
        }


        /// <summary>
        /// this is where most of the work is done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= this.counterMax; i++)
            {
                System.Threading.Thread.Sleep(100);

                // this will automatically trigger the Progress changed function / event
                (sender as BackgroundWorker).ReportProgress(i);

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
            }
        }

        /// <summary>
        /// this is where the worker sends data back to the UI thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pbar.Value = e.ProgressPercentage;
            this.lbl.Content = e.ProgressPercentage;
        }

        /// <summary>
        /// this is what tells the UI the worker is done
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workder_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) btn.Content = "Stopped";
            else btn.Content = "Start";
        }
    }
}

