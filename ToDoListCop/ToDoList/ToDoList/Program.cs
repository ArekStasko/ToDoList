using System;
using ToDoList.DataControllers;

namespace ToDoList
{
    public class Program
    {
        static void Main(string[] args)
        {

            var dataController = new FileDataController();

            while (true)
            {
                dataController.ChooseMainOption();
            }

        }
    }
}