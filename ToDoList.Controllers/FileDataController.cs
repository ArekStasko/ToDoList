using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;
using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;

namespace ToDoList.Controllers
{
    public class FileDataController : IFileDataController
    {

        public void ChooseMainOption()
        {
            int userSelection;

            do
            {
                var optionsProvider = new Options();
                optionsProvider.PrintMainOptions();

                userSelection = GetUserSelection();

                switch (userSelection)
                {
                    case 1:
                        {
                            var dataProvider = new FileDataProvider();
                            IEnumerable<Activity> activities = dataProvider.GetActivities();

                            var showProvider = new ShowProvider();
                            showProvider.PrintActivities(activities);
                            break;
                        }
                    case 2:
                        {
                            optionsProvider.PrintActivitySearchOptions();
                            int selectedOption = GetUserSelection();
                            var showProvider = new ShowProvider();


                            if (selectedOption == 1)
                            {
                                var searchedActivity = GetActivityByID("Provide activity ID to find");
                                showProvider.PrintActivity(searchedActivity);
                            }
                            else if (selectedOption == 2)
                            {
                                var dataProvider = new FileDataProvider();
                                showProvider.PrintActivities(dataProvider.GetActivityByTerm(true));
                            }
                            else if (selectedOption == 3)
                            {
                                var dataProvider = new FileDataProvider();
                                showProvider.PrintActivities(dataProvider.GetActivityByTerm(false));
                            }
                            else if (selectedOption == 4)
                            {
                                var searchedActivity = GetActivitiesByCategory();
                                showProvider.PrintActivities(searchedActivity);
                            }
                            break;
                        }
                    case 3:
                        {
                            optionsProvider.PrintEditionOptions();
                            int selectedEditionOption = GetUserSelection();

                            GetSelectedEditionOption(selectedEditionOption);
                            break;
                        }
                    default:
                        Console.WriteLine("You provide wrong option number");
                        break;

                }
            }
            while (userSelection != 4);
        }

        private int GetUserSelection()
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

        private IEnumerable<Activity> GetActivitiesByCategory()
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

        private void GetSelectedEditionOption(int selectedOption)
        {
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();
            var activities = dataProvider.GetActivities();

            switch (selectedOption)
            {
                case 1:
                    {
                        var categoriesControllers = new CategoriesControllers();
                        categoriesControllers.AddNewCategory();
                        break;
                    }
                case 2:
                    {
                        if (!categories.Any())
                        {
                            Console.WriteLine("Please first add at least one category");
                            var categoriesControllers = new CategoriesControllers();
                            categoriesControllers.AddNewCategory();
                        }
                        else
                        {
                            var activitiesControllers = new ActivitiesControllers();
                            activitiesControllers.AddNewActivity();
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
                            var activitiesControllers = new ActivitiesControllers();
                            activitiesControllers.DeleteActivity();
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
                            var categoriesControllers = new CategoriesControllers();
                            categoriesControllers.DeleteCategory();
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