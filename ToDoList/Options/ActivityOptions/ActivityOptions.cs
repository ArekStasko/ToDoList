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
                        activity._id = GetID();

                        Console.WriteLine("Provide activity Title");
                        activity.Title = GetStringValue();

                        Console.WriteLine("Provide activity Description");
                        activity.Description = GetStringValue();

                        Console.WriteLine("Provide activity Category");
                        activity.Category = categoriesControllers.GetCategory();

                        Console.WriteLine("Provide activity end date");
                        activity.EndDate = activitiesControllers.GetDate();

                        activitiesControllers.AddNewActivity(activity);
                        DisplayMessage("Successfully added new Activity");
                        break;
                    }
                case 2:
                    {
                        GetEditedData();
                        break;
                    }
                case 3:
                        activitiesControllers.SetActivityAsActive(GetID());
                        break;
                case 4:
                        activitiesControllers.SetActivityAsDone(GetID());
                        break;
                case 5:
                    PrintActivityDesc();
                    foreach (var activity in activities)
                        PrintActivity(activity.ConvertToDataRow());
                    break;
                case 6:
                    {
                        if (!activities.Any())
                        {
                            DisplayMessage("You don't have any activities");
                            break;
                        }

                        activitiesControllers.DeleteActivity(GetID());
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
            int option;

            do
            {
                _options.PrintEditActivityOptions();
                option = GetNumericValue();
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
                option = GetNumericValue();
            } while (option != 5);

            activitiesControllers.EditActivity(activity);
        }
    }
}