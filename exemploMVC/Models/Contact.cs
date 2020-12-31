using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exemploMVC.Models
{
    public class Contact : User
    {
        
        public string ContactID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CPF { get; set; }

        public Contact()
        {
            ContactID = Guid.NewGuid().ToString();
        }

        public Contact(string name) : this()
        {
            Name = name;
        }

        public Contact(string name, string email) : this(name)
        {
            Email = email;
        }

        public Contact(string name, string email, string city) : this(name, email)
        {
            City = city;
        }

        public Contact(string name, string email, string city, string state) : this(name, email, city)
        {
            State = state;
        }

        public Contact(string name, string email, string city, string state, string cpf) : this(name, email, city, state)
        {
            CPF = cpf;
        }

        public Contact(string id, string name, string email, string city, string state, string cpf)
        {
            ContactID = id;
            Name = name;
            Email = email;
            City = city;
            State = state;
            CPF = cpf;
        }
    }
}