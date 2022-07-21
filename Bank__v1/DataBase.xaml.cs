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

namespace Bank__v1
{
    /// <summary>
    /// Логика взаимодействия для DataBase.xaml
    /// </summary>
    public partial class DataBase : Window
    {
        public DataBase(User user)
        {
            if (!File.Exists("base.json")) File.Create("base.json").Close();
            string jsonData = File.ReadAllText("base.json");
            Person.Clients = JsonConvert.DeserializeObject<ObservableCollection<Person>>(jsonData) ?? new ObservableCollection<Person>();
            //for (int i = 0; i < 50; i++)
            //{
            //    Person p = new Person();
            //    Thread.Sleep(50);
            //}

            InitializeComponent();
            ClientsDataGrid.CanUserAddRows = false;
            ClientsDataGrid.CanUserDeleteRows = false;
            ClientsDataGrid.ItemsSource = Person.Clients;

            this.Closing += Window_Closing;
            this.Show();
            Start(user);
            userData = user;

            ClientsDataGrid.CellEditEnding += ClientsDataGrid_CellEditEnding;
            ClientsDataGrid.BeginningEdit += ClientsDataGrid_BeginningEdit;
            ClientsDataGrid.GotFocus += ClientsDataGrid_GotFocus;
            
        }

        private void ClientsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            editingPerson = ClientsDataGrid.SelectedItem as Person;
        }
        Person editingPerson;

        private void ClientsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            user.Validate(e, editingPerson, userData);
        }

        private void ClientsDataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            if (user is Manager)
            {
                removeButton.IsEnabled = true;
                if ((ClientsDataGrid.CurrentCell.Item as Person).Changes.Count > 0)
                    showHistoryButton.IsEnabled = true;
                else showHistoryButton.IsEnabled = false;
            }
            currentPerson = (Person)(ClientsDataGrid.CurrentCell.Item);
        }
        Person currentPerson;

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addingWindow = new AddPerson(userData);
            this.Hide();
            addingWindow.ShowDialog();
            this.Show();
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            //Удалить текущего или открыть фильтр для поиска и тд *еще не сделано
            if (MessageBox.Show("Вы действительно хотите удалить клиента из базы?\nОтменить это действие будет невозможно.", "Удалить", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Person.Clients.Remove(currentPerson);
            }
            (sender as Button).IsEnabled = false;
            showHistoryButton.IsEnabled = false;
        }

        Consultant user;
        User userData;

        public void Start(User user)
        
        {
            if (user.Post == "Consultant")
                this.user = new Consultant(this);
            else
                this.user = new Manager(this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string jsonData = JsonConvert.SerializeObject(Person.Clients);
            File.WriteAllText("base.json", jsonData);
        }

        private void showHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            string changes = "";
            foreach (DateTime time in currentPerson.Changes.Keys)
            {
                changes += $"{time} {currentPerson.Changes[time]}\n\n";
            }
            MessageBox.Show(changes);
        }
    }
}
