using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Activities
{
    public class ActivitiesControllers : DataController
    {
        public ActivitiesControllers(IView _view) : base(_view) { }

        public void StartActivity()
        {
            if (checkIfActivitiesExist()) return;

            var dataProvider = new DataProvider();

            int activityID = _view.GetID();
            var activity = GetActivityByID(activityID);

            if (activity.IsDone)
            {
                _view.ErrorMessage("You already done this activity");
                return;
            }

            activity.IsActive = true;
            dataProvider.UpdateActivity(activity);
        }

        public void SetActivityAsDone()
        {
            if (checkIfActivitiesExist()) return;

            var dataProvider = new DataProvider();

            int activityID = _view.GetID();
            var activity = GetActivityByID(activityID);

            if (!activity.IsActive)
            {
                _view.ErrorMessage("You didn't start this activity");
                return;
            }

            activity.IsDone = true;
            dataProvider.UpdateActivity(activity);
        }

        public void AddNewActivity(ViewBag viewBag)
        {
            var dataProvider = new DataProvider();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity._id == viewBag.Id))
            {
                _view.ErrorMessage("You already have activity with this ID");
                return;
            }

            try
            {
                var activity = new Activity()
                {
                    _id = viewBag.Id,
                    Title = viewBag.Title,
                    Description = viewBag.Description,
                    Category = viewBag.Category,
                    EndDate = viewBag.EndDate
                };

                dataProvider.AddActivity(activity);
            }
            catch (Exception)
            {
                _view.ErrorMessage("You provided wrong data");
                return;
            }

        }

        public void EditActivity(ViewBag viewBag)
        {
            var dataProvider = new DataProvider();
            try
            {
                var activity = new Activity()
                {
                    _id = viewBag.Id,
                    Title = viewBag.Title,
                    Description = viewBag.Description,
                    Category = viewBag.Category,
                    EndDate = viewBag.EndDate
                };
                dataProvider.UpdateActivity(activity);
            }
            catch (Exception)
            {
                _view.ErrorMessage("You provided wrong data");
                return;
            }
        }

        public void DeleteActivity()
        {
            var dataProvider = new DataProvider();

            int activityID = _view.GetID();
            var activityToDelete = GetActivityByID(activityID);
            dataProvider.RemoveActivity(activityToDelete);

            _view.DisplayMessage("Successfully deleted activity");
        }

        public DateTime GetDate()
        {
            string[] dateQuery = new string[] { "Year", "Month", "Day", "Hour", "Minute" };

            DateTime date;
            do
            {
                var dataDate = new List<int> { };

                foreach (var data in dateQuery)
                {
                    _view.DisplayMessage($"Provide {data}");
                    int numericValue = _view.GetNumericValue();
                    dataDate.Add(numericValue);
                }

                date = new DateTime(
                    dataDate[0],
                    dataDate[1],
                    dataDate[2],
                    dataDate[3],
                    dataDate[4],
                    0
                );

            } while (date.CompareTo(DateTime.Now) <= 0);

            return date;
        }

        private bool checkIfActivitiesExist()
        {
            var dataProvider = new DataProvider();
            return dataProvider.GetActivities().Any();  
        }
    }
}
