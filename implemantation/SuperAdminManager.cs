using System.Collections.Generic;
using BankSectorApplication.enums;
using BankSectorApplication.interfaces;
using BankSectorApplication.models;

namespace BankSectorApplication.implemantation
{
    public class SuperAdminManager : ISuperAdminManager
    {
        public static List<SuperAdmin> listOfSuperAdmin = new List<SuperAdmin>();
       
        IUserManager userManager = new UserManager();
        string file = @"C:\Users\USER\Desktop\BankToFile\Files\superAdmin.txt";
         public void ReadSuperAdminFromFile()
        {
            try
            {
                if (File.Exists(file))
                {
                    var superAdmin = File.ReadAllLines(file);
                    foreach (var item in superAdmin)
                    {
                        listOfSuperAdmin.Add(SuperAdmin.ToSuperAdmin(item));
                    }
                }
                else
                {
                    string path = @"C:\Users\USER\Desktop\BankToFile\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "superAdmin.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {
                        
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void WriteManagerToFile(SuperAdmin superAdmin)
        {
            try
            {
                using (var write = new StreamWriter(file, true))
                {
                    write.WriteLine(superAdmin.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

         }

            private void RefreshFile()
        {
            try
            {
                    using(StreamWriter str = new StreamWriter(file))
                    {
                        foreach (var superAdmin in listOfSuperAdmin)
                        {
                        str.WriteLine(superAdmin.ToString());
                        }
                    }
                    
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

         public SuperAdmin Get(string email)
        {
            foreach (var superAdmin in listOfSuperAdmin)
            {
                var user = userManager.GetUserByEmail(email);
                if(user.Id == superAdmin.UserId)
                {
                    return superAdmin;
                }
            }
            return null;
        }
  }
}
