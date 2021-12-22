using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess
{
    public class FileDataProvider : IDataProvider
    {
        private const string categoriesFilePath = @".\categories.txt";
        private const string itemsFilePath = @".\items.txt";
        private const string separator = "|";

        private void InitializeItemsFile()
        {
            if (!File.Exists(itemsFilePath))
            {
                using (File.Create(itemsFilePath))
                {

                }
            }
        }

        private void InitializeCategoriesFile()
        {
            if (!File.Exists(categoriesFilePath))
            {
                using (File.Create(categoriesFilePath))
                {

                }
            }
        }

        public IEnumerable<Item> GetItems()
        {
            InitializeItemsFile();
            foreach (string line in File.ReadLines(itemsFilePath))
            {
                if (!String.IsNullOrWhiteSpace(line))
                {
                    string[] data = line.Split(separator.ToCharArray());
                    var newItem = new Item()
                    {
                        ItemId = Int32.Parse(data[0]),
                        ItemCategory = data[1],
                        ItemName = data[2],
                        ItemDescription = data[3],
                    };
                    yield return newItem;
                }
            }
        }

        public IEnumerable<string> GetCategories()
        {
            InitializeCategoriesFile();
            foreach (string line in File.ReadLines(categoriesFilePath))
                if (!String.IsNullOrWhiteSpace(line))
                {
                    yield return line;
                }
        }

        public void AddCategory(string newCategory)
        {
            InitializeCategoriesFile();
            File.AppendAllText(categoriesFilePath, newCategory + Environment.NewLine);
        }

        public void RemoveCategory(string categoryToRemove)
        {
            InitializeCategoriesFile();
            var categories = GetCategories().ToList();
            categories.Remove(categoryToRemove);
            File.WriteAllText(categoriesFilePath, string.Empty);
            foreach (var category in categories)
            {
                AddCategory(category);
            }
        }

        public void AddItem(Item newItem)
        {
            InitializeItemsFile();
            string line = string.Join(separator, newItem.ConvertToDataRow());
            File.AppendAllText(itemsFilePath, line + Environment.NewLine);
        }

        public void AddItems(List<Item> newItems)
        {
            foreach (var item in newItems)
                AddItem(item);
        }

        public void RemoveItem(Item itemToRemove)
        {
            InitializeItemsFile();
            var items = GetItems().ToList();
            items.Remove(itemToRemove);
            File.WriteAllText(itemsFilePath, string.Empty);
            AddItems(items);
        }

        public void RemoveItems(List<Item> newItems)
        {
            foreach (var item in newItems)
                RemoveItem(item);
        }

    }
}
