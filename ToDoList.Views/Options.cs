
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
                "Show all activities", 
                "Show specific activities", 
                "Manage Activities", 
                "Manage Categories",
                "Close ToDo list" 
            };
            
            printOptions(mainOptions, "Please select one option :");
        }

        public void PrintActivitiesOptions()
        {
            Console.Clear();
            string[] editionOptions = new string[] {
                "Add new activity",
                "Set activity as done",
                "Show all done activities",
                "Delete activity"
            };

            printOptions(editionOptions, "Please select one option :");
        }

        public void PrintCategoriesOptions()
        {
            Console.Clear();
            string[] editionOptions = new string[] {
                "Add new category",
                "Delete category"
            };

            printOptions(editionOptions, "Please select one option :");
        }

        public void PrintActivitySearchOptions()
        {
            Console.Clear();
            string[] itemOptions = new string[] { 
                "Get single activity from ID",
                "Get current activities",
                "Get activities out of date",
                "Get activities with specific category" 
            };

            string msg = "Please select one item search option :";
            printOptions(itemOptions, msg);
        }

    }
}