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

            return categories.ElementAt(categoryNumber - 1);
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

        private DateTime GetUserDate(string msg)
        {
            Console.WriteLine(msg);
            string[] dateQuery = new string[] { "Month", "Day", "Year", "Hour", "Minute" };
            var dataDate = new List<int> { };

            foreach (var data in dateQuery)
            {
                int numericValue;
                do
                {
                    numericValue = GetNumericValue($"Provide {data}");
                }
                while (numericValue <= 0);
                dataDate.Add(numericValue);
            }

            try
            {
                DateTime date = new DateTime(
                    dataDate[0],
                    dataDate[1],
                    dataDate[2],
                    dataDate[3],
                    dataDate[4],
                    0
                    );

                var currentDate = DateTime.Now;

                if (date.CompareTo(currentDate) >= 0)
                    return date;
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("You provided wrong date");
            }

        }

        private void AddNewActivity()
        {
            string[] activityQuery = new string[] { "Activity Name", "Activity Description" };
            List<string> activityData = new List<string>(2) { "", "" };

            int activityID = GetNumericValue("Provide new activity ID");

            var dataProvider = new FileDataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity.ActivityID == activityID))
            {
                Console.WriteLine("You already have activity with this ID");
                activityID = GetNumericValue("Provide new activity unique ID");
            }

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

            var startDate = GetUserDate("Provide start date of activity");
            var deadlineDate = GetUserDate("Provide deadline date of activity");

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

            switch (selectedOption)
            {
                case 1:
                    {
                        AddNewCategory();
                        break;
                    }
                case 2:
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
                        break;
                    }
                case 3:
                    {
                        if (!activities.Any())
                        {
                            Console.WriteLine("You don't have any activities");
                        }
                        else
                        {
                            DeleteActivity();
                        }
                        break;
                    }
                case 4:
                    {
                        if (!categories.Any())
                        {
                            Console.WriteLine("You don't have any category");
                        }
                        else
                        {
                            DeleteCategory();
                        }

                        break;
                    }
                default:
                    {
                        Console.WriteLine("You provide wrong option number");
                        break;
                    }
            }

        }


    }
}
