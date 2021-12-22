using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;

namespace ToDoList.DataControllers
{
    public class FileDataController : IFileDataController
    {
        private int GetUserSelection()
        {
            string selectedOption = Console.ReadLine();
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
            string ID = Console.ReadLine();
            while (!Int32.TryParse(ID, out int n))
            {
                Console.WriteLine("ID must be number");
                ID = Console.ReadLine();
            }
            return Int32.Parse(ID);
        }

        protected Item GetItemByID(string msg)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<Item> items = dataProvider.GetItems();

            int itemID = GetID(msg);

            try
            {
                var item = items.Single(item => item.ItemId == itemID);
                Console.Clear();
                return item;
            }
            catch(Exception)
            {
                throw new Exception($"You don't have item with {itemID} ID");
            }

        }


        protected IEnumerable<Item> GetItemsByCategory(string category)
        {
            var dataProvider = new FileDataProvider();
            IEnumerable<string> categories = dataProvider.GetCategories();
            IEnumerable<Item> items = dataProvider.GetItems();
            try
            {
                Console.Clear();
                return items.Where(item => item.ItemCategory == category);
            }
            catch(Exception)
            {
                throw new Exception($"You don't have {category} category");
            }
        }


        public void ChooseMainOption()
        {
            var optionsProvider = new Options();
            optionsProvider.PrintMainOptions();
            int userSelection = GetUserSelection();


            if (userSelection == 1)
            {
                var dataProvider = new FileDataProvider();
                IEnumerable<Item> items = dataProvider.GetItems();
                var showProvider = new ShowProvider();
                showProvider.PrintManyItems(items);
            }
            else if (userSelection == 2)
            { 
                optionsProvider.PrintItemSearchOptions();
                int selectedSearchItemOption = GetUserSelection();

                if (selectedSearchItemOption == 1)
                {
                    Item searchedItem = GetItemByID("Provide item ID to find");
                    var showProvider = new ShowProvider();
                    showProvider.PrintItem(searchedItem);
                }
                else if (selectedSearchItemOption == 2)
                {

                    Console.WriteLine("Provide items category to find");
                    string userInput = Console.ReadLine();

                    IEnumerable<Item> searchedItems = GetItemsByCategory(userInput);
                    Console.Clear();
                    var showProvider = new ShowProvider();
                    showProvider.PrintManyItems(searchedItems);                    
                }

            }
            else if (userSelection == 3)
            {
                optionsProvider.PrintEditionOptions();
                int selectedEditionOption = GetUserSelection();

                var editionController = new FileEditionController();
                editionController.GetSelectedEditionOption(selectedEditionOption);
            }
            else if (userSelection == 4)
            {
                Console.WriteLine("Goodbye ! :D");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("You provide wrong option number");
            }
        }
    }
}
