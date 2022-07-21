using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Practice_11
{
    internal class Consultant
    {
        protected DataBase dataBase;
        protected string inMemory;
        protected List<string> allowedToChangeColumns;

        public EventHandler<DataGridBeginningEditEventArgs> DataGridBeginningEditEventHandler { get; private set; }

        public Consultant()
        {
            dataBase = new DataBase();
            dataBase.Show();
            SetAllowedToChangeColumns();
            SetDataBase();
            SetBaseName();
        }

        private void SetBaseName()
        {
            dataBase.Title = "A-Bank : Consultant";
        }

        public virtual void SetAllowedToChangeColumns()
        {
            allowedToChangeColumns = new List<string> { "Номер телефона" };
        }

        private void ClientsDataGrid_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            if ((e.EditingElement as TextBox).Text.Count() < 12)
            {
                e.Cancel = true;
                (e.EditingElement as TextBox).Text = inMemory;
            }
        }

        void SetDataBase()
        {
            foreach (DataGridColumn col in dataBase.ClientsDataGrid.Columns)
            {
                foreach (string header in allowedToChangeColumns)
                {
                    if (col.Header.ToString() == header)
                    {
                        col.IsReadOnly = false;
                        break;
                    }
                    else
                        col.IsReadOnly = true;
                }
            }
            SetDataBaseTriggers();
        }

        public virtual void SetDataBaseTriggers()
        {
            dataBase.ClientsDataGrid.CellEditEnding += ClientsDataGrid_CellEditEnding;
            dataBase.ClientsDataGrid.BeginningEdit += ClientsDataGrid_BeginningEdit;
        }


        private void ClientsDataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            inMemory = (dataBase.ClientsDataGrid.CurrentCell.Item as Person).PhoneNumber;
        }
    }
}
