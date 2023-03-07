using Bank_To_File.models;
using BankSectorApplication.enums;

namespace BankSectorApplication.models
{
    public class User : BaseEntity
    {
        public string Name { get;set; }
        public string Email { get;set; }
        public int Pin { get;set; }
        public Gender Gender { get;set; }
        public string Address { get;set; }
        public string PhoneNumber { get;set; }


        public User(int id, bool isDeleted, string name, string email, int pin, Gender gender, string address, string phoneNumber) : base(id, isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
            Name = name;
            Email = email;
            Pin = pin;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
        }
        public override string ToString()
        {
            return $"{Id}\t{IsDeleted}\t{Name}\t{Email}\t{Pin}\t{Gender}\t{Address}\t{PhoneNumber}";
        }

        public static User ToUser(string user)
        {
            var userStr = user.Split('\t');
            int id = int.Parse(userStr[0]);
            bool isDeleted = bool.Parse(userStr[1]);
            string name = userStr[2];
            string email = userStr[3];
            int pin = int.Parse(userStr[4]);
            Gender gender = (Gender) Enum.Parse(typeof(Gender),userStr[5]);
            string address = userStr[6];
            string phoneNumber = userStr[7];
            return new User(id, isDeleted, name, email, pin, gender, address, phoneNumber);
        }

     }
 }
