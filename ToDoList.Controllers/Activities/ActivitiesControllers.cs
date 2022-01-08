using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;

namespace ToDoList.Controllers.Activities
{
    internal class ActivitiesControllers : FileDataController, IActivitiesControllers
    {

        public void SetActivityAsDone()
        {
            var dataProvider = new FileDataProvider();
            var activity = GetActivityByID("Please provide activity ID");

            var currentDate = DateTime.Now;
            if (activity.StartDate.CompareTo(currentDate) >= 0)
            {
                throw new Exception("You don't even start this activity");
            }

            activity.IsDone = true;
            dataProvider.UpdateActivity(activity);
        }

        public void AddNewActivity()
        {
            string[] activityQuery = new string[] { "Activity Name", "Activity Description" };
            List<string> activityData = new List<string>(2) { "", "" };

            Console.WriteLine("Provide new activity ID");
            int activityID = GetNumericValue();

            var dataProvider = new FileDataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity.ActivityID == activityID))
            {
                Console.WriteLine("You already have activity with this ID");
                activityID = GetNumericValue();
            }

            for (int i = 0; i < activityQuery.Length; i++)
            {
                activityData[i] = GetStringValue($"Insert {activityQuery[i]}");
            }

            var categoriesControllers = new CategoriesControllers();
            string category = categoriesControllers.GetCategory();

            var startDate = GetDate("Provide start date of activity");
            var deadlineDate = GetDate("Provide deadline date of activity");

            while (deadlineDate <= startDate)
            {
                deadlineDate = GetDate("Deadline can't be earlier than start date");
            }

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

        public void EditActivity(int editOption, Activity activityToEdit)
        {
            switch (editOption)
            {
                case 1:
                    {
                        activityToEdit.ActivityName = GetStringValue("Provide new activity name");
                        break;
                    }
                case 2:
                    {
                        var categoriesControllers = new CategoriesControllers();
                        activityToEdit.ActivityCategory = categoriesControllers.GetCategory();
                        break;
                    }
                case 3:
                    {
                        activityToEdit.ActivityDescription = GetStringValue("Provide new activity description");
                        break;
                    }
                case 4:
                    {
                        activityToEdit.StartDate = GetDate("Provide new activity start date");
                        break;
                    }
                case 5:
                    {
                        activityToEdit.DeadlineDate = GetDate("Provide new activity deadline date");
                        break;
                    }
            }
            var dataProvider = new FileDataProvider();
            dataProvider.UpdateActivity(activityToEdit);

        }

        public void DeleteActivity()
        {
            var dataProvider = new FileDataProvider();

            var activityToDelete = GetActivityByID("Provide ID of activity to delete");
            dataProvider.RemoveActivity(activityToDelete);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully deleted activity");
        }

        private DateTime GetDate(string msg)
        {
            Console.WriteLine(msg);
            string[] dateQuery = new string[] { "Year", "Month", "Day", "Hour", "Minute" };
            var dataDate = new List<int> { };

            foreach (var data in dateQuery)
            {
                int numericValue;
                do
                {
                    Console.WriteLine($"Provide {data}");
                    numericValue = GetNumericValue();
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
