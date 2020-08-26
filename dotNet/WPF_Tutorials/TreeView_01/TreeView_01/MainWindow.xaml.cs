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
using System.Diagnostics;

namespace TreeView_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // global property
        public List<Person> people = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();

            // add objects to list
            people.Add(new Person() { name = "charlie", age = 12, number = 32 });
            people.Add(new Person() { name = "bob", age = 2, number = 3 });
            people.Add(new Person() { name = "ron", age = 1, number = 23 });

            // set the parent object
            TreeViewItem parent = new TreeViewItem() { Header = "TreeViewItem Header", Name = "TreeViewItemName" };
            Tree_01.Items.Add(parent);

            // set the children objects
            foreach (Person person in people)
            {
                TreeViewItem tvi = new TreeViewItem() { Name = person.name, Header = person.name };
                parent.Items.Add(tvi);
            }
        }

        private void TreeView_Selected(object sender, RoutedEventArgs e) { }

        private void Tree_01_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e) {
            TreeView tv = sender as TreeView;                       // get the parent tree
            TreeViewItem tvi = tv.SelectedItem as TreeViewItem;     // get the selected item
            Person pep = people.Find(x => x.name == tvi.Name);      // get the selected person
            if (pep != null)
                txtBlock.Text = pep.ToString();                     // display person attributes
            else
                txtBlock.Text = string.Empty;
        }
    }

    public class Person
    {
        public string name;
        public int age;
        public int number;

        public override string ToString()
        {
            return $"{name} : {age} : {number}";
        }
    }
}
