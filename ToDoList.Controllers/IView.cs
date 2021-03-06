using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers
{
    public interface IView
    {
        public void PrintCategories(IEnumerable<string> categories);
        public void DisplayMessage(string msg);
        public void ErrorMessage(string msg);
        public void ClearView();
        public string? GetData();
        public int GetNumericValue();
        public string GetStringValue();
        public int GetID();
    }
}
