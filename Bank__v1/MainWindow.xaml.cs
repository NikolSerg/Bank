using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Bank__v1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            string users = null;
            string path = System.IO.Path.GetFullPath("../../../BankEmpManager/bin/Debug/net6.0/BankEmpManager.exe");
            if (!File.Exists("empdata.encrypt"))
                try
                {
                    File.Copy("../../../BankEmpManager/bin/Debug/net6.0/empdata.encrypt", "empdata.encrypt");
                }
                catch
                {
                    File.Create("empdata.encrypt").Close();
                }
            try
            {
                users = Decode(File.ReadAllText("empdata.encrypt"));
            }
            catch { users = null; }
            if (string.IsNullOrEmpty(users))
            {
                if (MessageBox.Show("Нет ни одного зрегистрированного пользователя!\nВоспользуйтесь приложением \"BankEmpManager\"\n" +
                    "Запустить приложение?",
                    "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Process pr = Process.Start(path);
                    pr.WaitForExit();
                }
            }
            InitializeComponent();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            string path = System.IO.Path.GetFullPath("../../../BankEmpManager/bin/Debug/net6.0/BankEmpManager.exe");
            this.Hide();
            Process pr = Process.Start(path);
            pr.WaitForExit();
            this.Show();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            (sender as Button).Background = new SolidColorBrush(Color.FromArgb(255, 255, 153, 0));
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(102, 102, 102));
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Background = new SolidColorBrush(Color.FromRgb(102, 102, 102));
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Background = new SolidColorBrush(Color.FromRgb(65, 65, 65));
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = new SolidColorBrush(Color.FromRgb(65, 65, 65));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool verified = Verify(loginBox.Text, passwordBox.Password);
            passwordBox.Password = "";
            if (!verified)
                MessageBox.Show("Неверный логин или пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool Verify(string login, string password)
        {
            string jUsers = null;
            bool verified = false;
            try
            {
                jUsers = Decode(File.ReadAllText("empdata.encrypt"));

                List<User> users = JsonConvert.DeserializeObject<List<User>>(jUsers);

                foreach (User user in users)
                {
                    if (login == user.PhoneNumber && password == user.Password)
                    {
                        DataBase data = new DataBase(user);
                        verified = true;
                        this.Close();
                    }
                }
                return verified;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Application.Current.Shutdown();
                return verified;
            }
        }

        private string Decode(string code)
        {
            string separator = Convert.ToByte('z').ToString();
            string[] parsedCode = code.Split(new string[] { "2z57" }, StringSplitOptions.RemoveEmptyEntries);
            code = parsedCode[0];
            int lenght = Convert.ToInt32(parsedCode[1]);
            string result = null;
            for (int i = 0; i < lenght; i++)
            {
                separator += i.ToString() + Convert.ToByte('|').ToString();
                string lastSeparator;
                if (i != 0)
                {
                    lastSeparator = Convert.ToByte('z').ToString() + (i - 1).ToString() + Convert.ToByte('|').ToString();
                    parsedCode = code.Split(new string[] { lastSeparator, separator }, StringSplitOptions.None);
                    result += Convert.ToChar(Convert.ToByte(parsedCode[1]));
                }
                else
                {
                    parsedCode = code.Split(new string[] { separator }, StringSplitOptions.None);
                    result += Convert.ToChar(Convert.ToByte(parsedCode[0]));
                }
                separator = Convert.ToByte('z').ToString();
            }
            return result;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (loginBox.IsFocused)
                {
                    passwordBox.Focus();
                }
                else
                {
                    bool verified = Verify(loginBox.Text, passwordBox.Password);
                    passwordBox.Password = "";
                    if (!verified)
                        MessageBox.Show("Неверный логин или пароль.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
