using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ActivitiesControllers : DataController
    {
        public ActivitiesControllers(IView _view) : base(_view) { }

        public void SetActivityAsDone()
        {
            var dataProvider = new DataProvider();

            int activityID = _view.GetID();
            var activity = GetActivityByID(activityID);

            var currentDate = DateTime.Now;
            if (activity.StartDate.CompareTo(currentDate) >= 0)
            {
                _view.ErrorMessage("You don't even start this activity");
                return;
            }

            activity.IsDone = true;
            dataProvider.UpdateActivity(activity);
        }

        public void AddNewActivity()
        {
            var newActivity = new Activity();

            int activityID = _view.GetNumericValue();

            var dataProvider = new DataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity._id == activityID))
            {
                _view.ErrorMessage("You already have activity with this ID");
                activityID = _view.GetNumericValue();
            }
            
            newActivity._id = activityID;

            newActivity.Title = _view.GetStringValue();
            newActivity.Description = _view.GetStringValue();

            var categoriesControllers = new CategoriesControllers(_view);
            string category = categoriesControllers.GetCategory();

            newActivity.Category = category;

            var startDate = GetDate();
            var endDate = GetDate();

            while (endDate <= startDate)
            {
                _view.ErrorMessage("Deadline can't be earlier than start date");
                endDate = GetDate();
            }

            newActivity.StartDate = startDate;
            newActivity.EndDate = endDate;

            dataProvider.AddActivity(newActivity);
            _view.DisplayMessage("Successfully added new Activity");
        }

        public void EditActivity(int editOption, Activity activityToEdit)
        {
            switch (editOption)
            {
                case 1:
                    {
                        _view.DisplayMessage("Provide new activity name");
                        activityToEdit.Title = _view.GetStringValue();
                        break;
                    }
                case 2:
                    {
                        var categoriesControllers = new CategoriesControllers(_view);
                        activityToEdit.Category = categoriesControllers.GetCategory();
                        break;
                    }
                case 3:
                    {
                        _view.DisplayMessage("Provide new activity description");
                        activityToEdit.Description = _view.GetStringValue();
                        break;
                    }
                case 4:
                    {
                        _view.DisplayMessage("Provide new activity start date");
                        activityToEdit.StartDate = GetDate();
                        break;
                    }
                case 5:
                    {
                        _view.DisplayMessage("Provide new activity deadline date");
                        activityToEdit.EndDate = GetDate();
                        break;
                    }
            }

            var dataProvider = new DataProvider();
            dataProvider.UpdateActivity(activityToEdit);

        }

        public void DeleteActivity()
        {
            var dataProvider = new DataProvider();

            int activityID = _view.GetID();
            var activityToDelete = GetActivityByID(activityID);
            dataProvider.RemoveActivity(activityToDelete);

            _view.DisplayMessage("Successfully deleted activity");
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
                    _view.DisplayMessage($"Provide {data}");
                    int numericValue = _view.GetNumericValue();
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
