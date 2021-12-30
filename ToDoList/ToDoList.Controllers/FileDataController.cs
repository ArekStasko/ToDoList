using ToDoList.DataAccess;
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

        private IEnumerable<Item> GetItemsByCategory()
        {
            Console.WriteLine("Provide items category to find");
            string? category;
            Console.Clear();

            do
            {
                category = Console.ReadLine();
            } 
            while (string.IsNullOrEmpty(category));

            return GetItemsByCategory(category);            

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
                            IEnumerable<Item> items = dataProvider.GetItems();

                            var showProvider = new ShowProvider();
                            showProvider.PrintManyItems(items);
                            break;
                        }
                    case 2:
                        {
                            optionsProvider.PrintItemSearchOptions();
                            int selectedSearchItemOption = GetUserSelection();
                            var showProvider = new ShowProvider();


                            if (selectedSearchItemOption == 1)
                            {
                                Item searchedItem = GetItemByID("Provide item ID to find");
                                showProvider.PrintItem(searchedItem);
                            }
                            else if (selectedSearchItemOption == 2)
                            {
                                IEnumerable<Item> searchedItems = GetItemsByCategory();
                                showProvider.PrintManyItems(searchedItems);
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
