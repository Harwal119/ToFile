using System;
using System.Collections.Generic;
using BankSectorApplication.interfaces;
using BankSectorApplication.models;

namespace BankSectorApplication.implemantation
{
    public class CustomerManager : ICustomerManager
    {
        IUserManager userManager = new UserManager();
        IWalletManager walletManager = new WalletManager();
        public static List<Customer> listOfCustomer = new List<Customer>();
        string file = @"C:\Users\USER\Desktop\BankToFile\Files\customer.txt";
        
        public void ReadCustomerFromFile()
        {
            try
            {
                   if (File.Exists(file))
                {
                    if (listOfCustomer.Count == 0)
                    {
                        var customers = File.ReadAllLines(file);
                        foreach (var customer in customers)
                        {
                            listOfCustomer.Add(Customer.ToCustomer(customer));
                        }
                    }
                    else
                    {
                        listOfCustomer.RemoveRange(0,listOfCustomer.Count);
                        var customers = File.ReadAllLines(file);
                        foreach (var customer in customers)
                        {
                            listOfCustomer.Add(Customer.ToCustomer(customer));
                        }
                    }
                    
                }
                else
                {
                    string path = @"C:\Users\USER\Desktop\BankToFile\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "customer.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {
                        
                    }
                }
            }
            catch(Exception c)
            {
                Console.WriteLine(c.Message);
            }
        }

        private void WriteCustomerToFile(Customer customer)
        {
            try
            {
                using (var write = new StreamWriter(file, true))
                {
                    write.WriteLine(customer.ToString());
                }
            }
            catch (Exception c)
            {
                Console.WriteLine(c.Message);
            }
        }



            public  void RefreshFile(List<Customer> newcustomer)
        {
            try
            {
                    using(StreamWriter str = new StreamWriter(file))
                    {
                        foreach (var customer in listOfCustomer)
                        {
                        str.WriteLine(customer.ToString());
                        }
                    }
                    
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }






    
        public Customer DeleteCustomer(int userId)
        {
            foreach (var customer in listOfCustomer)
            {
               if(customer.UserId == userId)
               {
                    listOfCustomer.Remove(customer);
               }
            }
            return null;
        }

        
        public void GetAllCustomer()
        {
            foreach (var customer in listOfCustomer)
            {
                var user = userManager.GetUser(customer.UserId);
                Console.WriteLine($"{user.Name}/t{user.Email}/t{user.PhoneNumber}");
            }
        }

        public Customer GetCustomer(int id)
        {
            
            foreach (var customer in listOfCustomer)
            {
                if ( customer.Id == id )
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer GetCustomerByUserId(int userId)
        {
            foreach (var customer in listOfCustomer)
            {
                if ( customer.UserId == userId )
                {
                    return customer;
                }
            }
            return null;
        }
  
      

        public void RegisterCustomer()
        {
            var customer = new Customer(listOfCustomer.Count + 1,false, UserManager.listOfUser.Count, "Customer");
            WriteCustomerToFile(customer);
            var user = userManager.GetUser(customer.UserId);
            var createWallet = walletManager.CreateWallet(user.Id,user.Pin.ToString());
            Console.WriteLine($"Congrats {user.Name}, your account Number is {createWallet.AccountNumber} and your wallet balance is {createWallet.AccountBalance}");
            ReadCustomerFromFile();
        }
        public Customer UpdateCustomerByEmail(string email)
        {
            foreach (var customer in listOfCustomer )
            {
                var user = userManager.GetUserByEmail(email);
                if( user.Email == email )
                {
                    return customer;
                }
            }
            return null;
        }

        public Customer Get(string email)
        {

            var user = userManager.GetUserByEmail(email);
            foreach (var customer in listOfCustomer)
            {
                if(user.Id == customer.UserId )
                {
                    
                    return customer;
                }
            }
            return null;
        }

        public void GetCustomerProfileByEmail(string email)
        {
            var user = userManager.GetUserByEmail(email);
            foreach (var customer in listOfCustomer)
            {
                if(user.Id == customer.UserId)
                {
                    var getwallet = walletManager.GetWalletByUserId(user.Id);
                    Console.WriteLine($" Id : {customer.Id}\t Name : {user.Name}\t Email: {user.Email}\t AccountNumber : {getwallet.AccountNumber}\tAccount Balance : {getwallet.AccountBalance} ");
                }
            }
            
        }
        public Customer GetCustomerByEmail(string email)
        {
            var user = userManager.GetUserByEmail(email);
            foreach (var customer in listOfCustomer)
            {
                if(user.Id == customer.UserId)
                {
                   return customer;
                }
            }
            return null;
        }
           
       
    }
}