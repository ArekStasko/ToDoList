using ToDoList.DataAccess;
using ToDoList.Controllers.Factories;

namespace ToDoList.Controllers.Categories
{
    public class CategoriesControllers  : ICategoriesControllers
    {
        private readonly IDataProvider _provider;
        private readonly IView _view;
        public CategoriesControllers(IView view)
        {
            _provider = Factory.NewDataProviderInstance();
            _view = view;
        } 

        public IEnumerable<string> GetCategories()
        {
            return _provider.GetCategories();
        }

        public string GetCategory()
        {
            _view.DisplayMessage("- Please choose number of category -");
            var categories = _provider.GetCategories();

            _view.PrintCategories(categories);
            int categoryNumber;
            do
            {
                categoryNumber = _view.GetNumericValue();
            } while (categoryNumber >= categories.Count());


            return categories.ElementAt(categoryNumber - 1);     
        }

        public void AddNewCategory()
        {
            string? categoryName = _view.GetStringValue();
            _provider.AddCategory(categoryName);

            _view.DisplayMessage("Successfully added new category");
        }

        public void DeleteCategory()
        {
            var categoryToDelete = GetCategory();
            _provider.RemoveCategory(categoryToDelete);
            _view.DisplayMessage("Successfully deleted category");
        }
    }
}
