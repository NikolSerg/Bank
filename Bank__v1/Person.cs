using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank__v1
{
    internal class Person
    {
        public static ObservableCollection<Person> Clients;
        static Person()
        {
            Clients = new ObservableCollection<Person>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Passport { get; set; }

        public Dictionary<DateTime, string> Changes = new Dictionary<DateTime, string>();



        public Person(string firstName, string lastName, string patronymic, string phoneNumber, string passport)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            Passport = passport;
            Clients.Add(this);
        }
        public Person()
        {
            Random random = new Random();
            char[] charset = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            string temp = null;
            for (int i = 0; i <= 5; i++)
            {
                if (i < 3)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        temp += charset[random.Next(charset.Length)];
                    }
                }
                else if (i == 3)
                {
                    temp = "+7";
                    for (int k = 0; k < 10; k++)
                        temp += random.Next(10).ToString();
                }
                else if (i==4)
                {
                    for (int k = 0; k <= 10; k++)
                    {
                        if (k < 3 || k > 4)
                            temp += random.Next(10).ToString();
                        else if (k == 3)
                            temp += random.Next(10) + " ";
                    }
                }
                
                switch (i)
                {
                    case 0: 
                        FirstName = temp;
                        temp = null;
                        break;
                    case 1:
                        LastName = temp;
                        temp = null;
                        break;
                    case 2:
                        Patronymic = temp;
                        temp = null;
                        break;
                    case 3:
                        PhoneNumber = temp;
                        temp = null;
                        break;
                    case 4:
                        Passport = temp;
                        temp = null;
                        break;
                }
            }
            Clients.Add(this);
        }
    }
}
