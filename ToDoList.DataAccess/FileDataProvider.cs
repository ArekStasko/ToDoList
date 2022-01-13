using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess
{
    public class FileDataProvider : IDataProvider
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

        public IEnumerable<Activity> GetActivityByTerm(bool available)
        {
            var activities = GetActivities();

            if (activities.Any())
            {
                return available ? 
                    activities.Where(activity => activity.IsActive) :
                    activities.Where(activity => !activity.IsActive);
            }
            else
            {
                throw new Exception("You don't have any activity");
            }
        }

        public IEnumerable<Activity> GetActivities()
        {
            InitializeActivitiesFile();
            foreach (string line in File.ReadLines(activitiesFilePath))
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] data = line.Split(separator.ToCharArray());
                    var newActivity = new Activity()
                    {
                        ActivityID = Int32.Parse(data[0]),
                        ActivityCategory = data[1],
                        ActivityName = data[2],
                        ActivityDescription = data[3],
                        StartDate = DateTime.ParseExact(data[4], "MM/dd/yyyy HH:mm", null),
                        DeadlineDate = DateTime.ParseExact(data[5], "MM/dd/yyyy HH:mm", null),
                        IsDone = data[6] == "1"
                    };
                    yield return newActivity;
                }
            }
        }

        public IEnumerable<string> GetActivitiesToShow()
        {
            InitializeActivitiesFile();
            foreach (string line in File.ReadLines(activitiesFilePath))
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    yield return line;                  
                }
            }
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
            File.WriteAllText(categoriesFilePath, string.Empty);
            foreach (var category in categories)
            {
                AddCategory(category);
            }
        }

        public void AddActivity(Activity newActivity)
        {
            InitializeActivitiesFile();
            string line = string.Join(separator, newActivity.ConvertToDataRow());
            File.AppendAllText(activitiesFilePath, line + Environment.NewLine);
        }

        public void AddActivities(List<Activity> newActivities)
        {
            foreach (var activity in newActivities)
                AddActivity(activity);
        }

        public void RemoveActivity(Activity activityToRemove)
        {
            InitializeActivitiesFile();
            var activities = GetActivities().ToList();
            activities.Remove(activityToRemove);
            File.WriteAllText(activitiesFilePath, string.Empty);
            AddActivities(activities);
        }

        public void RemoveActivity(List<Activity> newActivities)
        {
            foreach (var activity in newActivities)
                RemoveActivity(activity);
        }

        public void UpdateActivity(Activity activityToUpdate)
        {
            RemoveActivity(activityToUpdate);
            AddActivity(activityToUpdate);
        }

    }
}
