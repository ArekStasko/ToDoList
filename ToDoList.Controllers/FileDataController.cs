using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;

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
                            showProvider.PrintActivities(activities.Where(activity => !activity.IsDone));
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
                            optionsProvider.PrintActivitiesOptions();
                            int selectedEditionOption = GetUserSelection();

                            RunActivityController(selectedEditionOption);
                            break;
                        }
                    case 4:
                        {
                            optionsProvider.PrintCategoriesOptions();
                            int selectedEditionOption = GetUserSelection();

                            RunCategoryController(selectedEditionOption);
                            break;
                        }
                    default:
                        Console.WriteLine("You provide wrong option number");
                        break;

                }
            }
            while (userSelection != 5);
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

        private void RunCategoryController(int selectedOption)
        {
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();

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



        private void RunActivityController(int selectedOption)
        {
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();
            var activities = dataProvider.GetActivities();
            var activitiesControllers = new ActivitiesControllers();

            switch (selectedOption)
            {
                case 1:
                    {
                        if (!categories.Any())
                        {
                            Console.WriteLine("Please first add at least one category");
                            var categoriesControllers = new CategoriesControllers();
                            categoriesControllers.AddNewCategory();
                        }
                        else
                            activitiesControllers.AddNewActivity();
                        break;
                    }
                case 2:
                    {
                        if (!activities.Any())
                        {
                            Console.WriteLine("You don't have any activities");
                        }
                        else
                            activitiesControllers.SetActivityAsDone();
                        break;
                    }
                case 3:
                    {
                        var showProvider = new ShowProvider();
                        showProvider.PrintActivities(activities.Where(activity => activity.IsDone));
                        break;
                    }
                case 4:
                    {
                        if (!activities.Any())
                        {
                            Console.WriteLine("You don't have any activities");
                        }
                        else
                            activitiesControllers.DeleteActivity();
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