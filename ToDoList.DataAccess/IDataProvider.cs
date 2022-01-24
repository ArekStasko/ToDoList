using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess
{
    public interface IDataProvider
    {
        IEnumerable<Activity> GetActivities();
        IEnumerable<string> GetCategories();
        void AddCategory(string newCategory);
        void AddActivity(Activity newActivity);
        void AddActivities(List<Activity> newActivities);
        void RemoveActivity(Activity activityToRemove);
        public IEnumerable<Activity> GetInactiveActivities();
        public IEnumerable<Activity> GetActiveActivities();
        public Activity GetActivityByID(int _id);
        public IEnumerable<Activity> GetActivitiesByCategory(string category);
        public void UpdateActivity(Activity activityToUpdate);
    }
}