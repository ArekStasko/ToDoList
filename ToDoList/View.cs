using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;
using ToDoList.Controllers;
using ToDoList.Controllers.Factories;

namespace ToDoList
{
    public class View : IView
    {
        protected IOptionsPrinter _options;
        protected IActivitiesControllers activitiesControllers;
        protected ICategoriesControllers categoriesControllers;
        protected View()
        {
            activitiesControllers = Factory.NewActControllersInstance(this);
            categoriesControllers = Factory.NewCatControllersInstance(this);
            _options = OptionsFactory.GetOptionsPrinterInstance();
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
                            PrintActivityDesc();
                            foreach (var activity in activities)
                                Console.WriteLine($"| {activity._id} | {activity.Category} | {activity.Title} | {activity.Description} | {activity.EndDate} |");
                            break;
                        }
                    case 2:
                        {
                            _options.PrintActivitySearchOptions();
                            int selectedOption = GetNumericValue();
                            PrintActivityDesc();

                            if (selectedOption == 1)
                            {
                                int activityID = GetID();
                                var activity = activitiesControllers.GetActivityByID(activityID);
                                PrintActivity(activity.ConvertToDataRow());
                            }
                            else if (selectedOption == 2)
                            {
                                var activities = activitiesControllers.GetActiveActivities();
                                foreach (var activity in activities)
                                    PrintActivity(activity.ConvertToDataRow());
                            }
                            else if (selectedOption == 3)
                            {
                                var activities = activitiesControllers.GetInactiveActivities();
                                foreach (var activity in activities)
                                    PrintActivity(activity.ConvertToDataRow());
                            }
                            else if (selectedOption == 4)
                            {
                                Console.WriteLine("Provide activities category to find");
                                string category = GetStringValue();
                                var activities = activitiesControllers.GetActivitiesByCategory(category);
                                foreach (var activity in activities)
                                    PrintActivity(activity.ConvertToDataRow());
                            }
                            break;
                        }
                    case 3:
                        IActivityOptions activityOptions = OptionsFactory.GetActOptionsInstance();
                        activityOptions.RunActivityController();
                        break;
                    case 4:
                        ICategoryOptions categoryOptions = OptionsFactory.GetCatOptionsInstance();
                        categoryOptions.RunCategoryController();
                        break;
                    default:
                        Console.WriteLine("Goodbye :D");
                        break;

                }
            } while (userSelection != 5);
        }

        protected void PrintActivity(string[] activityData)
        {
            Console.WriteLine($"| {activityData[0]} | {activityData[1]} | {activityData[2]} | {activityData[3]} | {activityData[4]} |");
        }

        protected void PrintActivityDesc()
        {
            Console.WriteLine($"| Activity ID | Activity Category | Activity Title | Activity Description | End Date |");
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