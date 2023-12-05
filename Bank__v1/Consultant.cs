using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Bank__v1
{
    internal class Consultant : IConsultantMethodsInterface
    {
        DataBase window;
        public Consultant(DataBase window)
        {
            this.window = window;
            SetAllowedToChangeColumns();
            window.addButton.IsEnabled = false;
        }

        public virtual void Validate(DataGridCellEditEndingEventArgs e, Person p, User u)
        {
            if ((e.EditingElement as TextBox).Text.Length < 12 || (e.EditingElement as TextBox).Text.First() != '+')
            {
                e.Cancel = true;
                (e.EditingElement as TextBox).Text = p.PhoneNumber;
                MessageBox.Show("Неверный формат номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if ((e.EditingElement as TextBox).Text != p.PhoneNumber)
            {
                if (MessageBox.Show("Подтвердить изменения?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                    (e.EditingElement as TextBox).Text = p.PhoneNumber;
                }
                else
                {
                    DateTime dateTime = DateTime.Now;
                    string changes = $"Изменил: {u.PhoneNumber}, {u.Post}\nНомер телефона: {p.PhoneNumber} => {(e.EditingElement as TextBox).Text}";
                    p.Changes.Add(dateTime, changes);
                }
            }
        }

        void SetAllowedToChangeColumns()
        {
            if (this is Manager)
            {
                int j = 4;
                for (int i = 0; i < window.ClientsDataGrid.Columns.Count; i++)
                {
                    if (window.ClientsDataGrid.Columns[i].Header.ToString() == "Серия, номер паспорта")
                        j = i;
                }
                window.ClientsDataGrid.Columns.RemoveAt(j);
                Binding bind = new Binding("Passport");
                bind.Mode = BindingMode.TwoWay;
                DataGridColumn col = new DataGridTextColumn { Binding = bind };
                col.Header = "Серия, номер паспорта";
                col.Width = window.ClientsDataGrid.Columns[0].Width;
                window.ClientsDataGrid.Columns.Add(col);
            }
            else
            {
                for (int i = 0; i < window.ClientsDataGrid.Columns.Count; i++)
                {
                    if (window.ClientsDataGrid.Columns[i].Header.ToString() != "Номер телефона")
                        window.ClientsDataGrid.Columns[i].IsReadOnly = true;
                }
            }
        }
    }
}
