using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers
{
    public class FileDataController
    {
        private readonly IView _view;

        public FileDataController(IView view)
        {
            _view = view;
        }

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

        protected string GetStringValue(string msg)
        {
            _view.DisplayMessage(msg);
            string? providedData = _view.GetData();

            while (String.IsNullOrEmpty(providedData))
            {
                _view.DisplayMessage("-You can't add empty data-");
                providedData = _view.GetData();
            }

            return providedData;
        }

        public Activity GetActivityByID()
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            _view.DisplayMessage("Provide activity ID");
            int activityID = GetNumericValue();

            try
            {
                var activity = activities.Single(activity => activity.ActivityID == activityID);
                _view.ClearView();
                return activity;
            }
            catch (Exception)
            {
                _view.ErrorMessage($"There is no activity with {activityID} ID");
                throw new Exception();
            }

        }

        public Activity GetActivityByID(int ID)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            var activity = activities.Single(activity => activity.ActivityID == ID);

            _view.ClearView();
            return activity;
        }


        protected IEnumerable<Activity> GetActivitiesByCategory(string category)
        {
            var dataProvider = new FileDataProvider();

            IEnumerable<string> categories = dataProvider.GetCategories();
            IEnumerable<Activity> activities = dataProvider.GetActivities();
            try
            {
                _view.ClearView();
                return activities.Where(activity => activity.ActivityCategory == category);
            }
            catch (Exception)
            {
                _view.ErrorMessage($"You don't have {category} category");
                throw new Exception();
            }
        }

        public IEnumerable<Activity> GetActivitiesByCategory()
        {
            _view.DisplayMessage("Provide activities category to find");
            string? category;
            _view.ClearView();

            do
            {
                category = _view.GetData();
            }
            while (string.IsNullOrEmpty(category));

            return GetActivitiesByCategory(category);

        }

    }
}