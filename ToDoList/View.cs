using ToDoList.Controllers.Activities;
using ToDoList.Controllers;


namespace ToDoList
{
    public class View : IView
    {
        public void PrintActivity(ViewBag activity)
        {
            Console.WriteLine($"| Activity ID | Activity Category | Activity Title | Activity Description | End Date |");

            if (activity._isActive)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;


            Console.WriteLine($"| {activity.Id} | {activity.Category} | {activity.Title} | {activity.Description} | {activity.EndDate} |");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintActivities(IEnumerable<ViewBag> activities)
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

        public string GetStringValue()
        {
            string? providedData = GetData();

            while (String.IsNullOrEmpty(providedData))
            {
                DisplayMessage("-You can't add empty data-");
                providedData = GetData();
            }

            return providedData;
        }

        public int GetNumericValue()
        {
            string? NumVal = GetData();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                DisplayMessage("Input must be non 0 number");
                NumVal = GetData();
            }
            return Int32.Parse(NumVal);
        }

        public int GetID()
        {
            DisplayMessage("Provide activity ID");
            return GetNumericValue();
        }
    }
}
