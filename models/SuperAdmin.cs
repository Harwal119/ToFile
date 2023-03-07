using Bank_To_File.models;

namespace BankSectorApplication.models
{
    public class SuperAdmin: BaseEntity
    {
        public int UserId{ get;set; }
        public string StaffRegNo{ get;set; }
        public string Role{ get;set; }

        public SuperAdmin(int id, bool isDeleted, int userId,string staffRegNo,string role): base(id,isDeleted)
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

        public static SuperAdmin ToSuperAdmin(string superAdmin)
        {
            var superAdminStr = superAdmin.Split(';');
            int id = int.Parse(superAdminStr[0]);
            bool isDeleted = bool.Parse(superAdminStr[1]);
            int userId = int.Parse(superAdminStr[2]);
            string staffRegNo = superAdminStr[3];
            string role = superAdminStr[4];
            return new SuperAdmin(id, isDeleted, userId, staffRegNo, role);
        }
    }
}