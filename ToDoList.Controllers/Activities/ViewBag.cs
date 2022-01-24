using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ViewBag
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; } = "None Title";
        public string Description { get; set; } = "None Description";
        public string Category { get; set; } = "None Category";
        public bool _isActive { get => EndDate > DateTime.Now; }

        public ViewBag() { }
        internal ViewBag(Activity activity)
        {
            Id = activity._id;
            EndDate = activity.EndDate;
            Title = activity.Title;
            Description = activity.Description; 
            Category = activity.Category;
        }
    }
}
