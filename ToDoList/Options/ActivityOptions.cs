using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;

namespace ToDoList
{
    internal class ActivityOptions : OptionsPrinter
    {
        public View _view = new View();
        private ActivitiesControllers activitiesControllers;
        private CategoriesControllers categoriesControllers;

        public ActivityOptions()
        {
            activitiesControllers = new ActivitiesControllers(_view);
            categoriesControllers = new CategoriesControllers(_view);
        }
        internal void RunActivityController()
        {
            var categories = categoriesControllers.GetCategories();
            var activities = activitiesControllers.GetActivityData();

            PrintActivitiesOptions();
            int selectedOption = _view.GetNumericValue();

            switch (selectedOption)
            {
                case 1:
                    {
                        if (!categories.Any())
                        {
                            Console.WriteLine("Please first add at least one category");
                            categoriesControllers.AddNewCategory();
                        }

                        var viewBag = new ViewBag()
                        {
                            Id = _view.GetNumericValue(),
                            Title = _view.GetStringValue(),
                            Description = _view.GetStringValue(),
                            Category = categoriesControllers.GetCategory(),
                            EndDate = activitiesControllers.GetDate(),
                        };

                        activitiesControllers.AddNewActivity(viewBag);
                        _view.DisplayMessage("Successfully added new Activity");
                        PrintAddActivityOptions();
                        break;
                    }
                case 2:
                    {
                        var activity = GetEditedData();
                        activitiesControllers.EditActivity(activity);
                        break;
                    }
                case 3:
                    {
                        activitiesControllers.StartActivity();
                        break;
                    }
                case 4:
                    {
                        activitiesControllers.SetActivityAsDone();
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

        private ViewBag GetEditedData()
        {
            var activity = activitiesControllers.GetActivityDataByID(_view.GetID());
            int option = _view.GetNumericValue();

            do
            {
                switch (option)
                {
                    case 1:
                        _view.DisplayMessage("Provide new activity name");
                        activity.Title = _view.GetStringValue();
                        break;
                    case 2:
                        var categoriesControllers = new CategoriesControllers(_view);
                        activity.Category = categoriesControllers.GetCategory();
                        break;
                    case 3:
                        _view.DisplayMessage("Provide new activity description");
                        activity.Description = _view.GetStringValue();
                        break;
                    case 4:
                        _view.DisplayMessage("Provide new activity deadline date");
                        activity.EndDate = activitiesControllers.GetDate();
                        break;
                }
                PrintEditActivityOptions();
                option = _view.GetNumericValue();
            } while (option != 6);



            return activity;
        }
    }
}
