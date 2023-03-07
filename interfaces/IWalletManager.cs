using BankSectorApplication.models;

namespace BankSectorApplication.interfaces
{
    public interface IWalletManager
    {
         public Wallet CreateWallet(int userId,string walletPassword);
         public List<Wallet> FundWallet(decimal amount,string walletPassword);
         public Wallet GetWalletByUserId(int userId);
         public Wallet GetWalletAccountNumber(string accountNumber);
         public void GetWalletByCustomerAccountNumberAndPin(string WalletPassword,string pin);
         public void ReadWalletFromFile();
         public void RefreshFile(List<Wallet> newWalletList);
        public List<Wallet> DebitWallet(decimal ammountToDeduct, string accountNumber);
    }
}