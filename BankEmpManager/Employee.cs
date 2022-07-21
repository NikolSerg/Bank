using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEmpManager
{
    public class Employee
    {
        public static List<Employee> Employees;

        static Employee()
        {
            Employees = new List<Employee>();
        }

        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Post { get; set; }

        public Employee(string phoneNumber, string password, string post)
        {
            PhoneNumber = phoneNumber;
            Password = password;
            Post = post;
            Employees.Add(this);
        }
    }
}
