using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;

namespace ToDoList
{
    internal class ActivityOptions 
    {
        internal void RunActivityController()
        {
            var _options = new OptionsPrinter(); ;
            var _view = new View();

            var activitiesController = new ActivitiesControllers(_view);
            var categoriesController = new CategoriesControllers(_view);

            var categories = categoriesController.GetCategories();
            var activities = activitiesController.GetActivityStructs();

            _options.PrintActivitiesOptions();
            int selectedOption = _view.GetNumericValue();

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

                        _options.PrintAddActivityOptions();
                        activitiesController.AddNewActivity();
                        break;
                    }
                case 2:
                    {
                        int activityID = _view.GetID();
                        var activityToEdit = activitiesController.GetActivityByID(activityID);

                        int editSelection;
                        do
                        {
                            activityToEdit = activitiesController.GetActivityByID(activityToEdit._id);

                            _options.PrintEditActivityOptions();
                            editSelection = _view.GetNumericValue();
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

                        activitiesController.StartActivity();
                        break;
                    }
                case 4:
                    {
                        if (!activities.Any())
                        {
                            _view.DisplayMessage("You don't have any activities");
                            break;
                        }

                        activitiesController.SetActivityAsDone();
                        break;
                    }
                case 5:
                    {
                        _view.PrintActivities(activities);
                        break;
                    }
                case 6:
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
