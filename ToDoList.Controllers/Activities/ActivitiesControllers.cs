﻿using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ActivitiesControllers : FileDataController, IActivitiesControllers
    {
        private readonly IView view;
        public ActivitiesControllers(IView _view) : base(_view) => view = _view;

        public void SetActivityAsDone()
        {
            var dataProvider = new FileDataProvider();
            var activity = GetActivityByID();

            var currentDate = DateTime.Now;
            if (activity.StartDate.CompareTo(currentDate) >= 0)
            {
                view.ErrorMessage("You don't even start this activity");
                return;
            }

            activity.IsDone = true;
            dataProvider.UpdateActivity(activity);
        }

        public void AddNewActivity()
        {
            var newActivity = new Activity();

            int activityID = GetNumericValue();

            var dataProvider = new FileDataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity._id == activityID))
            {
                view.ErrorMessage("You already have activity with this ID");
                activityID = GetNumericValue();
            }
            
            newActivity._id = activityID;

            newActivity.Title = GetStringValue();
            newActivity.Description = GetStringValue();

            var categoriesControllers = new CategoriesControllers(view);
            string category = categoriesControllers.GetCategory();

            newActivity.Category = category;

            var startDate = GetDate();
            var endDate = GetDate();

            while (endDate <= startDate)
            {
                view.ErrorMessage("Deadline can't be earlier than start date");
                endDate = GetDate();
            }

            newActivity.StartDate = startDate;
            newActivity.EndDate = endDate;

            dataProvider.AddActivity(newActivity);
            view.DisplayMessage("Successfully added new Activity");
        }

        public void EditActivity(int editOption, Activity activityToEdit)
        {
            switch (editOption)
            {
                case 1:
                    {
                        _view.DisplayMessage("Provide new activity name");
                        activityToEdit.Title = GetStringValue();
                        break;
                    }
                case 2:
                    {
                        var categoriesControllers = new CategoriesControllers(view);
                        activityToEdit.Category = categoriesControllers.GetCategory();
                        break;
                    }
                case 3:
                    {
                        _view.DisplayMessage("Provide new activity description");
                        activityToEdit.Description = GetStringValue();
                        break;
                    }
                case 4:
                    {
                        view.DisplayMessage("Provide new activity start date");
                        activityToEdit.StartDate = GetDate();
                        break;
                    }
                case 5:
                    {
                        view.DisplayMessage("Provide new activity deadline date");
                        activityToEdit.EndDate = GetDate();
                        break;
                    }
            }

            var dataProvider = new FileDataProvider();
            dataProvider.UpdateActivity(activityToEdit);

        }

        public void DeleteActivity()
        {
            var dataProvider = new FileDataProvider();

            var activityToDelete = GetActivityByID();
            dataProvider.RemoveActivity(activityToDelete);

            view.DisplayMessage("Successfully deleted activity");
        }

        private DateTime GetDate()
        {
            string[] dateQuery = new string[] { "Year", "Month", "Day", "Hour", "Minute" };

            DateTime date;
            do
            {
                var dataDate = new List<int> { };

                foreach (var data in dateQuery)
                {
                    view.DisplayMessage($"Provide {data}");
                    int numericValue = GetNumericValue();
                    dataDate.Add(numericValue);
                }

                date = new DateTime(
                    dataDate[0],
                    dataDate[1],
                    dataDate[2],
                    dataDate[3],
                    dataDate[4],
                    0
                );

            } while (date.CompareTo(DateTime.Now) >= 0);

            return date;
        }
    }
}
