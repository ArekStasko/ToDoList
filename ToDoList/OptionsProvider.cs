
namespace ToDoList
{
    public class OptionsProvider 
    {
        private Options Options;
        private View _view;

        public OptionsProvider()
        {
            Options = new Options();
            _view = new View();  
        }

        public void ChooseMainOption()
        {
            int userSelection;

            do
            {
                userSelection = GetUserSelection(5);

                switch (userSelection)
                {
                    case 1:
                        {
                            var dataProvider = new FileDataProvider();
                            IEnumerable<Activity> activities = dataProvider.GetActivities();


                            _view.PrintActivities(activities.Where(activity => !activity.IsDone));
                            break;
                        }
                    case 2:
                        {
                            int selectedOption = GetUserSelection(4);



                            if (selectedOption == 1)
                            {
                                var searchedActivity = GetActivityByID();
                                _view.PrintActivity(searchedActivity);
                            }
                            else if (selectedOption == 2)
                            {
                                var dataProvider = new FileDataProvider();
                                _view.PrintActivities(dataProvider.GetActivityByTerm(true));
                            }
                            else if (selectedOption == 3)
                            {
                                var dataProvider = new FileDataProvider();
                                _view.PrintActivities(dataProvider.GetActivityByTerm(false));
                            }
                            else if (selectedOption == 4)
                            {
                                var searchedActivity = GetActivitiesByCategory();
                                _view.PrintActivities(searchedActivity);
                            }
                            break;
                        }
                    case 3:
                        {
                            int selectedEditionOption = GetUserSelection(5);

                            RunActivityController(selectedEditionOption);
                            break;
                        }
                    case 4:
                        {
                            int selectedEditionOption = GetUserSelection(2);

                            RunCategoryController(selectedEditionOption);
                            break;
                        }
                    default:
                        Console.WriteLine("Goodbye :D");
                        break;

                }
            }
            while (userSelection != 5);
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
                        var activityToEdit = GetActivityByID();
                        var currentDate = DateTime.Now;
                        if (activityToEdit.StartDate.CompareTo(currentDate) >= 0)
                        {
                            _view.ErrorMessage("You can't edit activity which already started");
                            throw new Exception();
                        }
                        int editSelection;
                        do
                        {
                            activityToEdit = GetActivityByID(activityToEdit.ActivityID);

                            editSelection = GetUserSelection(6);
                            activitiesControllers.EditActivity(editSelection, activityToEdit);
                        } while (editSelection != 6);
                        break;
                    }
                case 3:
                    {
                        if (!activities.Any())
                        {
                            _view.DisplayMessage("You don't have any activities");
                            break;
                        }

                        activitiesControllers.SetActivityAsDone();
                        break;
                    }
                case 4:
                    {
                        _view.PrintActivities(activities.Where(activity => activity.IsDone));
                        break;
                    }
                case 5:
                    {
                        if (!activities.Any())
                        {
                            _view.DisplayMessage("You don't have any activities");
                            break;
                        }

                        activitiesControllers.DeleteActivity();
                        break;
                    }
                default:
                    {
                        _view.ErrorMessage("You provide wrong option number");
                        break;
                    }
            }
        }
    }
}
