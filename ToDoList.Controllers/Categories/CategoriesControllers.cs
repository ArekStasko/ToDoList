using ToDoList.DataAccess;
using ToDoList.DataAccess.Models;

namespace ToDoList.Controllers.Categories
{
    public class CategoriesControllers : FileDataController, ICategoriesControllers
    {
        private readonly IView view;

        public CategoriesControllers(IView _view) : base(_view) => view = _view;

        public IEnumerable<string> GetCategories()
        {
            var dataProvider = new FileDataProvider();
            return dataProvider.GetCategories();
        }

        public string GetCategory()
        {
            view.DisplayMessage("- Please choose number of category -");
            var dataProvider = new FileDataProvider();
            var categories = dataProvider.GetCategories();

            view.PrintCategories(categories);
            string? category = view.GetData();
            int categoryNumber;

            while (!Int32.TryParse(category, out categoryNumber) || string.IsNullOrEmpty(category))
            {
                view.DisplayMessage("- Please provide number of category -");
                category = view.GetData();
            };

            return categories.ElementAt(categoryNumber - 1);
        }

        public void AddNewCategory()
        {
            string? categoryName = view.GetData();
            while (String.IsNullOrEmpty(categoryName))
            {
                view.DisplayMessage("-Category can't be empty-");
                categoryName = view.GetData();
            }

            var dataProvider = new FileDataProvider();
            dataProvider.AddCategory(categoryName);

            view.DisplayMessage("Successfully added new category");
        }

        public void DeleteCategory()
        {
            var dataProvider = new FileDataProvider();
            var categoryToDelete = GetCategory();
            var activities = dataProvider.GetActivities();

            if (activities.Any(activity => activity.ActivityCategory == categoryToDelete))
            {
                activities = GetActivitiesByCategory(categoryToDelete);
                dataProvider.RemoveActivity(activities.ToList());
            }

            dataProvider.RemoveCategory(categoryToDelete);
            view.DisplayMessage("Successfully deleted category");
        }
    }
}
