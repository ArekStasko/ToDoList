
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
            string[] activitiesOptions = new string[] {
                "Add new activity",
                "Edit activity",
                "Set activity as done",
                "Show all done activities",
                "Delete activity"
            };

            printOptions(activitiesOptions, "Please select one option :");
        }

        public void PrintEditActivityOptions()
        {
            string[] editOptions = new string[]
            {
                "Edit Activity Name",
                "Edit Activity Category",
                "Edit Activity Description",
                "Edit Start Date",
                "Edit Deadline Date",
                "Close edition"
            };

            printOptions(editOptions, "What you want to change ?");
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