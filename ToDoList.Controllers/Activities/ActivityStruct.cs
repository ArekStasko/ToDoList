using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public struct ActivityStruct
    {
        public int Id { get; set; }    
        public string Title { get; set; } = "none Title";
        public string Category { get; set; } = "none Category";
        public string Description { get; set; } = "none Description";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool _isActive { get => EndDate > DateTime.Now; }
        public bool _isDone { get; set; }

        public ActivityStruct(Activity act)
        {
            Id = act._id;
            Title = act.Title;
            Category = act.Category;
            Description = act.Description;
            StartDate = act.StartDate;
            EndDate = act.EndDate;
            _isDone = act.IsDone;
        }
    }
}
