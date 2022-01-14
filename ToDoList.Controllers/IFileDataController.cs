using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers
{
    internal interface IFileDataController
    {
        public void ChooseMainOption();
        public int GetUserSelection(int numberOfOptions);
        public Activity GetActivityByID(int ID);
        public Activity GetActivityByID();
    }
}
