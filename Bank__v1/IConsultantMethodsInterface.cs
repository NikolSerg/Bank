using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Bank__v1
{
    internal interface IConsultantMethodsInterface
    {
        void Validate(DataGridCellEditEndingEventArgs e, Person p, User u);

    }
}
