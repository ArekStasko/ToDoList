using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList
{
    public interface IOptionsPrinter
    {
        public void PrintMainOptions();
        public void PrintActivitiesOptions();
        public void PrintEditActivityOptions();
        public void PrintCategoriesOptions();
        public void PrintActivitySearchOptions();
        public void PrintAddActivityOptions();
    }
}
