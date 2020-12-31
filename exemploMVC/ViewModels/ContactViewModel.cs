using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exemploMVC.ViewModels
{
    public class ContactViewModel 
    {
        [Display(Name = "Contact ID")]
        public string ContactID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Contact Name")]
        public string Name { get; set; }
        
        [EmailAddress]
        [StringLength(255)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(100)]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(100)]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        public ContactViewModel()
        {

        }
        public ContactViewModel(string id, string name, string email, string city, string state, string cpf)
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