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
using System.Windows.Shapes;

namespace WPFProgressBars
{
    /// <summary>
    /// Interaction logic for SimpleExample.xaml
    /// </summary>
    public partial class SimpleExample : Window
    {
        public SimpleExample()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Simulated data structure: List of Lists
            List<List<int>> data = new List<List<int>>
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 4, 5, 6 },
                new List<int> { 7, 8, 9 }
            };

            pbar1.Maximum = data.Count;
            pbar2.Maximum = 100;
            pbar2.Minimum = 0;


            // Process the outer list
            for (int i = 0; i < data.Count; i++)
            {
                // Update the global progress bar
                pbar1.Value = i + 1;
                lbl1.Content = i + 1;

                // Process the inner list
                for (int j = 0; j < data[i].Count; j++)
                {
                    // Simulated processing delay
                    await Task.Delay(200);

                    // Update the granular progress bar
                    pbar2.Value = (j + 1) * (100.0 / data[i].Count);
                    lbl2.Content = (j + 1) * (100.0 / data[i].Count);
                }

                // Reset the granular progress bar for the next outer list item
                pbar2.Value = 0;
            }

            // Reset the global progress bar
            pbar1.Value = 0;

            MessageBox.Show("Processing Complete!");
        }

        // do stuff here



    }
}
