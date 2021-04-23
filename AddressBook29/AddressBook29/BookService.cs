using AddressBook27.Model;
using AddressBook27.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AddressBook27
{
    class BookService
    {
        ContactRepo repo = new ContactRepo();
        ContactModel contactModel = new ContactModel();

        public void addDetails()
        {
            ContactModel contactModel = new ContactModel();
            contactModel.bookName = "myBook3";
            contactModel.firstName = "Aman";
            contactModel.lastName = "swaraj";
            contactModel.address = "5th main road";
            contactModel.city = "cpr";
            contactModel.state = "Bhr";
            contactModel.zip = "841424";
            contactModel.phoneNumber = "9876543210";
            contactModel.email = "aman@gmail.com";
            if (!repo.GetBookByName(contactModel.bookName))
            {
                repo.AddBook(contactModel.bookName);
            }

            ContactModel contactModel2 = new ContactModel();
            contactModel2.bookName = "myBook4";
            contactModel.firstName = "Rashi";
            contactModel2.lastName = "Ritu";
            contactModel2.address = "Boaring road";
            contactModel2.city = "Sitamadhi";
            contactModel2.state = "Bhr";
            contactModel2.zip = "453656";
            contactModel2.phoneNumber = "7890654321";
            contactModel2.email = "rashi@gmail.com";

            if (!repo.GetBookByName(contactModel2.bookName))
            {
                repo.AddBook(contactModel2.bookName);
            }
            Thread t1 = new Thread(() => repo.AddContact(contactModel));
            Thread t2 = new Thread(() => repo.AddContact(contactModel2));
            t1.Start();
            t2.Start();
           
        }


    }
}