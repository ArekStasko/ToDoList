using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;

namespace ToDoList
{
    internal class ActivityOptions : View
    {
        private ActivitiesControllers activitiesControllers;
        private CategoriesControllers categoriesControllers;

        public ActivityOptions()
        {
            activitiesControllers = new ActivitiesControllers(this);
            categoriesControllers = new CategoriesControllers(this);
        }
        internal void RunActivityController()
        {
            var categories = categoriesControllers.GetCategories();
            var activities = activitiesControllers.GetActivityData();

            _options.PrintActivitiesOptions();
            int selectedOption = GetNumericValue();

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
                            Id = GetNumericValue(),
                            Title = GetStringValue(),
                            Description = GetStringValue(),
                            Category = categoriesControllers.GetCategory(),
                            EndDate = activitiesControllers.GetDate(),
                        };

                        activitiesControllers.AddNewActivity(viewBag);
                        DisplayMessage("Successfully added new Activity");
                        _options.PrintAddActivityOptions();
                        break;
                    }
                case 2:
                        var activity = GetEditedData();
                        activitiesControllers.EditActivity(activity);
                        break;
                case 3:
                        activitiesControllers.StartActivity();
                        break;
                case 4:
                        activitiesControllers.SetActivityAsDone();
                        break;
                case 5:
                        PrintActivities(activities);
                        break;
                case 6:
                    {
                        if (!activities.Any())
                        {
                            DisplayMessage("You don't have any activities");
                            break;
                        }

                        activitiesControllers.DeleteActivity();
                        break;
                    }
                default:
                    {
                        ErrorMessage("You provide wrong option number");
                        break;
                    }
            }
        }

        private ViewBag GetEditedData()
        {
            var activity = activitiesControllers.GetActivityDataByID(GetID());
            int option = GetNumericValue();

            do
            {
                switch (option)
                {
                    case 1:
                        DisplayMessage("Provide new activity name");
                        activity.Title = GetStringValue();
                        break;
                    case 2:
                        activity.Category = categoriesControllers.GetCategory();
                        break;
                    case 3:
                        DisplayMessage("Provide new activity description");
                        activity.Description = GetStringValue();
                        break;
                    case 4:
                        DisplayMessage("Provide new activity deadline date");
                        activity.EndDate = activitiesControllers.GetDate();
                        break;
                }
                _options.PrintEditActivityOptions();
                option = GetNumericValue();
            } while (option != 6);



            return activity;
        }
    }
}
