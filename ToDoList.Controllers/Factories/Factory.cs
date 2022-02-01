using ToDoList.DataAccess.Models;
using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;
using ToDoList.DataAccess;

namespace ToDoList.Controllers.Factories
{
    public static class Factory 
    {
        public static Activity NewActivityInstance()
        {
            return new Activity();
        }

        public static IDataProvider NewDataProviderInstance()
        {
            return new DataProvider();
        }
        
        public static CategoriesControllers NewCatControllersInstance(this IView view)
        {
            return new CategoriesControllers(view);
        }

        public static ActivitiesControllers NewActControllersInstance(this IView view)
        {
            return new ActivitiesControllers(view);
        }
    }
}
