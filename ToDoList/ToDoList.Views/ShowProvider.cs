﻿using ToDoList.DataAccess.Models;

namespace ToDoList.Views
{
    public class ShowProvider
    {
        public void PrintActivity(Activity activity)
        {
            string activityRow = $"| {activity.ActivityID} | {activity.ActivityCategory} | {activity.ActivityName} | {activity.ActivityDescription} |";
            Console.WriteLine(activityRow);
        }

        public void PrintManyActivities(IEnumerable<Activity> activities)
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
    }
}