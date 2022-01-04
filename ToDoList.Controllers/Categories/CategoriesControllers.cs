using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;
using ToDoList.Views;


namespace ToDoList.Controllers.Categories
{
    internal class CategoriesControllers : FileDataController, ICategoriesControllers
    {

        public string GetCategory()
        {
            Console.WriteLine("- Please choose number of category -");
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();

            var showProvider = new ShowProvider();
            showProvider.PrintCategories(categories);
            string? category = Console.ReadLine();
            int categoryNumber;

            while (!Int32.TryParse(category, out categoryNumber) || string.IsNullOrEmpty(category))
            {
                Console.WriteLine("- Please provide number of category -");
                category = Console.ReadLine();
            };

            return categories.ElementAt(categoryNumber - 1);
        }

        public void AddNewCategory()
        {
            Console.WriteLine("- Please insert new category name -");
            string? categoryName = Console.ReadLine();
            while (String.IsNullOrEmpty(categoryName))
            {
                Console.WriteLine("-Category can't be empty-");
                categoryName = Console.ReadLine();
            }

            var dataProvider = new FileDataProvider();
            dataProvider.AddCategory(categoryName);

            var showProvider = new ShowProvider();
            showProvider.DisplayMessage("Successfully added new category");
        }

        public void DeleteCategory()
        {
            Console.WriteLine("Please provide the category name to delete");
            Console.WriteLine("- This will delete all items with this category -");

            var showProvider = new ShowProvider();
            var dataProvider = new FileDataProvider();

            var categoryToDelete = GetCategory();

            IEnumerable<Activity> ActivitiesToDelete = GetActivitiesByCategory(categoryToDelete);

            if (ActivitiesToDelete.Any())
            {
                dataProvider.RemoveActivity(ActivitiesToDelete.ToList());

            }
            dataProvider.RemoveCategory(categoryToDelete);
            showProvider.DisplayMessage("Successfully deleted category");
        }
    }
}
