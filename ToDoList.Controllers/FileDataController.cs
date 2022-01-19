using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Controllers.Activities;

namespace ToDoList.Controllers
{
    public class FileDataController
    {
        protected readonly IView _view;
        public FileDataController(IView view) => _view = view;


        public int GetUserSelection(int numberOfOptions)
        {
            int selectedOption = GetNumericValue();

            while (selectedOption > numberOfOptions)
            {
                _view.DisplayMessage($"There is no {selectedOption} option");
                selectedOption = GetNumericValue();
            }

            _view.ClearView();
            return selectedOption;
        }

        public IEnumerable<ActivityStruct> GetInactiveActivities()
        {
            var activities = GetActivities();
            activities = activities.Where(activity => !activity._isActive);
            foreach(var activity in activities)
            {
                yield return new ActivityStruct(activity);
            }
        }

        public IEnumerable<ActivityStruct> GetActiveActivities()
        {
            var activities = GetActivities();
            activities = activities.Where(activity => activity._isActive);
            foreach (var activity in activities)
            {
                yield return new ActivityStruct(activity);
            }
        }

        public ActivityStruct GetActivityStructByID()
        {
            var activity = GetActivityByID();
            return new ActivityStruct(activity);
        }

        public IEnumerable<Activity> GetActivities()
        {
            var dataProvider = new FileDataProvider();
            return dataProvider.GetActivities();    
        }

        public Activity GetActivityByID()
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();
            int activityID;

            do
            {
                _view.DisplayMessage("Provide activity ID");
                activityID = GetNumericValue();
            } while (!activities.Any(activity => activity._id == activityID));

            var activity = activities.Single(activity => activity._id == activityID);
            _view.ClearView();
            return activity;
        }

        public Activity GetActivityByID(int ID)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            var activity = activities.Single(activity => activity._id == ID);

            _view.ClearView();
            return activity;
        }

        public IEnumerable<Activity> GetActivitiesByCategory()
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();
            string category;

            do
            {
                category = GetStringValue();
                _view.ClearView();
            }
            while (activities.Any(activity => activity.Category == category));

            return GetActivitiesByCategory(category);

        }

        protected IEnumerable<Activity> GetActivitiesByCategory(string category)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            _view.ClearView();
            return activities.Where(activity => activity.Category == category);
        }

        protected int GetNumericValue()
        {
            string? NumVal = _view.GetData();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                _view.DisplayMessage("Input must be non 0 number");
                NumVal = _view.GetData();
            }
            return Int32.Parse(NumVal);
        }

        protected string GetStringValue()
        {
            string? providedData = _view.GetData();

            while (String.IsNullOrEmpty(providedData))
            {
                _view.DisplayMessage("-You can't add empty data-");
                providedData = _view.GetData();
            }

            return providedData;
        }
    }
}