﻿using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public interface IActivitiesControllers
    {
        public void StartActivity();
        public void SetActivityAsDone();
        public void AddNewActivity(IActivity activity);
        public void EditActivity(IActivity activity);
        public void DeleteActivity();
        public DateTime GetDate();
        public IEnumerable<IActivity> GetInactiveActivities();
        public IEnumerable<IActivity> GetActiveActivities();
        public IActivity GetActivityByID(int ID);
        public IEnumerable<IActivity> GetActivitiesByCategory(string category);
        public IEnumerable<IActivity> GetActivities();
    }
}