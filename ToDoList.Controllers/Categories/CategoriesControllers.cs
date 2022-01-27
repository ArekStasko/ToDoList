using ToDoList.DataAccess;

namespace ToDoList.Controllers.Categories
{
    public class CategoriesControllers  : ICategoriesControllers
    {
        private readonly IView _view;
        public CategoriesControllers(IView view) => _view = view;

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
            int categoryNumber;
            do
            {
                categoryNumber = _view.GetNumericValue();
            } while (categories.ElementAt(categoryNumber - 1) != null);


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
