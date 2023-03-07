using Bank_To_File.models;
using BankSectorApplication.enums;

namespace BankSectorApplication.models
{
    public class CustomerTransaction: BaseEntity
    {
        public int UserId{ get;set;}
        public decimal Amount{ get;set;}
        public DateTime Date{ get;set;}
        public TransactionType TransactionType{ get;set;}
        public string ThirdPartyAccount{ get;set;}

        public CustomerTransaction(int id, bool isDeleted, int userId,decimal amount,DateTime date,TransactionType transactionType,string thirdPartyAccount): base(id,isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
            UserId = userId;
            Amount = amount;
            Date = date;
            TransactionType = transactionType;
            ThirdPartyAccount = thirdPartyAccount;
        }

        public override string ToString()
        {
            return $"{Id}\t{IsDeleted}\t{UserId}\t{Amount}\t{Date}\t{TransactionType}\t{ThirdPartyAccount}";
        }

        public static CustomerTransaction ToCustomerTransaction(string customerTransaction)
        {
            var customerTransactionStr = customerTransaction.Split('\t');
            int id = int .Parse(customerTransactionStr[0]);
            bool isDeleted = bool .Parse(customerTransactionStr[1]); 
            int userId = int.Parse(customerTransactionStr[2]);
            decimal amount = decimal.Parse(customerTransactionStr[3]);
            DateTime date = DateTime.Parse(customerTransactionStr[4]);
            TransactionType transactionType = (TransactionType) Enum.Parse(typeof(TransactionType),customerTransactionStr[5]);
            string thirdPartyAccount = customerTransactionStr[6];
            return new CustomerTransaction(id, isDeleted, userId, amount, date, transactionType, thirdPartyAccount);
        }
    }
}