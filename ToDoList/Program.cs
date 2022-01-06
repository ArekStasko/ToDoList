using ToDoList.Controllers;

namespace ToDoList
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dataController = new FileDataControllerChooser();
            dataController.ChooseMainOption();
        }
    }
}