//using System.ComponentModel.Design;
using BankSectorApplication.implemantation;
using BankSectorApplication.interfaces;
using BankSectorApplication.menu;

internal class Program
{
    private static void Main(string[] args)
    {
        UserManager  userManager = new UserManager();
        userManager.RaedUserFromFile();
        CustomerManager customerManager = new CustomerManager();
        customerManager.ReadCustomerFromFile();
        CustomerTransactionManager customerTransactionManager = new CustomerTransactionManager();
        customerTransactionManager.ReadCustomerTransactionFromFile();
        ManagerManager managerManager = new ManagerManager();
        managerManager.ReadManagerFromFile();
        SuperAdminManager superAdminManager = new SuperAdminManager();
        superAdminManager.ReadSuperAdminFromFile();
        WalletManager walletManager = new WalletManager();
        walletManager.ReadWalletFromFile();
        MainMenu menu = new MainMenu();
        menu.RealMenu();
    }
}