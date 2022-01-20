using ToDoList.Controllers;

namespace ToDoList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _options = new OptionsPrinter(); ;
            var _view = new View();
            var dataController = new FileDataController(_view);


            int userSelection;

            do
            {
                _options.PrintMainOptions();
                userSelection = dataController.GetUserSelection(5);

                switch (userSelection)
                {
                    case 1:
                        {
                            var activities = dataController.GetActivityStructs();
                            activities = activities.Where(activity => !activity._isDone);
                            _view.PrintActivities(activities);
                            break;
                        }
                    case 2:
                        {
                            _options.PrintActivitySearchOptions();
                            int selectedOption = dataController.GetUserSelection(4);

                            if (selectedOption == 1)
                            {
                                var activity = dataController.GetActivityStructByID();
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
                                var activities = dataController.GetActivitiesByCategory();
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