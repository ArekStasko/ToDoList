
namespace ToDoList.Controllers.Activities
{
    internal interface IActivitiesControllers
    {
        public void AddNewActivity();
        public void DeleteActivity();
        public void EditActivity(int editOption);
        public void SetActivityAsDone();
    }
}
