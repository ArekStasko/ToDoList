using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Categories
{
    public class CategoriesControllers : DataController
    {
        public CategoriesControllers(IView _view) : base(_view) { }

        public IEnumerable<string> GetCategories()
        {
            var dataProvider = new DataProvider();
            return dataProvider.GetCategories();
        }

        public string GetCategory()
        {
            _view.DisplayMessage("- Please choose number of category -");
            var dataProvider = new DataProvider();
            var categories = dataProvider.GetCategories();

            _view.PrintCategories(categories);
            string? category = _view.GetData();
            int categoryNumber;

            while (!Int32.TryParse(category, out categoryNumber) || string.IsNullOrEmpty(category))
            {
                _view.DisplayMessage("- Please provide number of category -");
                category = _view.GetData();
            };

            return categories.ElementAt(categoryNumber - 1);
        }

        public void AddNewCategory()
        {
            string? categoryName = _view.GetStringValue();

            var dataProvider = new DataProvider();
            dataProvider.AddCategory(categoryName);

            _view.DisplayMessage("Successfully added new category");
        }

        public void DeleteCategory()
        {
            var dataProvider = new DataProvider();
            var categoryToDelete = GetCategory();
            dataProvider.RemoveCategory(categoryToDelete);
            _view.DisplayMessage("Successfully deleted category");
        }
    }
}
