
namespace ToDoList.Controllers.Categories
{
    internal interface ICategoriesControllers
    {
        public string GetCategory();
        public void AddNewCategory();
        public void DeleteCategory();
    }
}
