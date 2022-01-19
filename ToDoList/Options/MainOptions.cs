using ToDoList.Controllers;

namespace ToDoList
{
    public class MainOptions
    {
        public OptionsPrinter _options;
        public View _view;
        protected FileDataController dataController;

        public MainOptions()
        {
            _options = new OptionsPrinter();
            _view = new View();
            dataController = new FileDataController(_view);
        }

        public void ChooseMainOption()
        {
            int userSelection;

            do
            {
                _options.PrintMainOptions();
                userSelection = dataController.GetUserSelection(5);

                switch (userSelection)
                {
                    case 1:
                        {
                            var activities = dataController.GetActivities();
                            activities = activities.Where(activity => !activity.IsDone);

                            foreach (var activity in activities)
                            {
                                _view.PrintActivity(activity.ConvertToString());
                            }
                            break;
                        }
                    case 2:
                        {
                            _options.PrintActivitySearchOptions();
                            int selectedOption = dataController.GetUserSelection(4);

                            if (selectedOption == 1)
                            {
                                var searchedActivity = dataController.GetActivityByID();
                                _view.PrintActivity(searchedActivity.ConvertToString());
                            }
                            else if (selectedOption == 2)
                            {
                                var activities = dataController.GetActiveActivities();
                                foreach (var activity in activities)
                                    _view.PrintActivity(activity.ConvertToString());
                            }
                            else if (selectedOption == 3)
                            {
                                var activities = dataController.GetInactiveActivities();
                                foreach (var activity in activities)
                                    _view.PrintActivity(activity.ConvertToString());
                            }
                            else if (selectedOption == 4)
                            {
                                Console.WriteLine("Provide activities category to find");
                                var searchedActivity = dataController.GetActivitiesByCategory();
                                foreach (var activity in searchedActivity)
                                    _view.PrintActivity(activity.ConvertToString());
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
