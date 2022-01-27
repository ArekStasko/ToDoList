using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Controllers.Categories
{
    public interface ICategoriesControllers
    {
        public IEnumerable<string> GetCategories();
        public string GetCategory();
        public void AddNewCategory();
        public void DeleteCategory();

    }
}
