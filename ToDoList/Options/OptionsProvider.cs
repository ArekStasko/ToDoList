using ToDoList.Controllers;
using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;

namespace ToDoList
{
    public class OptionsProvider
    {
        public Options _options;
        public View _view;

        public OptionsProvider()
        {
            _options = new Options();
            _view = new View();
        }

        public void ChooseMainOption()
        {
            int userSelection;
            var dataController = new FileDataController(_view);

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
                        RunActivityController();
                        break;
                    case 4:
                        RunCategoryController();
                        break;
                    default:
                        Console.WriteLine("Goodbye :D");
                        break;

                }
            } while (userSelection != 5);
        }

        private void RunCategoryController()
        {
            var categoriesControllers = new CategoriesControllers(_view);
            var categories = categoriesControllers.GetCategories();

            var dataController = new FileDataController(_view);
            int selectedOption = dataController.GetUserSelection(5);

            switch (selectedOption)
            {
                case 1:
                    {
                        Console.WriteLine("- Please insert new category name -");
                        categoriesControllers.AddNewCategory();
                        break;
                    }
                case 2:
                    {
                        if (categories.Any())
                        {
                            Console.WriteLine("Please provide the category name to delete");
                            Console.WriteLine("- This will delete all items with this category -");
                            categoriesControllers.DeleteCategory();
                            break;
                        }

                        Console.WriteLine("You don't have any categories");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("You provide wrong option number");
                        break;
                    }
            }
        }

        private void RunActivityController()
        {
            var activitiesController = new ActivitiesControllers(_view);
            var categoriesController = new CategoriesControllers(_view);

            var categories = categoriesController.GetCategories();
            var activities = activitiesController.GetActivities();

            var dataController = new FileDataController(_view);
            int selectedOption = dataController.GetUserSelection(2);

            switch (selectedOption)
            {
                case 1:
                    {
                        if (!categories.Any())
                        {
                            Console.WriteLine("Please first add at least one category");
                            categoriesController.AddNewCategory();
                            break;
                        }

                        string[] activityQuery = new string[] { "Activity Name", "Activity Description" };
                        Console.WriteLine("Provide data :");
                        Console.WriteLine();
                        activitiesController.AddNewActivity();
                        break;
                    }
                case 2:
                    {
                        var activityToEdit = activitiesController.GetActivityByID();
                        var currentDate = DateTime.Now;

                        if (activityToEdit.StartDate.CompareTo(currentDate) >= 0)
                        {
                            _view.ErrorMessage("You can't edit activity which already started");
                            throw new Exception();
                        }

                        int editSelection;
                        do
                        {
                            activityToEdit = activitiesController.GetActivityByID(activityToEdit._id);

                            editSelection = activitiesController.GetUserSelection(6);
                            activitiesController.EditActivity(editSelection, activityToEdit);
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

                        activitiesController.SetActivityAsDone();
                        break;
                    }
                case 4:
                    {
                        foreach (var activity in activities.Where(activity => activity.IsDone))
                            _view.PrintActivity(activity.ConvertToString());

                        break;
                    }
                case 5:
                    {
                        if (!activities.Any())
                        {
                            _view.DisplayMessage("You don't have any activities");
                            break;
                        }

                        activitiesController.DeleteActivity();
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
