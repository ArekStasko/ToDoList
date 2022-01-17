using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Controllers;


namespace ToDoList
{
    public class View : IView
    {
        public void PrintActivity(string activity)
        {
            Console.WriteLine($"| Activity ID | Activity Category | Activity Name | Activity Description | Start Date | Deadline |");
            Console.WriteLine(activity);
        }

        public void PrintActivities(IEnumerable<string> activities)
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

        public void ClearView() => Console.Clear();
        public string? GetData() => Console.ReadLine();

    }
}
