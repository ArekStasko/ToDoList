using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ActivitiesControllers : IActivitiesControllers
    {
        protected readonly IView _view;
        private readonly IDataProvider _dataProvider;
        public ActivitiesControllers(IView view)
        {
            _view = view;
            _dataProvider = new DataProvider();
        }

        public void StartActivity()
        {
            if (checkIfActivitiesExist()) return;

            int activityID = _view.GetID();
            IActivity activity = GetActivityByID(activityID);

            if (activity.IsDone)
            {
                _view.ErrorMessage("You already done this activity");
                return;
            }

            activity.IsActive = true;
            _dataProvider.UpdateActivity(activity);
        }

        public void SetActivityAsDone()
        {
            if (checkIfActivitiesExist()) return;

            int activityID = _view.GetID();
            IActivity activity = GetActivityByID(activityID);

            if (!activity.IsActive)
            {
                _view.ErrorMessage("You didn't start this activity");
                return;
            }

            activity.IsDone = true;
            _dataProvider.UpdateActivity(activity);
        }

        public void AddNewActivity(IActivity activity)
        {
            var activities = _dataProvider.GetActivities();

            if (activities.Any(act => act._id == activity._id))
            {
                _view.ErrorMessage("You already have activity with this ID");
                return;
            }

            try
            {
                _dataProvider.AddActivity(activity);
            }
            catch (Exception)
            {
                _view.ErrorMessage("You provided wrong data");
                return;
            }

        }

        public void EditActivity(IActivity activity)
        {
            try
            {
                _dataProvider.UpdateActivity(activity);
            }
            catch (Exception)
            {
                _view.ErrorMessage("You provided wrong data");
                return;
            }
        }

        public void DeleteActivity()
        {
            int activityID = _view.GetID();
            IActivity activityToDelete = GetActivityByID(activityID);
            _dataProvider.RemoveActivity(activityToDelete);

            _view.DisplayMessage("Successfully deleted activity");
        }

        public DateTime GetDate()
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

            } while (date.CompareTo(DateTime.Now) <= 0);

            return date;
        }

        public IEnumerable<IActivity> GetInactiveActivities()
        {
            return _dataProvider.GetInactiveActivities();
        }

        public IEnumerable<IActivity> GetActiveActivities()
        {
            return _dataProvider.GetActiveActivities();
        }

        public IActivity GetActivityByID(int ID)
        {
            return _dataProvider.GetActivityByID(ID);
        }

        public IEnumerable<IActivity> GetActivitiesByCategory(string category)
        {
            return _dataProvider.GetActivitiesByCategory(category);
        }

        public IEnumerable<IActivity> GetActivities()
        {
            return _dataProvider.GetActivities();
        }

        private bool checkIfActivitiesExist()
        {
            return _dataProvider.GetActivities().Any();  
        }
    }
}
