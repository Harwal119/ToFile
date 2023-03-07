using BankSectorApplication.models;

namespace BankSectorApplication.interfaces
{
    public interface ICustomerTransactionManager
    {
         public List<CustomerTransaction> CreateTransfer(int userId,decimal amount,string thirdPartyAccount);
         public List<CustomerTransaction> CraeteWithdraw(int userId, decimal amount);
         public void GetCustomer(int userId);
         public void ReadCustomerTransactionFromFile();
         public  void RefreshFile(List<CustomerTransaction> newcustomerTransaction);
         
    }
}