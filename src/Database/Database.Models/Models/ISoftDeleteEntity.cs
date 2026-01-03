namespace Database.Models
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
    }
}
