using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;
using ToDoList.Controllers.Categories;

namespace ToDoList.Controllers.Activities
{
    internal class ActivitiesControllers : FileDataController, IActivitiesControllers
    {

        public void AddNewActivity()
        {
            string[] activityQuery = new string[] { "Activity Name", "Activity Description" };
            List<string> activityData = new List<string>(2) { "", "" };

            int activityID = GetNumericValue("Provide new activity ID");

            var dataProvider = new FileDataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity.ActivityID == activityID))
            {
                Console.WriteLine("You already have activity with this ID");
                activityID = GetNumericValue("Provide new activity unique ID");
            }

            for (int i = 0; i < activityQuery.Length; i++)
            {
                Console.WriteLine($"Insert {activityQuery[i]}");
                string? providedData = Console.ReadLine();

                while (String.IsNullOrEmpty(providedData))
                {
                    Console.WriteLine("-You can't add empty data-");
                    providedData = Console.ReadLine();
                }

                activityData[i] = providedData;
            }

            var categoriesControllers = new CategoriesControllers();
            string category = categoriesControllers.GetCategory();

            var startDate = GetUserDate("Provide start date of activity");
            var deadlineDate = GetUserDate("Provide deadline date of activity");

            var newActivity = new Activity()
            {
                ActivityName = activityData[0],
                ActivityDescription = activityData[1],
                ActivityID = activityID,
                ActivityCategory = category,
                StartDate = startDate,
                DeadlineDate = deadlineDate,
            };



            dataProvider.AddActivity(newActivity);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully added new Activity");
        }

        public void DeleteActivity()
        {
            var dataProvider = new FileDataProvider();

            var activityToDelete = GetActivityByID("Provide ID of activity to delete");
            dataProvider.RemoveActivity(activityToDelete);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully deleted activity");
        }

        private DateTime GetUserDate(string msg)
        {
            Console.WriteLine(msg);
            string[] dateQuery = new string[] { "Year", "Month", "Day", "Hour", "Minute" };
            var dataDate = new List<int> { };

            foreach (var data in dateQuery)
            {
                int numericValue;
                do
                {
                    numericValue = GetNumericValue($"Provide {data}");
                }
                while (numericValue <= 0);
                dataDate.Add(numericValue);
            }

            try
            {
                DateTime date = new DateTime(
                    dataDate[0],
                    dataDate[1],
                    dataDate[2],
                    dataDate[3],
                    dataDate[4],
                    0
                    );

                var currentDate = DateTime.Now;

                if (date.CompareTo(currentDate) >= 0)
                    return date;
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                throw new Exception("You provided wrong date");
            }

        }
    }
}
