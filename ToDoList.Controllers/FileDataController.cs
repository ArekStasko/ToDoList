using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers
{
    public class FileDataController
    {

        protected int GetUserSelection()
        {
            string? selectedOption = Console.ReadLine();
            int optionNumber;

            while (!Int32.TryParse(selectedOption, out optionNumber))
            {
                Console.WriteLine("You have to choose the number of option");
                selectedOption = Console.ReadLine();
            }
            Console.Clear();
            return optionNumber;
        }

        protected int GetNumericValue(string message)
        {
            Console.WriteLine(message);
            string? NumVal = Console.ReadLine();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                Console.WriteLine("Input must be non 0 number");
                NumVal = Console.ReadLine();
            }
            return Int32.Parse(NumVal);
        }

        protected Activity GetActivityByID(string msg)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            int activityID = GetNumericValue(msg);

            try
            {
                var activity = activities.Single(activity => activity.ActivityID == activityID);
                Console.Clear();
                return activity;
            }
            catch (Exception)
            {
                throw new Exception($"You don't have item with {activityID} ID");
            }

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