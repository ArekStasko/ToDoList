
namespace ToDoList.Views
{
    public class Options : IOptions
    {
        private void printOptions(string[] options, string msg)
        {
            Console.WriteLine(msg);
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
        }

        public void PrintMainOptions()
        {
            string[] mainOptions = new string[] { 
                "Show all items", 
                "Show specific item", 
                "Edit items", 
                "Close ToDo list" 
            };

            string msg = "Please select one option :";
            printOptions(mainOptions, msg);
        }

        public void PrintEditionOptions()
        {
            Console.Clear();
            string[] editionOptions = new string[] { 
                "Add new Category", 
                "Add new item", 
                "Delete item", 
                "Delete category" 
            };

            string msg = "Please select one edition option :";
            printOptions(editionOptions, msg);
        }

        public void PrintItemSearchOptions()
        {
            Console.Clear();
            string[] itemOptions = new string[] { 
                "Get single item from ID", 
                "Get items with specific category" 
            };

            string msg = "Please select one item search option :";
            printOptions(itemOptions, msg);
        }

    }
}