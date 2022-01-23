using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ViewBag
    {
        public int Id { get; }
        public DateTime EndDate { get; }
        public string Title { get; }
        public string Description { get; }
        public string Category { get; }
        public bool _isActive { get => EndDate > DateTime.Now; }

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
