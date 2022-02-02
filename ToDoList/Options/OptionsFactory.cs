namespace ToDoList
{
    public static class OptionsFactory
    {
        public static ICategoryOptions GetCatOptionsInstance()
        {
            return new CategoryOptions();
        }

        public static IActivityOptions GetActOptionsInstance()
        {
            return new ActivityOptions();
        }

        public static IOptionsPrinter GetOptionsPrinterInstance()
        {
            return new OptionsPrinter();
        }
    }
}
