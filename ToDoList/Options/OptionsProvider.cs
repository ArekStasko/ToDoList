using ToDoList.Controllers;
using ToDoList.Controllers.Categories;
using ToDoList.Controllers.Activities;

namespace ToDoList
{
    public class OptionsProvider 
    {
        private Options Options;
        public View _view;

        public OptionsProvider()
        {
            Options = new Options();
            _view = new View();  
        }

        public void ChooseMainOption()
        {
            int userSelection;
            var dataController = new FileDataController(_view);

            do
            {
                userSelection = dataController.GetUserSelection(5);

                switch (userSelection)
                {
                    case 1:
                        {
                            var activities = dataController.GetActivities();
                            activities = activities.Where(activity => !activity.IsDone);

                            foreach(var activity in activities)
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
                                foreach(var activity in activities)
                                    _view.PrintActivity(activity.ConvertToString());
                            }
                            else if (selectedOption == 4)
                            {
                                var searchedActivity = dataController.GetActivitiesByCategory();
                                foreach(var activity in searchedActivity)                                
                                    _view.PrintActivity(activity.ConvertToString());
                            }
                            break;
                        }
                    case 3:
                        {
                            int selectedEditionOption = dataController.GetUserSelection(5);

                            RunActivityController(selectedEditionOption);
                            break;
                        }
                    case 4:
                        {
                            int selectedEditionOption = dataController.GetUserSelection(2);

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
            var categoriesControllers = new CategoriesControllers(_view);
            var categories = categoriesControllers.GetCategories();

            switch (selectedOption)
            {
                case 1:
                    {
                        categoriesControllers.AddNewCategory();
                        break;
                    }
                case 2:
                    {
                        if (categories.Any())
                        {
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

        private void RunActivityController(int selectedOption)
        {
            var activitiesController = new ActivitiesControllers(_view);
            var categoriesController = new CategoriesControllers(_view);   
            
            var categories = categoriesController.GetCategories();
            var activities = activitiesController.GetActivities();

            switch (selectedOption)
            {
                case 1:
                    {
                        if (categories.Any())
                        {
                            activitiesController.AddNewActivity();



                            break;
                        }

                        Console.WriteLine("Please first add at least one category");
                        categoriesController.AddNewCategory();
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
                            activityToEdit = activitiesController.GetActivityByID(activityToEdit.ActivityID);

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
