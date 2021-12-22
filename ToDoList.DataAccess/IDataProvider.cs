using System.Collections.Generic;
using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess
{
    public interface IDataProvider
    {
        IEnumerable<Item> GetItems();
        IEnumerable<string> GetCategories();
        void AddCategory(string newCategory);
        void AddItem(Item newItem);
        void AddItems(List<Item> newItems);
        void RemoveItem(Item itemToRemove);
    }
}