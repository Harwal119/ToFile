namespace Bank_To_File.models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public BaseEntity(int id, bool isDeleted)
        {
            Id = id;
            IsDeleted = isDeleted;
        }
    }


}