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

namespace Bank__v1
{
    /// <summary>
    /// Логика взаимодействия для BankAccountsList.xaml
    /// </summary>
    public partial class BankAccountsList : Window
    {
        public BankAccountsList(Person person)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            currentPerson = person;
            Bind();
            topUpButton.IsEnabled = false;
            transferButton.IsEnabled = false;
        }
        TextBlock block = new TextBlock();
        private void Bind()
        {
            if (currentPerson.Accounts.All(p => p is null))
            {
                block.Text = "Не найдено ни одного счёта";
                block.Width = this.Width - 20;
                block.Height = 26;
                block.HorizontalAlignment = HorizontalAlignment.Center;
                block.VerticalAlignment = VerticalAlignment.Top;
                block.FontSize = 22;
                block.Margin = new Thickness(10, 0, 0, 0);

                grid.Children.Remove(accsList);
                grid.Children.Add(block);
            }
            else if (currentPerson.Accounts.Any(p => p is null))
            {
                if (grid.Children.Contains(block))
                {
                    grid.Children.Remove(block);
                    grid.Children.Add(accsList);
                }
                foreach (var account in currentPerson.Accounts)
                {
                    if (account != null)
                        accsList.ItemsSource = new Account[] { account };
                }
            }
            else
            {
                if (grid.Children.Contains(block))
                {
                    grid.Children.Remove(block);
                    grid.Children.Add(accsList);
                }
                else
                {
                    grid.Children.Remove(accsList);
                    grid.Children.Add(accsList);
                }
                
                accsList.ItemsSource = new Account[] { currentPerson.Accounts[0], currentPerson.Accounts[1] };
            }
        }

        Person currentPerson;

        private void openAccButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 290;
            window.Height = 200;
            window.ResizeMode = ResizeMode.NoResize;
            window.Title = "Выберите тип счёта";
            StackPanel panel = new StackPanel();
            Canvas canvas = new Canvas();
            window.Content = canvas;
            panel.Width = 280;
            canvas.Children.Add(panel);

            Button openDepAccBtn = new Button();
            openDepAccBtn.Content = "Открыть депозитный счёт";
            openDepAccBtn.Click += new RoutedEventHandler((a, b) => Btn_Click(a, b, window));
            openDepAccBtn.Margin = new Thickness(0, 0, 0, 10);
            openDepAccBtn.Height = 40;
            openDepAccBtn.Width = 270;
            openDepAccBtn.HorizontalAlignment = HorizontalAlignment.Center;

            Button openNotDepAccBtn = new Button();
            openNotDepAccBtn.Content = "Открыть недепозитный счёт";
            openNotDepAccBtn.Click += new RoutedEventHandler((a, b) => OpenNotDepAccBtn_Click(a, b, window));
            openNotDepAccBtn.Margin = new Thickness(0, 10, 0, 10);
            openNotDepAccBtn.Height = 40;
            openNotDepAccBtn.Width = 270;
            openNotDepAccBtn.HorizontalAlignment = HorizontalAlignment.Center;

            Button cancelBtn = new Button();
            cancelBtn.Content = "Отмена";
            cancelBtn.Click += new RoutedEventHandler((a, b) => CancelBtn_Click(a, b, window));
            cancelBtn.Margin = new Thickness(0, 10, 0, 10);
            cancelBtn.Height = 40;
            cancelBtn.Width = 270;
            cancelBtn.HorizontalAlignment = HorizontalAlignment.Center;

            panel.Children.Add(openDepAccBtn);
            panel.Children.Add(openNotDepAccBtn);
            panel.Children.Add(cancelBtn);
            panel.HorizontalAlignment = HorizontalAlignment.Center;
            window.ShowDialog();
            Bind();
        }

        private void OpenNotDepAccBtn_Click(object sender, RoutedEventArgs e, Window window)
        {
            if(currentPerson.AddAccount(AccountManager<NotDepAccount>.OpenAcc()))
                MessageBox.Show("Счёт этого типа уже открыт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            window.Close();
            Bind();
        }

        private void Btn_Click(object sender, RoutedEventArgs e, Window window)
        {
            if (currentPerson.AddAccount(AccountManager<DepAccount>.OpenAcc()))
                MessageBox.Show("Счёт этого типа уже открыт", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            window.Close();
            Bind();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e, Window window)
        {
            Bind();
            window.Close();
        }

        private void topUpButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.ResizeMode = ResizeMode.NoResize;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Width = 400;
            window.Height = 150;
            Canvas canvas = new Canvas();
            window.Content = canvas;

            StackPanel panel = new StackPanel();
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            TextBox amount = new TextBox();
            amount.Width = 350;
            amount.HorizontalAlignment = HorizontalAlignment.Center;
            amount.VerticalAlignment = VerticalAlignment.Top;
            amount.Margin = new Thickness(10, 10, 10, 10);

            TextBox accNumberBox = new TextBox();
            accNumberBox.Width = 350;
            accNumberBox.HorizontalAlignment = HorizontalAlignment.Center;
            accNumberBox.VerticalAlignment = VerticalAlignment.Top;
            accNumberBox.Margin = new Thickness(10, 10, 10, 10);

            Label label1 = new Label();
            label1.Width = 350;
            label1.HorizontalAlignment = HorizontalAlignment.Left;
            label1.VerticalAlignment = VerticalAlignment.Top;
            label1.Margin = new Thickness(10, 2, 10, 0);
            label1.Content = "Введите номер счёта:";

            Label label2 = new Label();
            label2.Width = 350;
            label2.HorizontalAlignment = HorizontalAlignment.Left;
            label2.VerticalAlignment = VerticalAlignment.Top;
            label2.Margin = new Thickness(10, 10, 10, 0);
            label2.Content = "Введите сумму, которую хотите перевести:";

            Button acceptBtn = new Button();
            acceptBtn.Margin = new Thickness(10, 10, 10, 10);

            Button cancelBtn = new Button();
            cancelBtn.Content = "Отмена";
            cancelBtn.Click += new RoutedEventHandler((a, b) => CancelBtn_Click1(window));
            cancelBtn.Margin = new Thickness(10, 10, 10, 0);

            switch ((sender as Button).Content)
            {
                case "Пополнить счёт":
                    window.Title = "Пополнение счёта";
                    acceptBtn.Content = "Пополнить";
                    label2.Content = "Введите сумму:";
                    label2.Margin = new Thickness(10, 0, 10, 0);
                    panel.Children.Add(label2 as Label);
                    acceptBtn.Click += new RoutedEventHandler((a, b) => AcceptBtn_Click(amount.Text, window));
                    break;
                case "Перевести на другой счёт":
                    window.Title = "Перевод на другой счёт";
                    window.Height = 260;
                    panel.Children.Add(label1 as Label);
                    panel.Children.Add(accNumberBox);
                    panel.Children.Add(label2 as Label);
                    acceptBtn.Content = "Перевести";
                    acceptBtn.Click += new RoutedEventHandler((a, b) => AcceptBtn_Click1(accNumberBox.Text, amount.Text, window));
                    break;
            }

            panel.Children.Add(amount);
            panel.Children.Add(acceptBtn);
            panel.Children.Add(cancelBtn);
            canvas.Children.Add(panel);


            window.ShowDialog();

            Bind();

        }

        private void AcceptBtn_Click1(string accNumber, string amount, Window window)
        {
            if (ValidateAccNumber(accNumber) && Validate(amount, currentAccount.Get().AccAmount))
            {
                destAccount.Get().Transfer(currentAccount.Get(), Convert.ToDouble(amount));
                MessageBox.Show("Средства успешно переведены", "Перевод", MessageBoxButton.OK);
            }
            else if (!ValidateAccNumber(accNumber))
                MessageBox.Show("Неверно введен номер счёта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (!Validate(amount, currentAccount.Get().AccAmount))
                MessageBox.Show("Не верный формат числа или недостаточно средств на счёте", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            window.Close();
            Bind();
        }

        private bool ValidateAccNumber(string accNumber)
        {
            bool isValid = false;
            try
            {
                ulong number = Convert.ToUInt64(accNumber);
                if (Person.PersonsAccNumbersBase.ContainsKey(number))
                {
                    destAccount = Person.PersonsAccNumbersBase[number];
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isValid;
        }

        private void CancelBtn_Click1(Window window)
        {
            Bind();
            window.Close();
        }

        private void AcceptBtn_Click(string amount, Window window)
        {
            if (Validate(amount))
            {
                currentAccount.TopUp(Convert.ToDouble(amount));
                MessageBox.Show("Счёт успешно пополнен", "Пополнение", MessageBoxButton.OK);
                window.Close();
                Bind();
            }
            else
            {
                MessageBox.Show("Неверно указано значение суммы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool Validate(string amount)
        {
            bool isValid = true;
            try
            {
                Convert.ToDouble(amount);
            }
            catch
            {
                isValid = false;
            }
            return isValid;
        }

        private bool Validate(string amount, double available)
        {
            bool isAmountValid = Validate(amount);
            bool isAvailable = true;
            try
            {
                double dAmount = Convert.ToDouble(amount);
                if (available < dAmount)
                    isAvailable = false;
            }
            catch { isAvailable = false; }

            return isAmountValid && isAvailable;
        }

        ITransfer<DepAccount> destAccount;
        ITopUp<Account> currentAccount;
        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ulong accNumber = Convert.ToUInt64((((sender as StackPanel).Children[1] as StackPanel).Children[1] as TextBlock).Text);
            currentAccount = Person.PersonsAccNumbersBase[accNumber];
            topUpButton.IsEnabled = true;
            transferButton.IsEnabled = true;
        }
    }
}
