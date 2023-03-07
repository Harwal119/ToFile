using Bank_To_File.models;

namespace BankSectorApplication.models
{
    public class Manager: BaseEntity
    {
        
        public int UserId{ get;set; }
        public string StaffRegNo{ get;set; }
        public string Role{ get;set; }

        public Manager(int id, bool isDeleted, int userId,string staffRegNo,string role): base(id,isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
            UserId = userId;
            StaffRegNo = staffRegNo;
            Role = role;
        }

        public override string ToString()
        {
            return $"{Id};{IsDeleted};{UserId};{StaffRegNo};{Role}";
        }

        public static Manager ToManager(string manager)
        {
            var managerStr = manager.Split(';');
            int id = int.Parse(managerStr[0]);
            bool isDeleted = bool.Parse(managerStr[1]); 
            int userId = int.Parse(managerStr[2]);
            string staffRegNo = managerStr[3];
            string role = managerStr[4];
            return new Manager(id, isDeleted, userId, staffRegNo, role);
        }

    }
}