using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;


namespace ToDoList.DataControllers
{
    public class FileEditionController : FileDataController
    {

        private string GetCategory()
        {
            Console.WriteLine("- Please choose number of your activity category -");
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();

            var showProvider = new ShowProvider();
            showProvider.PrintCategories(categories);
            string? category = Console.ReadLine();
            int categoryNumber;

            while (!Int32.TryParse(category, out categoryNumber) || string.IsNullOrEmpty(category))
            {
                Console.WriteLine("- Please provide number of category -");
                category = Console.ReadLine();
            };

            return categories.ElementAt(categoryNumber-1);
        }

        private void AddNewCategory()
        {
            Console.WriteLine("- Please insert new category name -");
            string? categoryName = Console.ReadLine();
            while (String.IsNullOrEmpty(categoryName))
            {
                Console.WriteLine("-Category can't be empty-");
                categoryName = Console.ReadLine();
            }

            var dataProvider = new FileDataProvider();
            dataProvider.AddCategory(categoryName);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully added new category");
        }

        private void AddNewActivity()
        {
            string[] activityQuery = new string[] { "Activity Name", "Activity Description" };
            List<string> activityData = new List<string>(2) { "", "" };

            for (int i = 0; i < activityQuery.Length; i++)
            {
                Console.WriteLine($"Insert {activityQuery[i]}");
                string? providedData = Console.ReadLine();

                while (String.IsNullOrEmpty(providedData))
                {
                    Console.WriteLine("-You can't add empty data-");
                    providedData = Console.ReadLine();
                }

                activityData[i] = providedData;
            }

            string category = GetCategory();
            int activityID = GetID("Provide new activity ID");

            var dataProvider = new FileDataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity=>activity.ActivityID == activityID))
            {
                Console.WriteLine("You already have activity with this ID");
                activityID = GetID("Provide new activity unique ID");
            }

            var newActivity = new Activity()
            {
                ActivityName = activityData[0],
                ActivityDescription = activityData[1],
                ActivityID = activityID,
                ActivityCategory = category
            };

            while (activities.Any(activity => activity.ActivityID == newActivity.ActivityID))
            {
                Console.WriteLine("- You already have activity with this ID -");
                int newActivityID = GetID("- Provide unique ID number -");
                newActivity.ActivityID = newActivityID;
            }

            dataProvider.AddActivity(newActivity);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully added new Activity");
        }

        private void DeleteActivity()
        {
            var dataProvider = new FileDataProvider();

            var activityToDelete = GetActivityByID("Provide ID of activity to delete");
            dataProvider.RemoveActivity(activityToDelete);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully deleted activity");
        }

        private void DeleteCategory()
        {

            Console.WriteLine("Please provide the category name to delete");
            Console.WriteLine("- This will delete all items with this category -");
            string? userInput;

            var dataProvider = new FileDataProvider();

            do
            {
                userInput = Console.ReadLine();
            }
            while (string.IsNullOrEmpty(userInput));

            IEnumerable<Activity> ActivitiesToDelete = GetActivitiesByCategory(userInput);
            if (ActivitiesToDelete.Any())
            {
                dataProvider.RemoveActivity(ActivitiesToDelete.ToList());
            }
            dataProvider.RemoveCategory(userInput);
            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully deleted category");
        }           
        

        public void GetSelectedEditionOption(int selectedOption)
        {
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();
            var activities = dataProvider.GetActivities();

            if (selectedOption == 1)
            {
                AddNewCategory();
            }
            else if (selectedOption == 2)
            {                
                if (!categories.Any())
                {
                    Console.WriteLine("Please first add at least one category");
                    AddNewCategory();
                }
                else
                {
                    AddNewActivity();
                }
            }
            else if (selectedOption == 3)
            {
                
                if (!activities.Any())
                {
                    Console.WriteLine("You don't have any activities");
                }
                else
                {
                    DeleteActivity();
                }
            }
            else if (selectedOption == 4)
            {
                if (!categories.Any())
                {
                    Console.WriteLine("You don't have any category");
                }
                else
                {
                    DeleteCategory();
                }
            }
            else
            {
                Console.WriteLine("You provide wrong option number");
            }
        }


    }
}
