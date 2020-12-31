using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exemploMVC.Models
{
    public class ContactVirtual : User
    {
        public string VirtualId { get; set; }
        public string VirtualName { get; set; }
        public string VirtualEmail { get; set; }
        public string VirtualCity { get; set; }


        public ContactVirtual(string id, string name, string email, string city) 
        {
            VirtualId = id;
            VirtualName = name;
            VirtualEmail = email;
            VirtualCity = city;
        }

    }
}