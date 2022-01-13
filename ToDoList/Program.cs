using ToDoList.Controllers;

namespace ToDoList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var view = new ShowProvider();

            var dataController = new FileDataControllerChooser(view);
            dataController.ChooseMainOption();
        }
    }
}