using ToDoList.Controllers.Categories;

namespace ToDoList
{
    internal class CategoryOptions : MainOptions
    {
        internal void RunCategoryController()
        {
            var categoriesControllers = new CategoriesControllers(_view);
            var categories = categoriesControllers.GetCategories();

            _options.PrintCategoriesOptions();
            int selectedOption = dataController.GetUserSelection(5);

            switch (selectedOption)
            {
                case 1:
                    {
                        Console.WriteLine("- Please insert new category name -");
                        categoriesControllers.AddNewCategory();
                        break;
                    }
                case 2:
                    {
                        if (categories.Any())
                        {
                            Console.WriteLine("Please provide the category name to delete");
                            Console.WriteLine("- This will delete all items with this category -");
                            categoriesControllers.DeleteCategory();
                            break;
                        }

                        Console.WriteLine("You don't have any categories");
                        break;
                    }
                default:
                    {
                        Console.WriteLine("You provide wrong option number");
                        break;
                    }
            }
        }

    }
}
