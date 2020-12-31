using exemploMVC.Models;
using exemploMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace exemploMVC.Controllers
{
    public class ContactController : Controller
    {
        
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetContact(string id)
        {
            List<Contact> contacts = LoadContacts();
            Contact contact = contacts.FirstOrDefault( c => c.ContactID == id);

            ContactViewModel contactViewModel = new ContactViewModel
            {
                ContactID = contact.ContactID,
                Name = contact.Name,
                Email = contact.Email,
                City = contact.City,
                State = contact.State,
                CPF = contact.CPF
            };

            return View(contactViewModel);
        }

        [HttpGet]
        public ActionResult CreateContact()
        {
            ContactViewModel contactView = new ContactViewModel();
            return View(contactView);
        }

        [HttpPost]
        public ActionResult CreateContact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact(contactViewModel.Name, contactViewModel.Email, contactViewModel.City, contactViewModel.State, contactViewModel.CPF);
                

                List<Contact> contacts = LoadContacts();
               
                contacts.Add(contact);
                SaveContac(contact);

                return RedirectToAction("LoadData");
            }

            return View(contactViewModel);
        }

        [HttpGet]
        public ActionResult UpdateContact(string id)
        {
            List<Contact> contacts = LoadContacts();
            Contact contact = contacts.FirstOrDefault(c => c.ContactID == id);

            ContactViewModel contactViewModel = new ContactViewModel
            {
                ContactID = contact.ContactID,
                Name = contact.Name,
                Email = contact.Email,
                City = contact.City,
                State = contact.State,
                CPF = contact.CPF
            };

            return View(contactViewModel);
        }

        [HttpPost]
        public ActionResult UpdateContact(ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                var contact = new Contact(contactViewModel.ContactID, contactViewModel.Name, contactViewModel.Email, contactViewModel.City, contactViewModel.State, contactViewModel.CPF);
                
                if (UpdateDataBase(contact))
                { 
                    return RedirectToAction("LoadData");
                }
            }

            return View(contactViewModel);
        }

        [HttpGet]
        public ActionResult DeleteContact(string id)
        {
            ContactViewModel contactViewModel = new ContactViewModel
            {
                ContactID = id
            };

            return View(contactViewModel);
        }

        [HttpPost]
        public ActionResult DeleteContact(ContactViewModel contactViewModel)
        {
            if (!DeleteContactInDB(contactViewModel.ContactID))
                throw new Exception("It was impossible to delete");

            return RedirectToAction("LoadData");
        }

        public bool DeleteContactInDB(string contactId)
        {
            bool deleted = false;
            List<Contact> contacts = LoadContacts();

            var contact = contacts.FirstOrDefault(c => c.ContactID == contactId);

            if(contact != null)
            {
                contacts.Remove(contact);
                deleted = PrintData(contacts);
            }

            return deleted;
        }

        public bool UpdateDataBase(Contact contact)
        {
            List<Contact> contacts = LoadContacts();

            var indexOf = contacts.IndexOf(contacts.Find(c => c.ContactID == contact.ContactID));
            contacts[indexOf] = contact;

            return PrintData(contacts);
        }


        public bool PrintData(List<Contact> _contacts)
        {
            bool printed = false;
            string path = @"D:\Usuario\Documents\ExemploMVCAspDotNet\data.txt";
            using (StreamWriter sw = System.IO.File.CreateText(path))
            {
                foreach (var _contact in _contacts)
                {
                    sw.WriteLine(_contact.ContactID + "," + _contact.Name + "," + _contact.Email + "," + _contact.City + "," + _contact.State + "," + _contact.CPF);
                }
                printed = true;
                sw.Close();
            }
            return printed;
        }

        public void SaveContac(Contact _contact)
        {
            string path = @"D:\Usuario\Documents\ExemploMVCAspDotNet\data.txt";
            using (StreamWriter sappend = System.IO.File.AppendText(path))
            {
                sappend.WriteLine(_contact.ContactID + "," + _contact.Name + "," + _contact.Email + "," + _contact.City + "," + _contact.State + "," + _contact.CPF);
                sappend.Close();
            }
        }


        public List<Contact> LoadContacts()
        {
            string path = @"D:\Usuario\Documents\ExemploMVCAspDotNet\data.txt";
            string[] lines = System.IO.File.ReadAllLines(path);

            var contacts = new List<Contact>();

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(',');
                var id = line[0];
                var name = line[1];
                var email = line[2];
                var city = line[3];
                var state = line[4];
                var cpf = line[5];

                Contact contact = new Contact(id, name, email, city, state, cpf);
                contacts.Add(contact);
            }

            return contacts;
        }

        public ActionResult LoadData()
        {
            List<Contact> contacts = LoadContacts();

            var contactViewModel = new ContactsViewModel
            {
                Contacts = contacts
            };

            return View(contactViewModel);
        }
    }
}