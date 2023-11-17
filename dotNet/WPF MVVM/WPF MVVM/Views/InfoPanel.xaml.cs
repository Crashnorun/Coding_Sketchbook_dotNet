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

namespace WPF_MVVM.Views
{
    /// <summary>
    /// Interaction logic for InfoPanel.xaml
    /// </summary>
    public partial class InfoPanel : UserControl
    {

        public InfoPanel()
        {
            InitializeComponent();

            //this.txtTitle.Text = "Template Title";
            ToolTip ttTitle = new ToolTip() { Content = "Example Tooltip" };
            txtTitle.ToolTip = ttTitle;

           // this.lblCount.Content = "0";

            this.pbar.Minimum = 0;
            this.pbar.Maximum = 100;
            this.pbar.Value = 0;
            ToolTip ttPbar = new ToolTip() { Content = "Counting up to 100" };
            pbar.ToolTip = ttPbar;

        }

        public void Count(int max)
        {
           // counterVM.Count(max);
        }

        private void btnCount_Click(object sender, RoutedEventArgs e)
        {
          //  counterVM.Count((int)this.pbar.Maximum);
        }
    }
}
