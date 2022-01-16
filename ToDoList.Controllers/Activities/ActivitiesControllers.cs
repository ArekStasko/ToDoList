﻿using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ActivitiesControllers : FileDataController, IActivitiesControllers
    {
        private readonly IView view;
        public ActivitiesControllers(IView _view) : base(_view)
        {
            view = _view;
        }

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
            string[] activityQuery = new string[] { "Activity Name", "Activity Description" };
            List<string> activityData = new List<string>(2) { "", "" };

            view.DisplayMessage("Provide new activity ID");
            int activityID = GetNumericValue();

            var dataProvider = new FileDataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity.ActivityID == activityID))
            {
                view.ErrorMessage("You already have activity with this ID");
                activityID = GetNumericValue();
            }

            for (int i = 0; i < activityQuery.Length; i++)
            {
                view.DisplayMessage($"Provide {activityQuery[i]}");
                activityData[i] = view.GetData() ?? "None";
            }

            var categoriesControllers = new CategoriesControllers(view);
            string category = categoriesControllers.GetCategory();

            view.DisplayMessage("Provide start date of activity");
            var startDate = GetDate();
            view.DisplayMessage("Provide deadline date of activity");
            var deadlineDate = GetDate();

            while (deadlineDate <= startDate)
            {
                view.DisplayMessage("Deadline can't be earlier than start date");
                deadlineDate = GetDate();
            }

            var newActivity = new Activity()
            {
                ActivityName = activityData[0],
                ActivityDescription = activityData[1],
                ActivityID = activityID,
                ActivityCategory = category,
                StartDate = startDate,
                DeadlineDate = deadlineDate,
            };



            dataProvider.AddActivity(newActivity);
            view.DisplayMessage("Successfully added new Activity");
        }

        public void EditActivity(int editOption, Activity activityToEdit)
        {
            switch (editOption)
            {
                case 1:
                    {
                        activityToEdit.ActivityName = GetStringValue("Provide new activity name");
                        break;
                    }
                case 2:
                    {
                        var categoriesControllers = new CategoriesControllers(view);
                        activityToEdit.ActivityCategory = categoriesControllers.GetCategory();
                        break;
                    }
                case 3:
                    {
                        activityToEdit.ActivityDescription = GetStringValue("Provide new activity description");
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
                        activityToEdit.DeadlineDate = GetDate();
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
