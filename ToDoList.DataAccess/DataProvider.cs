using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess
{
    public class DataProvider : IDataProvider
    {
        private const string categoriesFilePath = @".\categories.txt";
        private const string activitiesFilePath = @".\activities.txt";
        private const string separator = "|";

        private void InitializeActivitiesFile()
        {
            if (!File.Exists(activitiesFilePath))
            {
                using (File.Create(activitiesFilePath))
                {

                }
            }
        }

        private void InitializeCategoriesFile()
        {
            if (!File.Exists(categoriesFilePath))
            {
                using (File.Create(categoriesFilePath))
                {

                }
            }
        }

        public IEnumerable<IActivity> GetActivities()
        {
            InitializeActivitiesFile();
            foreach (string line in File.ReadLines(activitiesFilePath))
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] data = line.Split(separator.ToCharArray());
                    IActivity newActivity = new Activity()
                    {
                        _id = Int32.Parse(data[0]),
                        Category = data[1],
                        Title = data[2],
                        Description = data[3],
                        EndDate = DateTime.ParseExact(data[4], "MM/dd/yyyy HH:mm", null),
                        IsActive = data[5] == "1",
                        IsDone = data[6] == "1"
                    };
                    yield return newActivity;
                }
            }
        }
        
        public IEnumerable<IActivity> GetActiveActivities()
        {
            var activities = GetActivities();
            return activities.Where(activity => activity.IsActive);
        }

        public IEnumerable<IActivity> GetInactiveActivities()
        {
            var activities = GetActivities();
            return activities.Where(activity => !activity.IsActive);
        }

        public IActivity GetActivityByID(int _id)
        {
            var activity = GetActivities().First(act => act._id == _id);
            return activity;
        }

        public IEnumerable<IActivity> GetActivitiesByCategory(string category)
        {
            var activities = GetActivities();
            return activities.Where(activity => activity.Category == category);
        }

        public IEnumerable<string> GetCategories()
        {
            InitializeCategoriesFile();
            foreach (string line in File.ReadLines(categoriesFilePath))
                if (!String.IsNullOrWhiteSpace(line))
                {
                    yield return line;
                }
        }

        public void AddCategory(string newCategory)
        {
            InitializeCategoriesFile();
            File.AppendAllText(categoriesFilePath, newCategory + Environment.NewLine);
        }

        public void RemoveCategory(string categoryToRemove)
        {
            InitializeCategoriesFile();
            var categories = GetCategories().ToList();
            categories.Remove(categoryToRemove);

            var activitiesToRemove = GetActivitiesByCategory(categoryToRemove).ToList();
            RemoveActivity(activitiesToRemove);

            File.WriteAllText(categoriesFilePath, string.Empty);
            foreach (var category in categories)
            {
                AddCategory(category);
            }
        }

        public void AddActivity(IActivity newActivity)
        {
            InitializeActivitiesFile();
            string line = string.Join(separator, newActivity.ConvertToDataRow());
            File.AppendAllText(activitiesFilePath, line + Environment.NewLine);
        }

        public void AddActivities(List<IActivity> newActivities)
        {
            foreach (var activity in newActivities)
                AddActivity(activity);
        }

        public void RemoveActivity(IActivity activityToRemove)
        {
            InitializeActivitiesFile();
            var activities = GetActivities().ToList();
            activities.Remove(activityToRemove);
            File.WriteAllText(activitiesFilePath, string.Empty);
            AddActivities(activities);
        }

        public void RemoveActivity(List<IActivity> newActivities)
        {
            foreach (var activity in newActivities)
                RemoveActivity(activity);
        }

        public void UpdateActivity(IActivity activityToUpdate)
        {
            RemoveActivity(activityToUpdate);
            AddActivity(activityToUpdate);
        }

    }
}
