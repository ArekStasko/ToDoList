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
    }
}