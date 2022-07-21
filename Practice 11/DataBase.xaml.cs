using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Shapes;

namespace Practice_11
{
    /// <summary>
    /// Логика взаимодействия для DataBase.xaml
    /// </summary>
    public partial class DataBase : Window
    {
        public DataBase()
        {
            InitializeComponent();
            if (!File.Exists("base.json")) File.Create("base.json").Close();
            string jsonData = File.ReadAllText("base.json");
            Person.Clients = JsonConvert.DeserializeObject<ObservableCollection<Person>>(jsonData) ?? new ObservableCollection<Person>();
            //for (int i = 0; i < 50; i++)
            //{
            //    Person p = new Person();
            //    Thread.Sleep(50);
            //}

            ClientsDataGrid.CanUserAddRows = false;
            ClientsDataGrid.CanUserDeleteRows = false;
            ClientsDataGrid.ItemsSource = Person.Clients;
        }

        public TextBox currentBox;
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            currentBox = (TextBox)sender;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string jsonData = JsonConvert.SerializeObject(Person.Clients);
            File.WriteAllText("base.json", jsonData);
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addingWindow = new AddPerson();
            this.Hide();
            addingWindow.ShowDialog();
            this.Show();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить клиента из базы?\nОтменить это действие будет невозможно.", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Person.Clients.Remove(remove);
            }
            (sender as Button).IsEnabled = false;
               
        }

        Person remove;

        private void ClientsDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            removeButton.IsEnabled = true;
        }

        private void ClientsDataGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (ClientsDataGrid.CurrentCell.Item.GetType() == typeof(Person))
            remove = ClientsDataGrid.CurrentCell.Item as Person;
        }
    }
}
