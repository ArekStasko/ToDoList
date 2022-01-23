using ToDoList.Controllers;

namespace ToDoList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _options = new OptionsPrinter(); 
            var _view = new View();
            var dataController = new DataController(_view);


            int userSelection;

            do
            {
                _options.PrintMainOptions();
                userSelection = _view.GetNumericValue();

                switch (userSelection)
                {
                    case 1:
                        {
                            var activities = dataController.GetInactiveActivities();
                            _view.PrintActivities(activities);
                            break;
                        }
                    case 2:
                        {
                            _options.PrintActivitySearchOptions();
                            int selectedOption = _view.GetNumericValue();

                            if (selectedOption == 1)
                            {                                
                                int activityID = _view.GetID();
                                var activity = dataController.GetActivityDataByID(activityID);
                                _view.PrintActivity(activity);
                            }
                            else if (selectedOption == 2)
                            {
                                var activities = dataController.GetActiveActivities();
                                _view.PrintActivities(activities);
                            }
                            else if (selectedOption == 3)
                            {
                                var activities = dataController.GetInactiveActivities();
                                _view.PrintActivities(activities);
                            }
                            else if (selectedOption == 4)
                            {
                                Console.WriteLine("Provide activities category to find");
                                string category = _view.GetStringValue();
                                var activities = dataController.GetActivitiesByCategory(category);
                                _view.PrintActivities(activities);
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
    }
}