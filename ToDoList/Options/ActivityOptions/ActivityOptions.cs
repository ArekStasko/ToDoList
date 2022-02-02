using ToDoList.Controllers.Factories;

namespace ToDoList
{
    public class ActivityOptions : View, IActivityOptions
    {
        public void RunActivityController()
        {
            var categories = categoriesControllers.GetCategories();
            var activities = activitiesControllers.GetActivities();

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

                        var activity = Factory.NewActivityInstance();
                        activity._id = GetNumericValue();
                        activity.Title = GetStringValue();
                        activity.Description = GetStringValue();
                        activity.Category = categoriesControllers.GetCategory();
                        activity.EndDate = activitiesControllers.GetDate();

                        activitiesControllers.AddNewActivity(activity);
                        DisplayMessage("Successfully added new Activity");
                        _options.PrintAddActivityOptions();
                        break;
                    }
                case 2:
                    {
                        GetEditedData();
                        break;
                    }
                case 3:
                        activitiesControllers.StartActivity();
                        break;
                case 4:
                        activitiesControllers.SetActivityAsDone();
                        break;
                case 5:
                    PrintActivityDesc();
                    foreach (var activity in activities)
                        Console.WriteLine($"| {activity._id} | {activity.Category} | {activity.Title} | {activity.Description} | {activity.EndDate} |");
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

        public void GetEditedData()
        {
            var activity = activitiesControllers.GetActivityByID(GetID());
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

            activitiesControllers.EditActivity(activity);
        }
    }
}