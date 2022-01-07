using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers
{
    public class FileDataController
    {

        protected int GetUserSelection(int numberOfOptions)
        {
            int selectedOption = GetNumericValue();

            while (selectedOption > numberOfOptions)
            {
                Console.WriteLine($"There is no {selectedOption} option");
                selectedOption = GetNumericValue();
            }

            Console.Clear();
            return selectedOption;
        }

        protected int GetNumericValue()
        {
            string? NumVal = Console.ReadLine();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                Console.WriteLine("Input must be non 0 number");
                NumVal = Console.ReadLine();
            }
            return Int32.Parse(NumVal);
        }

        protected string GetStringValue(string msg)
        {
            Console.WriteLine(msg);
            string? providedData = Console.ReadLine();

            while (String.IsNullOrEmpty(providedData))
            {
                Console.WriteLine("-You can't add empty data-");
                providedData = Console.ReadLine();
            }

            return providedData;
        }

        protected Activity GetActivityByID(string msg)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            Console.WriteLine(msg);
            int activityID = GetNumericValue();

            try
            {
                var activity = activities.Single(activity => activity.ActivityID == activityID);
                Console.Clear();
                return activity;
            }
            catch (Exception)
            {
                throw new Exception($"You don't have activity with {activityID} ID");
            }

        }

        protected Activity GetActivityByID(int ID)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            var activity = activities.Single(activity => activity.ActivityID == ID);
            Console.Clear();
            return activity;
        }


        protected IEnumerable<Activity> GetActivitiesByCategory(string category)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<string> categories = dataProvider.GetCategories();
            IEnumerable<Activity> activities = dataProvider.GetActivities();
            try
            {
                Console.Clear();
                return activities.Where(activity => activity.ActivityCategory == category);
            }
            catch (Exception)
            {
                throw new Exception($"You don't have {category} category");
            }
        }

        protected IEnumerable<Activity> GetActivitiesByCategory()
        {
            Console.WriteLine("Provide activities category to find");
            string? category;
            Console.Clear();

            do
            {
                category = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(category));

            return GetActivitiesByCategory(category);

        }

    }
}