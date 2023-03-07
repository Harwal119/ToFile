using Bank_To_File.models;

namespace BankSectorApplication.models
{
    public class Wallet: BaseEntity
    {
        public int UserId{get;set;}
        public string WalletPassword{get;set;}
        public decimal AccountBalance{ get;set;}
        public string AccountNumber{ get;set; }

      public Wallet(int id, bool isDeleted, int userId, decimal accountBalance,string accountNumber,string walletPassword): base(id,isDeleted)
      {
        Id = id;
        IsDeleted = isDeleted;
        UserId = userId;
        AccountBalance = accountBalance;
        AccountNumber = accountNumber;
        WalletPassword = walletPassword;
      }

        public override string ToString()
        {
            return $"{Id}\t{IsDeleted}\t{UserId}\t{AccountBalance}\t{AccountNumber}\t{WalletPassword}";
        }

        public static Wallet ToWallet(string wallet)
        {
          var walletStr = wallet.Split('\t');
          int id = int.Parse(walletStr[0]);
          bool isDeleted = bool.Parse(walletStr[1]);
          int userId = int.Parse(walletStr[2]);
          decimal accountBalance = decimal.Parse(walletStr[3]);
          string accountNumber = walletStr[4];
          string walletPassword = walletStr[5];
          return new Wallet(id, isDeleted, userId, accountBalance, accountNumber, walletPassword);
        }
    }
}