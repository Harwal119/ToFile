using System;
using System.Collections.Generic;
using BankSectorApplication.interfaces;
using BankSectorApplication.models;

namespace BankSectorApplication.implemantation
{
    public class WalletManager : IWalletManager
    {
        public static List<Wallet> listOfWallet = new List<Wallet>();
        string file = @"C:\Users\USER\Desktop\BankToFile\Files\wallet.txt";
       
        public void ReadWalletFromFile()
        {
            try
            {
                if(File.Exists(file))
                {
                    if (listOfWallet.Count ==0 )
                    {
                        var wallets = File.ReadAllLines(file);
                        foreach (var wallet in wallets)
                        {
                            listOfWallet.Add(Wallet.ToWallet(wallet));
                        }
                    }
                    else
                    {
                        listOfWallet.RemoveRange(0,listOfWallet.Count);
                        var wallets = File.ReadAllLines(file);
                        foreach (var wallet in wallets)
                        {
                            listOfWallet.Add(Wallet.ToWallet(wallet));
                        }
                    }
                     
                }
                else
                {
                    string path = @"C:\Users\USER\Desktop\BankToFile\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "wallet.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {
                        
                    }
                }
            }
            catch(Exception w)
            {
                Console.WriteLine(w.Message);
            }
        }

        private void WriteWalletToFile(Wallet wallet)
        {
            try
            {
                  using (var write = new StreamWriter(file, true))
                {
                    write.WriteLine(wallet.ToString());
                }
            }
            catch(Exception w)
            {
                Console.WriteLine(w.Message);
            }
        }
        public Wallet CreateWallet(int userId,string walletPassword)
        {
            var wallet = new Wallet(listOfWallet.Count+1, false, userId,0,GenerateAccountNumber(),walletPassword);
            WriteWalletToFile(wallet);
            ReadWalletFromFile();
            return wallet;
        }
         
        public void RefreshFile(List<Wallet> newWalletList)
        {
            try
            { 
                using(StreamWriter str = new StreamWriter(file))
                {
                    foreach (var wallet in newWalletList)
                    {
                        str.WriteLine(wallet.ToString());
                    }
                }
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

        public List<Wallet> FundWallet(decimal amount,string accountNumber)
        {
            ReadWalletFromFile();
            foreach (var item in listOfWallet)
            {
                if(item.AccountNumber == accountNumber)
                {
                    
                    if(amount <= 0)
                    {
                        Console.WriteLine("Amount cant b found");
                    }
                    else
                    {
                        item.AccountBalance+=amount;
                        Console.WriteLine($"your balance is now {item.AccountBalance}"); 
                        return listOfWallet;  
                    }
                         
                }
                    
                
            }
            
                return listOfWallet;
            
        }

        public Wallet GetWalletAccountNumber(string accountNumber)
        {
            foreach (var item in listOfWallet)
            {
                if(item.AccountNumber == accountNumber)
                {
                    return item;
                }
            }
            return null;
        }

        public void GetWalletByCustomerAccountNumberAndPin(string accountNumber,string pin)
        {
             foreach (var item in listOfWallet)
            {
                if(item.AccountNumber == accountNumber && item.WalletPassword == pin && item.IsDeleted == false)
                {
                    Console.WriteLine($"your wallet balance is {item.AccountBalance}");
                }
            }
        }

        public Wallet GetWalletByUserId(int UserId)
        {
            foreach (var item in listOfWallet)
            {
                if(item.UserId == UserId)
                {
                    return item;
                }
            }
            return null;
        }
        private string GenerateAccountNumber()
        {
             Random rand = new Random();
            return rand.Next(100000000,999999999).ToString();

        }
        public List<Wallet> DebitWallet(decimal ammountToDeduct, string accountNumber)
        {
            ReadWalletFromFile();
            foreach (var item in listOfWallet)
            {
                if(item.AccountNumber == accountNumber)
                {
                    item.AccountBalance -= ammountToDeduct;
                    Console.WriteLine($"your balance is now {item.AccountBalance}"); 
                    return listOfWallet;       
                }
                    
                
            }
            
                return listOfWallet;
        }
    }
}