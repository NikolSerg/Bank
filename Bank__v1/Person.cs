using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Bank__v1
{


    public class Person
    {
        public static ObservableCollection<Person> Clients;
        public static Dictionary<ulong, NotDepAccount> PersonsAccNumbersBase;
        static Person()
        {
            Clients = new ObservableCollection<Person>();
            PersonsAccNumbersBase = new Dictionary<ulong, NotDepAccount>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PhoneNumber { get; set; }
        public string Passport { get; set; }

        public Dictionary<DateTime, string> Changes = new Dictionary<DateTime, string>();

        public NotDepAccount[] Accounts;


        public Person(string firstName, string lastName, string patronymic, string phoneNumber, string passport)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            PhoneNumber = phoneNumber;
            Passport = passport;
            Accounts = new NotDepAccount[2];
            Clients.Add(this);
        }

        public bool AddAccount(NotDepAccount acc)
        {
            bool exist = false;
            foreach (Account account in Accounts)
            {
                if (account is null) continue;
                else if (account.GetType() == acc.GetType())
                {
                    exist = true;
                    break;
                }
            }
            if (!exist)
            {
                if (acc is DepAccount) Accounts[1] = acc;
                else Accounts[0] = acc;
                if (PersonsAccNumbersBase.Count > 0)
                {
                    ulong max = Person.PersonsAccNumbersBase.Keys.Max() + 1;
                    acc.AccNumber = max;
                }
                else acc.AccNumber = 1;
                Person.PersonsAccNumbersBase.Add(acc.AccNumber, acc);
            }
            return exist;
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
                else if (i == 4)
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
            Accounts = new NotDepAccount[2];
            Clients.Add(this);
        }
    }
}
