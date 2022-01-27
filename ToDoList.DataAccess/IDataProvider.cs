using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess
{
    public interface IDataProvider
    {
        public IEnumerable<IActivity> GetActivities();
        public IEnumerable<IActivity> GetActiveActivities();
        public IEnumerable<IActivity> GetInactiveActivities();
        public IActivity GetActivityByID(int _id);
        public IEnumerable<IActivity> GetActivitiesByCategory(string category);
        public IEnumerable<string> GetCategories();
        public void AddCategory(string newCategory);
        public void RemoveCategory(string categoryToRemove);
        public void AddActivity(IActivity newActivity);
        public void AddActivities(List<IActivity> newActivities);
        public void RemoveActivity(IActivity activityToRemove);
        public void RemoveActivity(List<IActivity> newActivities);
        public void UpdateActivity(IActivity activityToUpdate);
 
    }
}
