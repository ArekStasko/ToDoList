using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;
using ToDoList.Controllers;
using ToDoList.DataAccess.Models;
using ToDoList.Controllers.Factories;

namespace ToDoList
{
    public class View : IView
    {
        protected OptionsPrinter _options;
        protected IActivitiesControllers activitiesControllers;
        protected ICategoriesControllers categoriesControllers;
        protected View()
        {
            activitiesControllers = Factory.NewActControllersInstance(this);
            categoriesControllers = Factory.NewCatControllersInstance(this);
            _options = new OptionsPrinter();
        }

        static void Main(string[] args)
        {
            var _view = new View();
            _view.RunApp();
        }

        public void RunApp()
        {
            int userSelection;
            do
            {
                _options.PrintMainOptions();
                userSelection = GetNumericValue();

                switch (userSelection)
                {
                    case 1:
                        {
                            var activities = activitiesControllers.GetInactiveActivities();
                            PrintActivities(activities);
                            break;
                        }
                    case 2:
                        {
                            _options.PrintActivitySearchOptions();
                            int selectedOption = GetNumericValue();

                            if (selectedOption == 1)
                            {
                                int activityID = GetID();
                                var activity = activitiesControllers.GetActivityByID(activityID);
                                PrintActivity(activity);
                            }
                            else if (selectedOption == 2)
                            {
                                var activities = activitiesControllers.GetActiveActivities();
                                PrintActivities(activities);
                            }
                            else if (selectedOption == 3)
                            {
                                var activities = activitiesControllers.GetInactiveActivities();
                                PrintActivities(activities);
                            }
                            else if (selectedOption == 4)
                            {
                                Console.WriteLine("Provide activities category to find");
                                string category = GetStringValue();
                                var activities = activitiesControllers.GetActivitiesByCategory(category);
                                PrintActivities(activities);
                            }
                            break;
                        }
                    case 3:
                        var activityOptions = new ActivityOptions();
                        activityOptions.RunActivityController();
                        break;
                    case 4:
                        var categoryOptions = new CategoryOptions();
                        categoryOptions.RunCategoryController();
                        break;
                    default:
                        Console.WriteLine("Goodbye :D");
                        break;

                }
            } while (userSelection != 5);
        }


        public void PrintActivity(IActivity activity)
        {
            Console.WriteLine($"| Activity ID | Activity Category | Activity Title | Activity Description | End Date |");

            if (activity.IsActive)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;


            Console.WriteLine($"| {activity._id} | {activity.Category} | {activity.Title} | {activity.Description} | {activity.EndDate} |");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintActivities(IEnumerable<IActivity> activities)
        {
            foreach (var activity in activities)
                PrintActivity(activity);
        }

        public void PrintCategories(IEnumerable<string> categories)
        {
            int index = 0;
            foreach (var category in categories)
            {
                Console.WriteLine($"{index + 1}. {category}");
                index++;
            }
        }

        public void DisplayMessage(string msg)
        {
            Console.WriteLine($"{msg}");
        }

        public void ErrorMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ClearView() => Console.Clear();
        public string? GetData() => Console.ReadLine();

        public string GetStringValue()
        {
            string? providedData = GetData();

            while (String.IsNullOrEmpty(providedData))
            {
                DisplayMessage("-You can't add empty data-");
                providedData = GetData();
            }

            return providedData;
        }

        public int GetNumericValue()
        {
            string? NumVal = GetData();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                DisplayMessage("Input must be non 0 number");
                NumVal = GetData();
            }
            return Int32.Parse(NumVal);
        }

        public int GetID()
        {
            DisplayMessage("Provide activity ID");
            return GetNumericValue();
        }
    }
}