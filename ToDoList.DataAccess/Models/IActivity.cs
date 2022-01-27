
namespace ToDoList.DataAccess.Models
{
    public interface IActivity
    {
        public int _id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDone { get; set; }
        public string GetTimeToDeadline();
        public string[] ConvertToDataRow();
    }
}
