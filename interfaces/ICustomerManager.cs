using System;
using BankSectorApplication.models;

namespace BankSectorApplication.interfaces
{
    public interface ICustomerManager
    {
        public void RegisterCustomer();
        public Customer GetCustomerByEmail(string email);
        public void GetCustomerProfileByEmail(string email);
        public Customer GetCustomer(int id);
        public Customer Get(string email);
        public Customer GetCustomerByUserId(int userId);
        public void GetAllCustomer(); 
        public Customer DeleteCustomer(int userId); 
        public Customer UpdateCustomerByEmail(string email); 
        public void ReadCustomerFromFile();

    }
}