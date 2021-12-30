﻿using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;

namespace ToDoList.DataControllers
{
    public class FileDataController : IFileDataController
    {
        private int GetUserSelection()
        {
            string? selectedOption = Console.ReadLine();
            int optionNumber;

            while (!Int32.TryParse(selectedOption, out optionNumber))
            {
                Console.WriteLine("You have to choose the number of option");
                selectedOption = Console.ReadLine();
            }
            Console.Clear();
            return optionNumber;
        }

        protected int GetID(string message)
        {
            Console.WriteLine(message);
            string? ID = Console.ReadLine();
            while (!Int32.TryParse(ID, out int n) || string.IsNullOrEmpty(ID))
            {
                Console.WriteLine("ID must be number");
                ID = Console.ReadLine();
            }
            return Int32.Parse(ID);
        }

        protected Activity GetActivityByID(string msg)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Activity> activities = dataProvider.GetActivities();

            int activityID = GetID(msg);

            try
            {
                var activity = activities.Single(activity => activity.ActivityID == activityID);
                Console.Clear();
                return activity;
            }
            catch(Exception)
            {
                throw new Exception($"You don't have item with {activityID} ID");
            }

        }


        protected IEnumerable<Activity> GetActivitiesByCategory(string category)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<string> categories = dataProvider.GetCategories();
            IEnumerable<Activity> activities = dataProvider.GetActivities();
            try
            {
                Console.Clear();
                return activities.Where(activity => activity.ActivityCategory == category);
            }
            catch(Exception)
            {
                throw new Exception($"You don't have {category} category");
            }
        }

        private IEnumerable<Activity> GetActivitiesByCategory()
        {
            Console.WriteLine("Provide activities category to find");
            string? category;
            Console.Clear();

            do
            {
                category = Console.ReadLine();
            } 
            while (string.IsNullOrEmpty(category));

            return GetActivitiesByCategory(category);            

        }


        public void ChooseMainOption()
        {
            var optionsProvider = new Options();
            optionsProvider.PrintMainOptions();

            int userSelection = GetUserSelection();

            while(userSelection != 4)
            {
                switch (userSelection)
                {
                    case 1:
                        {
                            var dataProvider = new FileDataProvider();
                            IEnumerable<Activity> activities = dataProvider.GetActivities();

                            var showProvider = new ShowProvider();
                            showProvider.PrintManyActivities(activities);
                            break;
                        }
                    case 2:
                        {
                            optionsProvider.PrintItemSearchOptions();
                            int selectedSearchItemOption = GetUserSelection();
                            var showProvider = new ShowProvider();


                            if (selectedSearchItemOption == 1)
                            {
                                Activity searchedActivity = GetActivityByID("Provide activity ID to find");
                                showProvider.PrintActivity(searchedActivity);
                            }
                            else if (selectedSearchItemOption == 2)
                            {
                                IEnumerable<Activity> searchedActivity = GetActivitiesByCategory();
                                showProvider.PrintManyActivities(searchedActivity);
                            }
                            break;
                        }
                    case 3:
                        {
                            optionsProvider.PrintEditionOptions();
                            int selectedEditionOption = GetUserSelection();

                            var editionController = new FileEditionController();
                            editionController.GetSelectedEditionOption(selectedEditionOption);
                            break;
                        }
                    default:
                        Console.WriteLine("You provide wrong option number");
                        break;
                }

                userSelection = GetUserSelection();
            }

        }
    }
}
