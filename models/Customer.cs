using Bank_To_File.models;

namespace BankSectorApplication.models
{
    public class Customer : BaseEntity
    {
        public int UserId{ get;set; }
        public string Role{ get;set; }
      


        public Customer(int id,bool isDeleted, int userId,string role): base(id,isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
            UserId = userId;
            Role = role;
        }

        public override string ToString()
        {
            return $"{Id}\t{IsDeleted}\t{UserId}\t{Role}";
        }

        public static Customer ToCustomer(string customer)
        {
            var customerStr = customer.Split('\t');
            int id = int.Parse(customerStr[0]);
            bool isDeleted = bool.Parse(customerStr[1]);
            int userId = int.Parse(customerStr[2]);
            string role = customerStr[3];
            return new Customer(id, isDeleted, userId, role);
        }
        
    }
}