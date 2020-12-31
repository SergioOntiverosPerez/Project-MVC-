using exemploMVC.Models;
using exemploMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace exemploMVC.Controllers.Api
{
    
    public class ContactsController : ApiController
    {

        //Get /api/contacts
        public IEnumerable<Contact> GetContacts()
        {
            return LoadContacts();
        }

        // GET /api/contact/1
        public IHttpActionResult GetContact(string id)
        {
            List<Contact> contacts = LoadContacts();

            return Ok(contacts.FirstOrDefault(c => c.ContactID == id));
        }

        // POST /api/contact
        [HttpPost]
        public IHttpActionResult PostContact(Contact _contact)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            List<Contact> contacts = LoadContacts();
            Contact contact = new Contact(_contact.Name, _contact.Email, _contact.City, _contact.State, _contact.CPF);

            contacts.Add(contact);

            SaveContact(contact);

            return Created(new Uri(Request.RequestUri + "/" + contact.ContactID), contact);
        }


        // PUT /api/contact/1
        [HttpPut]
        public IHttpActionResult UpdateContact(string id, Contact contact)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var contacts = LoadContacts();

            var contactInDb = contacts.FirstOrDefault(c => c.ContactID == id);

            if (contactInDb == null)
                return NotFound();

            contactInDb.ContactID = contact.ContactID;
            contactInDb.Name = contact.Name;
            contactInDb.Email = contact.Email;
            contactInDb.City = contact.City;
            contactInDb.State = contact.State;
            contactInDb.CPF = contact.CPF;

            if (!UpdateDataBase(contactInDb))
                throw new Exception("Impossivel to save");

            return Ok();
        }


        // Delete /api/contacts/1
        [HttpDelete]
        public IHttpActionResult DeleteContact(string Id)
        {
            var contacts = LoadContacts();

            var contactInDb = contacts.SingleOrDefault(c => c.ContactID == Id);

            if (contactInDb == null)
                return NotFound();

            if (!DeleteContactInDB(contactInDb.ContactID))
                throw new Exception("It was impossible to delete");

            return Ok();
        }


        private bool DeleteContactInDB(string contactId)
        {
            bool deleted = false;
            List<Contact> contacts = LoadContacts();

            var contact = contacts.FirstOrDefault(c => c.ContactID == contactId);

            if (contact != null)
            {
                contacts.Remove(contact);
                deleted = PrintData(contacts);
            }

            return deleted;
        }

        private bool UpdateDataBase(Contact contact)
        {
            List<Contact> contacts = LoadContacts();

            var indexOf = contacts.IndexOf(contacts.Find(c => c.ContactID == contact.ContactID));
            contacts[indexOf] = contact;

            return PrintData(contacts);
        }


        private bool PrintData(List<Contact> _contacts)
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

        private void SaveContact(Contact _contact)
        {
            string path = @"D:\Usuario\Documents\ExemploMVCAspDotNet\data.txt";
            using (StreamWriter sappend = System.IO.File.AppendText(path))
            {
                sappend.WriteLine(_contact.ContactID + "," + _contact.Name + "," + _contact.Email + "," + _contact.City + "," + _contact.State + "," + _contact.CPF);
                sappend.Close();
            }
        }


        private List<Contact> LoadContacts()
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
    }
}
