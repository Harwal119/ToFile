using System;
using System.Collections.Generic;
using BankSectorApplication.interfaces;
using BankSectorApplication.models;

namespace BankSectorApplication.implemantation
{
    public class ManagerManager : IManagerManager
    {
        public static List<Manager> listOfManager = new List<Manager>();

        IUserManager userManager = new UserManager();
        IWalletManager walletManager = new WalletManager();
        string file = @"C:\Users\USER\Desktop\BankToFile\Files\manager.txt";
     

        public void ReadManagerFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var managers = File.ReadAllLines(file);
                    foreach (var manager in managers)
                    {
                        listOfManager.Add(Manager.ToManager(manager));
                    }
                }
                else
                {
                    string path = @"C:\Users\USER\Desktop\BankToFile\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "manager.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {
                        
                    }
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }
        }

        private void WriteManagerToFile(Manager manager)
        {
            try
            {
                 using (var write = new StreamWriter(file, true))
                {
                    write.WriteLine(manager.ToString());
                }
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }

        }
        public Manager DeleteManager(int userId)
        {
            foreach (var manager in listOfManager)
            {
                var user = userManager.GetUser(userId);
                if(manager.UserId == userId)
                {
                    listOfManager.Remove(manager);
                }
            }
            return null;
        }

            public  void RefreshFile(List<Manager> newmanager)
        {
            try
            {
                    using(StreamWriter str = new StreamWriter(file))
                    {
                        foreach (var manager in listOfManager)
                        {
                        str.WriteLine(manager.ToString());
                        }
                    }
                    
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }
        

        public Manager Get(string email)
        {
            var user = userManager.GetUserByEmail(email);
            foreach (var manager in listOfManager)
            {
                if(user.Id == manager.UserId)
                {
                    return manager;
                }
            }
            return null;
        }

        public void GetAllManager()
        {
           foreach (var manager in listOfManager)
           {
                if(manager.Role == "Manager")
                {
                    var user = userManager.GetUser(manager.UserId);
                    Console.WriteLine($"{user.Id}\t{user.Name}\t{user.Email}\t{user.PhoneNumber}");
       
                }
            }
        }

        public Manager GetManager(int id)
        {
            foreach (var manager in listOfManager)
            {
                if(manager.Id == id)
                {
                    return manager;
                }
            }
            return null;
        }

        public Manager GetManagerUserId(int userId)
        {
            foreach (var manager in listOfManager)
            {
                if(manager.UserId == userId)
                {
                    return manager;
                }
            }
            return null;
        }

       

        public void RegisterManager()
        {
           var manager = new Manager(listOfManager.Count + 1, false, UserManager.listOfUser.Count, GenerateManagerRegistrationNumber(), "Manager");
           WriteManagerToFile(manager);
            listOfManager.Add(manager);
            var user = userManager.GetUser(manager.UserId);
            var createWallet = walletManager.CreateWallet(user.Id,user.Email);
            

            Console.WriteLine($"Congrats mr/mrs {user.Name},your account Number is {createWallet.AccountNumber} your reg no is {manager.StaffRegNo}");
        }

          private string GenerateManagerRegistrationNumber()
        {
            return "CLH/CTM/00" + (listOfManager.Count + 1).ToString();
        }
    }
}