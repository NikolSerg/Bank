using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Practice_11
{
    internal class Manager : Consultant
    {
        public override void SetAllowedToChangeColumns()
        {
            allowedToChangeColumns = new List<string>();
            foreach (DataGridColumn col in dataBase.ClientsDataGrid.Columns)
            {
                if (col.Header.ToString() != "Серия, номер паспорт")
                    allowedToChangeColumns.Add(col.Header.ToString());
            }
        }

        public Manager() : base()
        {
            dataBase.Title = "A-Bank : Manager";
            dataBase.addButton.Visibility = Visibility.Visible;
            dataBase.removeButton.Visibility = Visibility.Visible;
        }

        //private void SetColumnTemplate()
        //{
        //    dataBase.ClientsDataGrid.Columns.RemoveAt(4);




        //    DataGridTemplateColumn col1 = new DataGridTemplateColumn();
        //    col1.Width = dataBase.ClientsDataGrid.Columns[0].Width;
        //    col1.Header = "Серия, номер паспорта";

        //    Binding bind = new Binding("Passport");
        //    bind.Mode = BindingMode.TwoWay;

        //    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
        //    factory.SetValue(TextBlock.TextProperty, "**** ******");
        //    DataTemplate dp = new DataTemplate();
        //    dp.VisualTree = factory;



        //    FrameworkElementFactory editingFactory = new FrameworkElementFactory(typeof(TextBox));
        //    editingFactory.SetBinding(TextBox.TextProperty, bind);
        //    DataTemplate dp1 = new DataTemplate();
        //    dp1.VisualTree = editingFactory;

        //    col1.CellTemplate = dp;
        //    col1.CellEditingTemplate = dp1;


        //    dataBase.ClientsDataGrid.Columns.Add(col1);

        //}

        public override void SetDataBaseTriggers()
        {
            dataBase.ClientsDataGrid.BeginningEdit += ClientsDataGrid_BeginningEdit;
            dataBase.ClientsDataGrid.CellEditEnding += ClientsDataGrid_CellEditEnding;
        }

        private void ClientsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            string header = e.Column.Header.ToString();
            if (header == "Имя" || header == "Фамилия" || header == "Отчество")
            {
                if (string.IsNullOrEmpty((e.EditingElement as TextBox).Text))
                {
                    e.Cancel = true;
                    (e.EditingElement as TextBox).Text = inMemory;
                }
            }
            else if (header == "Номер телефона")
            {
                if ((e.EditingElement as TextBox).Text.Length < 12)
                {
                    e.Cancel = true;
                    (e.EditingElement as TextBox).Text = inMemory;
                }
            }
            else if (header == "Серия, номер паспорта")
            {
                if (dataBase.currentBox != null)
                {
                    if (dataBase.currentBox.Text.Length < 11)
                    {
                        dataBase.currentBox.Text = inMemory;
                    }
                }
            }
        }

        private void ClientsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            string header  = e.Column.Header.ToString();
            switch (header)
            {
                case "Фамилия":
                    inMemory = (dataBase.ClientsDataGrid.CurrentCell.Item as Person).LastName;
                    break;
                case "Имя":
                    inMemory = (dataBase.ClientsDataGrid.CurrentCell.Item as Person).FirstName;
                    break;
                case "Отчество":
                    inMemory = (dataBase.ClientsDataGrid.CurrentCell.Item as Person).Patronymic;
                    break;
                case "Номер телефона":
                    inMemory = (dataBase.ClientsDataGrid.CurrentCell.Item as Person).PhoneNumber;
                    break;
                case "Серия, номер паспорта":
                    inMemory = (dataBase.ClientsDataGrid.CurrentCell.Item as Person).Passport;
                    
                    break;
            }
        }
    }
}
