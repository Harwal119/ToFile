using System;
using System.Collections.Generic;
using BankSectorApplication.enums;
using BankSectorApplication.interfaces;
using BankSectorApplication.models;

namespace BankSectorApplication.implemantation
{
    public class UserManager : IUserManager
    {
       
        public static List<User> listOfUser = new List<User>();
        string file = @"C:\Users\USER\Desktop\BankToFile\Files\user.txt";

        public void RaedUserFromFile()
        {
            try
            {
                
                   if (File.Exists(file))
                {
                    if (listOfUser.Count == 0)
                    {
                        var users = File.ReadAllLines(file);
                        foreach (var user in users)
                        {
                            listOfUser.Add(User.ToUser(user));
                        }
                    }
                    else
                    {
                        listOfUser.RemoveRange(0,listOfUser.Count);
                        var users = File.ReadAllLines(file);
                    foreach (var user in users)
                    {
                        listOfUser.Add(User.ToUser(user));
                    }
                    }
                }
                else
                {
                    string path = @"C:\Users\USER\Desktop\BankToFile\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "user.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {
                        
                    }
                }
            }
            catch (Exception u)
            {
                Console.WriteLine(u.Message);
            }
        }

        private void WriteUserToFile(User user)
        {
            try
            {
                using (var write = new StreamWriter(file, true))
                {
                    write.WriteLine(user.ToString());
                }
            }
            catch (Exception u)
            {
                Console.WriteLine(u.Message);
            }
        }


        public User DeleteUser(int id)
        {

            foreach (var user in listOfUser)
            {
                if (user.Id == id && user.IsDeleted ==false)
                {
                    listOfUser.Remove(user);
                }
            }
            return null;
        }


            public  void RefreshFile()
        {
            try
            {
                    using(StreamWriter str = new StreamWriter(file))
                    {
                        foreach (var user in listOfUser)
                     {
                        str.WriteLine(user.ToString());
                     }
                    }
                    

            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }



        public void GetAllUser()
        {
            foreach (var user in listOfUser)
            {
                Console.WriteLine($"{user.Name}/t{user.Address}/t{user.Email}/t{user.Gender}/t{user.PhoneNumber}/t{user.Pin}");
            }

        }

        public User GetUser(int id)
        {
            foreach (var user in listOfUser)
            {
                if (user.Id == id && user.IsDeleted == false)
                {
                    return user;
                }
            }
            Console.WriteLine("null");
            return null;
        }

        public User GetUserByEmail(string email)
        {
            foreach (var user in listOfUser)
            {
                if (user.Email == email && user.IsDeleted == false)
                {
                    return user;
                }
            }
            return null;
        }

        public User Login(string email, int pin)
        {
            foreach (var user in listOfUser)
            {
                if (user.Email == email && user.Pin == pin && user.IsDeleted == false)
                {
                    return user;
                }
            }
            return null;
        }

        public User RegisterUser(string name, string email, int pin, Gender gender, string address, string phoneNumber)
        {
            var userExist = CheckIfExist(email);
            if (userExist != null)
            {
                Console.WriteLine("User already exist!");
                return null;
            }
            else
            {
                var user = new User(listOfUser.Count + 1, false, name, email, pin, gender, address, phoneNumber);
                WriteUserToFile(user);
                RaedUserFromFile();
                return user;
            }
        }


        private User CheckIfExist(string email)
        {
            foreach (var user in listOfUser)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }


        public User UpdateUser(string email)
        {
            foreach (var user in listOfUser)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }
            return null;
        }

        public User GetUserByPin(int pin)
        {
            foreach (var user in listOfUser)
            {
                if (user.Pin == pin)
                {
                    return user;
                }
            }
            return null;
        }
    }
}