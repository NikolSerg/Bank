using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Bank__v1
{
    public class Manager : Consultant, IConsultantMethodsInterface
    {
        DataBase window;
        public Manager(DataBase window) : base(window)
        {
            this.window = window;
            window.addButton.IsEnabled = true;
        }

        public override void Validate(DataGridCellEditEndingEventArgs e, Person p, User u)
        {
            if (e.Column.Header.ToString() == "Фамилия" || e.Column.Header.ToString() == "Имя" || e.Column.Header.ToString() == "Отчество")
            {
                if (FullNameValidate(e, p))
                {
                    switch (e.Column.Header.ToString())
                    {
                        case "Фамилия":
                            string changes = $"Изменил: Фамилия: {p.LastName} => {(e.EditingElement as TextBox).Text}";
                            p.Change(u, changes);
                            break;
                        case "Имя":
                            string changes1 = $"Изменил: Имя: {p.FirstName} => {(e.EditingElement as TextBox).Text}";
                            p.Change(u, changes1);
                            break;
                        case "Отчество":
                            string changes2 = $"Изменил: Отчество: {p.Patronymic} => {(e.EditingElement as TextBox).Text}";
                            p.Change(u, changes2);
                            break;
                    }
                }
            }
            else if (e.Column.Header.ToString() == "Номер телефона")
            {
                if (PhoneValidate(e, p))
                {
                    string changes = $"Изменил: Номер телефона: {p.PhoneNumber} => {(e.EditingElement as TextBox).Text}";
                    p.Change(u, changes);
                }
            }
            else if (e.Column.Header.ToString() == "Серия, номер паспорта")
            {
                if (PassportValidate(e, p))
                {
                    string changes = $"Изменил: Серия, номер паспорта: {p.Passport} => {(e.EditingElement as TextBox).Text}";
                    p.Change(u, changes);
                }
            }
        }

        bool PhoneValidate(DataGridCellEditEndingEventArgs e, Person p)
        {
            if ((e.EditingElement as TextBox).Text.Length != 12 || (e.EditingElement as TextBox).Text.First() != '+')
            {
                e.Cancel = true;
                (e.EditingElement as TextBox).Text = p.PhoneNumber;
                MessageBox.Show("Неверный формат номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((e.EditingElement as TextBox).Text != p.PhoneNumber)
            {
                if (MessageBox.Show("Подтвердить изменения?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    (e.EditingElement as TextBox).Text = p.PhoneNumber;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else return false;
        }

        bool FullNameValidate(DataGridCellEditEndingEventArgs e, Person p)
        {
            string baseName = "";
            switch (e.Column.Header.ToString())
            {
                case "Фамилия":
                    baseName = p.LastName;
                    break;
                case "Имя":
                    baseName = p.FirstName;
                    break;
                case "Отчество":
                    baseName = p.Patronymic;
                    break;
            }


            if ((e.EditingElement as TextBox).Text.Length < 1)
            {
                e.Cancel = true;
                (e.EditingElement as TextBox).Text = baseName;
                MessageBox.Show("Это поле не может быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((e.EditingElement as TextBox).Text != baseName)
            {
                if (MessageBox.Show("Подтвердить изменения?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    (e.EditingElement as TextBox).Text = baseName;
                    return false;
                }
                else return true;
            }
            else return false;
        }

        bool PassportValidate(DataGridCellEditEndingEventArgs e, Person p)
        {
            if ((e.EditingElement as TextBox).Text.Length != 11)
            {
                e.Cancel = true;
                (e.EditingElement as TextBox).Text = p.Passport;
                MessageBox.Show("Неверный формат серии и номера паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if ((e.EditingElement as TextBox).Text != p.Passport)
            {
                if (MessageBox.Show("Подтвердить изменения?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    (e.EditingElement as TextBox).Text = p.Passport;
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else return false;
        }
    }
}
