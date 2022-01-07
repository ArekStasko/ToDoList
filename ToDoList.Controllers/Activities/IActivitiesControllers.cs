using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    internal interface IActivitiesControllers
    {
        public void AddNewActivity();
        public void DeleteActivity();
        public void EditActivity(int editOption, Activity activityToEdit);
        public void SetActivityAsDone();
    }
}
