
namespace ToDoList
{
    public interface IOptions
    {

        void PrintMainOptions();
        void PrintActivitiesOptions();
        void PrintCategoriesOptions();
        void PrintActivitySearchOptions();
        public void PrintEditActivityOptions();
        public void printOptions(string[] options, string msg);
    }
}
