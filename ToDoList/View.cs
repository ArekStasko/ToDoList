using ToDoList.Controllers.Activities;
using ToDoList.Controllers;


namespace ToDoList
{
    public class View : IView
    {
        public void PrintActivity(ActivityStruct activity)
        {
            Console.WriteLine($"| Activity ID | Activity Category | Activity Title | Activity Description | Start Date | End Date |");

            if (activity._isActive)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;


            Console.WriteLine($"| {activity.Id} | {activity.Category} | {activity.Title} | {activity.Description} | {activity.StartDate} | {activity.EndDate} |");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintActivities(IEnumerable<ActivityStruct> activities)
        {
            foreach (var activity in activities)
                PrintActivity(activity);
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
            Console.WriteLine($"{msg}");
        }

        public void ErrorMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{msg}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void ClearView() => Console.Clear();
        public string? GetData() => Console.ReadLine();

    }
}
