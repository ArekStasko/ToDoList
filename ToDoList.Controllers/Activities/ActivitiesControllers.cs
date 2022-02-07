using ToDoList.Controllers.Factories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ActivitiesControllers : IActivitiesControllers
    {
        protected readonly IView _view;
        private readonly IDataProvider _provider;
        public ActivitiesControllers(IView view)
        {
            _view = view;
            _provider = Factory.NewDataProviderInstance();
        }

        public void SetActivityAsActive(int activityID)
        {
            if (CheckIfActivitiesAreEmpty()) return;
            IActivity activity = GetActivityByID(activityID);

            if (activity.IsDone)
            {
                _view.ErrorMessage("You already done this activity");
                return;
            }

            activity.IsActive = true;
            _provider.UpdateActivity(activity);
        }

        public void SetActivityAsDone(int activityID)
        {
            if (CheckIfActivitiesAreEmpty()) return;
            IActivity activity = GetActivityByID(activityID);

            if (!activity.IsActive)
            {
                _view.ErrorMessage("You didn't start this activity");
                return;
            }

            activity.IsDone = true;
            _provider.UpdateActivity(activity);
        }

        public void AddNewActivity(IActivity activity)
        {
            var activities = _provider.GetActivities();

            if (activities.Any(act => act._id == activity._id))
            {
                _view.ErrorMessage("You already have activity with this ID");
                return;
            }

            try
            {
                _provider.AddActivity(activity);
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
                _provider.UpdateActivity(activity);
            }
            catch (Exception)
            {
                _view.ErrorMessage("You provided wrong data");
                return;
            }
        }

        public void DeleteActivity(int activityID)
        {
            IActivity activityToDelete = GetActivityByID(activityID);
            _provider.RemoveActivity(activityToDelete);

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

        public IEnumerable<IActivity> GetInactiveActivities() => _provider.GetInactiveActivities();

        public IEnumerable<IActivity> GetActiveActivities() => _provider.GetActiveActivities();

        public IActivity GetActivityByID(int ID) => _provider.GetActivityByID(ID);

        public IEnumerable<IActivity> GetActivitiesByCategory(string category) => _provider.GetActivitiesByCategory(category);

        public IEnumerable<IActivity> GetActivities() => _provider.GetActivities();

        private bool CheckIfActivitiesAreEmpty() => !_provider.GetActivities().Any();  
    }
}
