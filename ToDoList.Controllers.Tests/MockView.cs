using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System;
using ToDoList.DataAccess.Models;
using System.Collections.Generic;

namespace ToDoList.Controllers.Tests
{
    public class MockView : IView
    {
        private void PrintActivity(IActivity activity)
        {
            Console.WriteLine($"| Activity ID | Activity Category | Activity Title | Activity Description | End Date |");

            if (activity.IsActive)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;


            Console.WriteLine($"| {activity._id} | {activity.Category} | {activity.Title} | {activity.Description} | {activity.EndDate} |");
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected void PrintActivities(IEnumerable<IActivity> activities)
        {
            foreach (var activity in activities)
                PrintActivity(activity);
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
        public string? GetData() => throw new NotImplementedException();

        public string GetStringValue()
        {
            throw new NotImplementedException();
        }

        public int GetNumericValue()
        {
            throw new NotImplementedException();
        }

        public int GetID()
        {
            throw new NotImplementedException();
        }
    }
}
