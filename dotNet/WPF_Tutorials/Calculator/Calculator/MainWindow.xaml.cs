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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
        }


        #region NUMBERS

        private void NumberClick(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;

            if (sender == btn_zero)
                selectedValue = 0;
            if (sender == btn_one)
                selectedValue = 1;
            if (sender == btn_two)
                selectedValue = 2;
            if (sender == btn_three)
                selectedValue = 3;
            if (sender == btn_four)
                selectedValue = 4;
            if (sender == btn_five)
                selectedValue = 5;
            if (sender == btn_six)
                selectedValue = 6;
            if (sender == btn_seven)
                selectedValue = 7;
            if (sender == btn_eight)
                selectedValue = 8;
            if (sender == btn_nine)
                selectedValue = 9;

            if (lbl_result.Content.ToString() == "0") { lbl_result.Content = $"{selectedValue}"; }
            else { lbl_result.Content = $"{lbl_result.Content}{selectedValue}"; }
        }

        #endregion

        #region FUNCTIONS

        private void OperationClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lbl_result.Content.ToString(), out lastNumber)) { }
            lbl_result.Content = 0;

            if (sender == btn_minus) selectedOperator = SelectedOperator.Subtraction;
            if (sender == btn_addition) selectedOperator = SelectedOperator.Addition;
            if (sender == btn_multiply) selectedOperator = SelectedOperator.Multiplication;
            if (sender == btn_divide) selectedOperator = SelectedOperator.Division;
        }

        private void Btn_decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!lbl_result.Content.ToString().Contains("."))
            {
                lbl_result.Content = $"{lbl_result.Content}.";
            }
        }

        private void Btn_equal_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(lbl_result.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Addition(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Division(lastNumber, newNumber);
                        break;
                }
                lbl_result.Content = result;
            }
        }

        private void Btn_negative_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lbl_result.Content.ToString(), out lastNumber))
                lbl_result.Content = -1 * lastNumber;
        }

        private void Btn_percent_Click(object sender, RoutedEventArgs e)
        {
            double tempVal;

            if (double.TryParse(lbl_result.Content.ToString(), out tempVal))
            {
                tempVal = (tempVal / 100);
                if (lastNumber != 0)
                    tempVal *= lastNumber;
                lbl_result.Content = tempVal;
            }
        }

        private void Btn_AC_Click(object sender, RoutedEventArgs e)
        {
            lbl_result.Content = "0";
            lastNumber = 0;
            result = 0;
        }
        #endregion
    }


    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Addition(double x, double y)
        {
            return x + y;
        }

        public static double Subtraction(double x, double y)
        {
            return x - y;
        }

        public static double Multiply(double x, double y)
        {
            return x * y;
        }

        public static double Division(double x, double y)
        {
            if (y == 0)
            {
                MessageBox.Show("Cannot divide by zero", "Invalid Operation", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return 0;
            }

            return x / y;
        }
    }

}
