using ToDoList.Controllers;

namespace ToDoList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var optionsProvider = new OptionsProvider();
            optionsProvider.ChooseMainOption();
        }
    }
}