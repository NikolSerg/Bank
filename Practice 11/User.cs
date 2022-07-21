using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_11
{
    internal class User
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Post { get; set; }
        public User(string mobilePhone, string password, string post)
        {
            PhoneNumber = mobilePhone;
            Password = password;
            Post = post;
        }
    }
}
