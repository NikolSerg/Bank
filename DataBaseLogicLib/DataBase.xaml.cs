using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Bank__v1
{
    /// <summary>
    /// Логика взаимодействия для DataBase.xaml
    /// </summary>
    public partial class DataBase : Window
    {
        string basePath = "baseEncrypted.encrypt";
        Popup popup;
        public DataBase(User user)
        {
            if (!File.Exists(basePath)) File.Create(basePath).Close();
            string jsonData = DBDecryptor.GetDataBase(basePath);
            Person.Clients = JsonConvert.DeserializeObject<ObservableCollection<Person>>(jsonData) ?? new ObservableCollection<Person>();
            for (int i = 0; i < Person.Clients.Count; i++)
            {
                foreach (NotDepAccount acc in Person.Clients[i].Accounts)
                {
                    if (acc != null)
                    {
                        
                        Person.PersonsAccNumbersBase.Add(acc.AccNumber, acc);
                        Person.Clients[i].OnLoad(acc);
                    }
                }
                
            }



            Person.OnChange += Person_OnChange;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromDays(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            //for (int i = 0; i < 50; i++)
            //{
            //    Person p = new Person();
            //    Thread.Sleep(50);
            //}

            InitializeComponent();
            ClientsDataGrid.CanUserAddRows = false;
            ClientsDataGrid.CanUserDeleteRows = false;
            ClientsDataGrid.ItemsSource = Person.Clients;

            popup = new Popup();
            TextBlock textBlock = new TextBlock();
            textBlock.Background = Brushes.Yellow;
            textBlock.TextWrapping = TextWrapping.Wrap;
            
            popup.Child = textBlock;
            popup.StaysOpen = false;
            popup.Placement = PlacementMode.Relative;
            popup.MaxHeight = 200;
            popup.Width = 400;
            popup.HorizontalOffset = 400;
            popup.VerticalOffset = -30;
            popup.AllowsTransparency = true;
            popup.Closed += Popup_Closed;
            grid.Children.Add(popup);


            this.Closing += Window_Closing;
            this.Show();
            Start(user);
            userData = user;

            ClientsDataGrid.CellEditEnding += ClientsDataGrid_CellEditEnding;
            ClientsDataGrid.BeginningEdit += ClientsDataGrid_BeginningEdit;
            ClientsDataGrid.GotFocus += ClientsDataGrid_GotFocus;

        }

        DispatcherTimer popUpTimer = new DispatcherTimer();

        private void Person_OnChange(Person client, User user, DateTime time, string changes)
        {
            client.Changes.Add(time, $"{user.PhoneNumber}, {user.Post}\n {changes}");
            (popup.Child as TextBlock).Text = $"{time}, {user.PhoneNumber}, {user.Post}\n {changes}";
            (popup.Child as TextBlock).Opacity = 0.8;
            popup.IsOpen = true;
            popUpTimer.Interval = TimeSpan.FromMilliseconds(20);
            popUpTimer.Tick += PopUpTimer_Tick;
            popUpTimer.Start();

        }

        private void Popup_Closed(object sender, EventArgs e)
        {
            popUpTimer.Stop();
            popUpTimer.Tick -= PopUpTimer_Tick;
        }

        private void PopUpTimer_Tick(object sender, EventArgs e)
        {
            if ((popup.Child as TextBlock).Opacity != 0)
            {
                (popup.Child as TextBlock).Opacity -= 0.01;
            }
            else
            {
                popUpTimer.Tick -= PopUpTimer_Tick;
                popUpTimer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (Account acc in Person.PersonsAccNumbersBase.Values)
            {
                switch (acc.AccType)
                {
                    case "Депозитный":
                        if ((DateTime.Now - acc.OpenDate).Days % 30 == 0 && acc.IsActive)
                        {
                            acc.AccAmount *= 1.0042;
                        }
                        break;
                    case "Недепозитный":
                        if ((DateTime.Now - acc.OpenDate).Days % 365 == 0 && acc.IsActive)
                        {
                            acc.AccAmount *= 1.01;
                        }
                        break;
                }
            }
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
            if ((ClientsDataGrid.CurrentCell.Item as Person).Changes.Count > 0)
                showHistoryButton.IsEnabled = true;
            else showHistoryButton.IsEnabled = false;
            if (user is Manager)
                removeButton.IsEnabled = true;
            openAccButton.IsEnabled = true;
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
            DBDecryptor.SaveDataBase(jsonData, basePath);
        }

        private void showHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            string changes = "";
            Window HistoryWindow = new Window();
            HistoryWindow.Width = 400;
            HistoryWindow.Height = 400;
            HistoryWindow.Topmost = true;
            HistoryWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            HistoryWindow.Title = "История изменений";
            Canvas canvas = new Canvas();
            HistoryWindow.Content = canvas;
            foreach (DateTime time in currentPerson.Changes.Keys)
            {
                if (user is Manager)
                {
                    changes += $"{time} {currentPerson.Changes[time]}\n\n";
                }
                else
                {
                    if (!currentPerson.Changes[time].Contains("Серия, номер паспорта"))
                        changes += $"{time} {currentPerson.Changes[time]}\n\n";
                    else changes += $"{time} Изменены серия, номер паспорта\n\n";
                }
            }
            canvas.Children.Add(new TextBox());
            (canvas.Children[0] as TextBox).Text = changes;
            (canvas.Children[0] as TextBox).Height = HistoryWindow.Height - 20;
            (canvas.Children[0] as TextBox).Width = HistoryWindow.Width;
            (canvas.Children[0] as TextBox).IsReadOnly = true;
            (canvas.Children[0] as TextBox).VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            HistoryWindow.ShowDialog();

        }

        private void openAccButton_Click(object sender, RoutedEventArgs e)
        {
            BankAccountsList window = new BankAccountsList(currentPerson, userData);
            
            window.ShowDialog();
        }
    }
}
