using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers
{
    public interface IView
    {
        public void PrintActivity(string activity);
        public void PrintActivities(IEnumerable<string> activities);
        public void PrintCategories(IEnumerable<string> categories);
        public void DisplayMessage(string msg);
        public void ErrorMessage(string msg);
        public void ClearView();
        public string? GetData();
    }
}
