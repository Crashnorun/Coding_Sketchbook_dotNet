using System;
using System.Collections.Generic;
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
using WPF_MVVM.ModelViews;

namespace WPF_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CounterMV counterMV;

        public MainWindow()
        {
            InitializeComponent();

            counterMV = new CounterMV();
            this.DataContext = counterMV;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //  infoPanel.Count(100);
        }
    }
}
