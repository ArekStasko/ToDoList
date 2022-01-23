using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Controllers.Activities;

namespace ToDoList.Controllers
{
    public class DataController
    {
        protected readonly IView _view;
        public DataController(IView view) => _view = view;

        public IEnumerable<ViewBag> GetInactiveActivities()
        {
            var dataProvider = new DataProvider();
            return ConvertActivities(dataProvider.GetInactiveActivities());
        }

        public IEnumerable<ViewBag> GetActiveActivities()
        {
            var dataProvider = new DataProvider();
            return ConvertActivities(dataProvider.GetActiveActivities());
        }

        public IEnumerable<ViewBag> GetActivityData() => ConvertActivities(GetActivities());

        public Activity GetActivityByID(int ID)
        {
            var dataProvider = new DataProvider();

            _view.ClearView();
            return dataProvider.GetActivityByID(ID);
        }

        public ViewBag GetActivityDataByID(int ID)
        {
            var activity = GetActivityByID(ID);
            return new ViewBag(activity);
        }

        public IEnumerable<ViewBag> GetActivitiesByCategory(string category)
        {
            var dataProvider = new DataProvider();
            var activities = dataProvider.GetActivitiesByCategory(category);
            return ConvertActivities(activities);
        }

        private IEnumerable<Activity> GetActivities()
        {
            var dataProvider = new DataProvider();
            return dataProvider.GetActivities();
        }

        private IEnumerable<ViewBag> ConvertActivities(IEnumerable<Activity> activities)
        {
            foreach(var activity in activities)
            {
                yield return new ViewBag(activity);
            }
        }
    }
}