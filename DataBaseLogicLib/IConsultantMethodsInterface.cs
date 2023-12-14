using System.Windows.Controls;

namespace Bank__v1
{
    internal interface IConsultantMethodsInterface
    {
        void Validate(DataGridCellEditEndingEventArgs e, Person p, User u);

    }
}
