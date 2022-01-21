
namespace ToDoList
{
    public class OptionsPrinter
    {
        private void printOptions(string[] options)
        {
            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
        }
        
        public void PrintMainOptions()
        {
            string[] mainOptions =
            { 
                "Show all activities", 
                "Show specific activities", 
                "Manage Activities", 
                "Manage Categories",
                "Close ToDo list" 
            };
            Console.WriteLine("Please select one option :");
            printOptions(mainOptions);
        }

        public void PrintActivitiesOptions()
        {
            Console.Clear();
            string[] activitiesOptions = 
            {
                "Add new activity",
                "Edit activity",
                "Start activity",
                "Set activity as done",
                "Show all done activities",
                "Delete activity"
            };
            Console.WriteLine("Please select one option :");
            printOptions(activitiesOptions);
        }

        public void PrintEditActivityOptions()
        {
            string[] editOptions = 
            {
                "Edit Activity Name",
                "Edit Activity Category",
                "Edit Activity Description",
                "Edit Deadline Date",
                "Close edition"
            };
            Console.WriteLine("What you want to change ?");
            printOptions(editOptions);
        }

        public void PrintCategoriesOptions()
        {
            Console.Clear();
            string[] editionOptions = 
            {
                "Add new category",
                "Delete category"
            };

            Console.WriteLine("Please select one option: ");
            printOptions(editionOptions);
        }

        public void PrintActivitySearchOptions()
        {
            Console.Clear();
            string[] activityOptions = 
            { 
                "Get single activity from ID",
                "Get current activities",
                "Get activities out of date",
                "Get activities with specific category" 
            };

            Console.WriteLine("Please select one item search option :");
            printOptions(activityOptions);
        }

        public void PrintAddActivityOptions()
        {
            string[] activityOptions =  
            {
                "Activity ID",
                "Activity Name", 
                "Activity Description",
                "Activity Category",
                "Start date of activity",
                "End date of activity"
            };
            Console.WriteLine("Provide data :");
            printOptions(activityOptions);
        }

    }
}