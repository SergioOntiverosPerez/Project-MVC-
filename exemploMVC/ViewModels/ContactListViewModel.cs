using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace exemploMVC.ViewModels
{
    public class ContactListViewModel
    {
        [Display(Name = "Contact ID")]
        public int ContactID { get; set; }

        [Display(Name = "Contact Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}