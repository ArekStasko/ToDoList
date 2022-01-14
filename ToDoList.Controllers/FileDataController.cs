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

        protected int GetUserSelection(int numberOfOptions)
        {
            int selectedOption = GetNumericValue();
            var showProvider = new ShowProvider();

            while (selectedOption > numberOfOptions)
            {
                showProvider.DisplayMessage($"There is no {selectedOption} option");
                selectedOption = GetNumericValue();
            }

            showProvider.ClearView();
            return selectedOption;
        }

        protected int GetNumericValue()
        {
            var showProvider = new ShowProvider();

            string? NumVal = showProvider.GetData();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                showProvider.DisplayMessage("Input must be non 0 number");
                NumVal = showProvider.GetData();
            }
            return Int32.Parse(NumVal);
        }

        protected string GetStringValue(string msg)
        {
            var showProvider = new ShowProvider();
            showProvider.DisplayMessage(msg);
            string? providedData = showProvider.GetData();

            while (String.IsNullOrEmpty(providedData))
            {
                showProvider.DisplayMessage("-You can't add empty data-");
                providedData = showProvider.GetData();
            }

            return providedData;
        }

        protected Activity GetActivityByID()
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Provide activity ID");
            int activityID = GetNumericValue();

            try
            {
                var activity = activities.Single(activity => activity.ActivityID == activityID);
                showProvider.ClearView();
                return activity;
            }
            catch (Exception)
            {
                showProvider.ErrorMessage($"There is no activity with {activityID} ID");
                throw new Exception();
            }

        }

        protected Activity GetActivityByID(int ID)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            var activity = activities.Single(activity => activity.ActivityID == ID);
            var showProvider = new ShowProvider();
            showProvider.ClearView();
            return activity;
        }


        protected IEnumerable<Activity> GetActivitiesByCategory(string category)
        {
            var dataProvider = new FileDataProvider();
            var showProvider = new ShowProvider();

            IEnumerable<string> categories = dataProvider.GetCategories();
            IEnumerable<Activity> activities = dataProvider.GetActivities();
            try
            {
                showProvider.ClearView();
                return activities.Where(activity => activity.ActivityCategory == category);
            }
            catch (Exception)
            {
                showProvider.ErrorMessage($"You don't have {category} category");
                throw new Exception();
            }
        }

        protected IEnumerable<Activity> GetActivitiesByCategory()
        {
            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Provide activities category to find");
            string? category;
            showProvider.ClearView();

            do
            {
                category = showProvider.GetData();
            }
            while (string.IsNullOrEmpty(category));

            return GetActivitiesByCategory(category);

        }

    }
}