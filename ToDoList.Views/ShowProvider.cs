using ToDoList.DataAccess.Models;

namespace ToDoList.Views
{
    public class ShowProvider
    {
        public void PrintActivity(Activity activity)
        {
            string activityRow = $"| {activity.ActivityID} | {activity.ActivityCategory} | {activity.ActivityName} | {activity.ActivityDescription} |";
            string activitiTime = $"| {activity.StartDate} | {activity.DeadlineDate} |";

            Console.WriteLine($"| Activity ID | Activity Category | Activity Name | Activity Description |");
            Console.WriteLine(activityRow);
            Console.WriteLine($"| Start Date | Deadline |");
            Console.WriteLine(activitiTime);
            Console.WriteLine($"| {activity.GetTimeToDeadline()} |");
        }

        public void PrintActivities(IEnumerable<Activity> activities)
        {
            foreach (var activity in activities)
            {
                PrintActivity(activity);
            }
        }

        public void PrintCategories(IEnumerable<string> categories)
        {
            int index = 0;
            foreach (var category in categories)
            {
                Console.WriteLine($"{index + 1}. {category}");
                index++;
            }
        }

        public void DisplayMessage(string msg)
        {
            Console.Clear();
            Console.WriteLine($"{msg}");
        }

        public void ErrorMessage(string msg)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ClearView()
        {
            Console.Clear();
        }

        public string? GetData()
        {
            return Console.ReadLine();
        }
    }
}
