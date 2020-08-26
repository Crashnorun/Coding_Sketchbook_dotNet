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
using ContactsApp.Classes;
using SQLite;

namespace ContactsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Contact> contacts;

        public MainWindow()
        {
            InitializeComponent();

            contacts = new List<Contact>();     // initilize list

            ReadDatabase();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewContactWindow newContactWindow = new NewContactWindow();
            newContactWindow.ShowDialog();

            ReadDatabase();
        }

        void ReadDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Contact>();

                // get the elements from the db
                // order the elements by name
                contacts = connection.Table<Contact>().ToList().OrderBy(x => x.Name).ToList();
            }

            if (contacts != null)
            {
                contactsListView.ItemsSource = contacts;        // data binding list of contacts to UI
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = sender as TextBox;

            // filter the elements in the list by the text in the search box.
            var filteredList = contacts.Where(x => x.Name.ToLower().Contains(searchBox.Text.ToLower())).ToList();
            contactsListView.ItemsSource = filteredList;
        }

        private void ContactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Contact selectedContact = (Contact)contactsListView.SelectedItem;
            if (selectedContact != null)
            {
                ContactDetails detailWindows = new ContactDetails(selectedContact);
                detailWindows.ShowDialog();
                ReadDatabase();
            }
        }
    }
}
