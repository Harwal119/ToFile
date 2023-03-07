using BankSectorApplication.interfaces;
using BankSectorApplication.models;

namespace BankSectorApplication.implemantation
{
    public class CustomerTransactionManager: ICustomerTransactionManager
    {
       
         public static List<CustomerTransaction> listOfCustomerTransaction =  new List<CustomerTransaction>();
        IWalletManager walletManager = new WalletManager();
        string file = @"C:\Users\USER\Desktop\BankToFile\Files\customerTransaction.txt";
        
        public void ReadCustomerTransactionFromFile()
        {
            try
            {
                 if (File.Exists(file))
                {
                    if(listOfCustomerTransaction.Count == 0)
                    {
                        var customerTransactions = File.ReadAllLines(file);
                        foreach (var customerTransaction in customerTransactions)
                        {
                        listOfCustomerTransaction.Add(CustomerTransaction.ToCustomerTransaction(customerTransaction));
                        }
                    }
                    else
                    {
                        listOfCustomerTransaction.RemoveRange(0,listOfCustomerTransaction.Count);
                        var customerTransactions = File.ReadAllLines(file);
                        foreach (var customerTransaction in customerTransactions)
                        {
                        listOfCustomerTransaction.Add(CustomerTransaction.ToCustomerTransaction(customerTransaction));
                        }
                    }
                   
                }
                else
                {
                    string path = @"C:\Users\USER\Desktop\BankToFile\Files";
                    Directory.CreateDirectory(path);
                    string fileName = "customerTransaction.txt";
                    string fullPath = Path.Combine(path, fileName);
                    using (File.Create(fullPath))
                    {
                        
                    }
                }
            }
            catch (Exception cT)
            {
                Console.WriteLine(cT.Message);
            }
        }
        private void WriteCustomerTransactionToFile(CustomerTransaction customerTransaction)
        {
            try
            {
                using (var write = new StreamWriter(file, true))
                {
                    write.WriteLine(customerTransaction.ToString());
                }
            }
            catch (Exception cT)
            {
                Console.WriteLine(cT.Message);
            }
        }
        public List<CustomerTransaction> CraeteWithdraw(int userId, decimal amount)
        {
            ReadCustomerTransactionFromFile();
            var getUserAccount = walletManager.GetWalletByUserId(userId);
            if(getUserAccount.AccountBalance < amount)
            {
                Console.WriteLine("Insufient balance!");
                return listOfCustomerTransaction;
            }
            else
            {
            var customer = new CustomerTransaction(listOfCustomerTransaction.Count+1, false, userId,amount,DateTime.Now,enums.TransactionType.Withdraw,"");
            listOfCustomerTransaction.Add(customer);
            List<Wallet> newList = walletManager.DebitWallet(amount,getUserAccount.AccountNumber);
            walletManager.RefreshFile(newList);
            Console.WriteLine($"Debit Alert! {customer.Amount} has been remove from from your account");  
            return listOfCustomerTransaction;
            }
       
        }
        
        public List<CustomerTransaction> CreateTransfer(int userId, decimal amount, string thirdPartyAccount)
        {
            ReadCustomerTransactionFromFile();
            var getUserAccount = walletManager.GetWalletByUserId(userId);
            if(getUserAccount.AccountBalance < amount)
            {
                Console.WriteLine("Insufient balance!");
                return listOfCustomerTransaction;
            }
            else
            {
                var getThirdPartyAccountNumber = walletManager.GetWalletAccountNumber(thirdPartyAccount);
                decimal thirdPartyAccountBalance = getThirdPartyAccountNumber.AccountBalance;
                if(getThirdPartyAccountNumber == null)
                {
                    Console.WriteLine("invalid acc number");
                    return listOfCustomerTransaction;
                }
                else
                {
                    var customer = new CustomerTransaction(listOfCustomerTransaction.Count+1, false,userId,amount,DateTime.Now,enums.TransactionType.Transfer,thirdPartyAccount);
                    WriteCustomerTransactionToFile(customer);
                    listOfCustomerTransaction.Add(customer);
                    List<Wallet> newList = walletManager.DebitWallet(amount,getUserAccount.AccountNumber);
                    walletManager.RefreshFile(newList);
                    List<Wallet> newList1 = walletManager.FundWallet(amount,thirdPartyAccount);
                    walletManager.RefreshFile(newList1);
                    WriteCustomerTransactionToFile(customer);
                    ReadCustomerTransactionFromFile();
                    Console.WriteLine($"Debit Alert!{customer.Amount} has been transferd to {customer.ThirdPartyAccount}");
                    return listOfCustomerTransaction;
                }

            }
           
            
        }


        public  void RefreshFile(List<CustomerTransaction> newcustomerTransaction)
        {
            try
            {
                    using(StreamWriter str = new StreamWriter(file))
                    {
                        foreach (var customerTransaction in listOfCustomerTransaction)
                        {
                        str.WriteLine(customerTransaction.ToString());
                        }
                    }
                   
            }
            catch (System.Exception ex)
            {
                
                Console.WriteLine(ex.Message);
            }
        }

        public void GetCustomer(int userId)
        {
           foreach (var customer in listOfCustomerTransaction)
           {
                if(customer.UserId == userId)
                {
                    WriteCustomerTransactionToFile(customer);
                    // listOfCustomerTransaction.Add(customer);
                  Console.WriteLine($"{customer.Id}Transactin Date:{customer.Date}Type:{customer.TransactionType}");
                }
           }
        
        }
    }
}