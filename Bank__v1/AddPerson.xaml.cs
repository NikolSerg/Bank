using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Bank__v1
{
    /// <summary>
    /// Логика взаимодействия для AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        public AddPerson(User user)
        {
            InitializeComponent();
            this.Focus();
            this.user = user;
        }
        User user;
        bool valid = false;
        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            ValidCheck();
            if (valid)
            {
                Person person = new Person(firstNameBox.Text, lastNameBox.Text, patroymicBox.Text, phoneBox.Text, EditPassportText());
                person.Changes.Add(DateTime.Now, $"Добавлен новый клиент\nДобавил: {user.PhoneNumber}, {user.Post}");
                firstNameBox.Clear();
                lastNameBox.Clear();
                patroymicBox.Clear();
                phoneBox.Text = "+7";
                passportBox.Clear();
                this.Close();
            }
        }

        SolidColorBrush invalidBrush = new SolidColorBrush(Color.FromArgb(70, 255, 0, 0));

        private void ValidCheck()
        {
            if (passportBox.Text.Length == 10 &&
                phoneBox.Text.Length == 12 &&
                firstNameBox.Text.Length > 0 &&
                lastNameBox.Text.Length > 0 &&
                patroymicBox.Text.Length > 0)
            {
                bool exist = false;
                foreach (Person client in Person.Clients)
                {
                    if (EditPassportText() == client.Passport)
                    {
                        MessageBox.Show("Пользователь с этими пасспортными данными уже зарегистрирован!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        passportBox.Background = invalidBrush;
                        exist = true;

                        break;
                    }
                }
                if (!exist) valid = true;
            }
        }

        private string EditPassportText()
        {
            string text = passportBox.Text;
            string editedText = null;
            for (int i = 0; i < text.Length; i++)
            {
                editedText += text[i];
                if (i == 3)
                    editedText += " ";
            }
            return editedText;
        }

        private void phoneBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (phoneBox.Text.Length != 12)
            {
                phoneBox.Background = invalidBrush;
            }
            else phoneBox.Background = Brushes.White;
        }

        private void passportBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (passportBox.Text.Length != 10)
            {
                passportBox.Background = invalidBrush;
            }
            else
                passportBox.Background = Brushes.White;
        }

        private void lastNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length == 0)
            {
                (sender as TextBox).Background = invalidBrush;
            }
            else
                (sender as TextBox).Background = Brushes.White;
        }

        private void addingWindow_KeyDown(object sender, KeyEventArgs e)


        {
            if (e.Key == Key.Enter)
            {
                if (lastNameBox.IsFocused) firstNameBox.Focus();
                else if (firstNameBox.IsFocused) patroymicBox.Focus();
                else if (patroymicBox.IsFocused) phoneBox.Focus();
                else if (phoneBox.IsFocused) passportBox.Focus();
                else if (passportBox.IsFocused) acceptButton.Focus();
                else if (acceptButton.IsFocused)
                {
                    ValidCheck();
                    if (valid)
                    {
                        Person person = new Person(firstNameBox.Text, lastNameBox.Text, patroymicBox.Text, phoneBox.Text, EditPassportText());
                        firstNameBox.Clear();
                        lastNameBox.Clear();
                        patroymicBox.Clear();
                        phoneBox.Text = "+7";
                        passportBox.Clear();
                        this.Close();
                    }
                }
            }
        }
    }
}
