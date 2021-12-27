using NUnit.Framework;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess.Tests.Unit
{
    [TestFixture]
    public class FileDataProviderTests
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            var dataProvider = new FileDataProvider();

            var allItems = dataProvider.GetItems().ToList();
            foreach (var item in allItems)
            {
                dataProvider.RemoveItem(item);
            }

            var allCategories = dataProvider.GetCategories().ToList();
            foreach (var category in allCategories)
            {
                dataProvider.RemoveCategory(category);
            }
        }

        [Test]
        public void AddItem_Should_Work()
        {
            var dataProvider = new FileDataProvider();
            var itemId = 1;
            var category = "TestCategory";
            var desc = "TestDescription";
            var itemName = "TestItemName";

            var newItem = new Item()
            {
                ItemId = itemId,
                ItemCategory = category,
                ItemDescription = desc,
                ItemName = itemName
            };

            dataProvider.AddItem(newItem);

            var itemsFromFile = dataProvider.GetItems().ToList();
            itemsFromFile.Should().ContainSingle(x => x.ItemId == itemId);
            var itemToAssert = itemsFromFile.Single(x => x.ItemId == itemId);
            itemToAssert.ItemCategory.Should().Be(category);
            itemToAssert.ItemDescription.Should().Be(desc);
            itemToAssert.ItemName.Should().Be(itemName);
        }

        [Test]
        public void RemoveItem_Should_Work()
        {
            var dataProvider = new FileDataProvider();
            var itemId = 2;
            var category = "TestCategory";
            var desc = "TestDescription";
            var itemName = "TestItemName";

            var newItem = new Item()
            {
                ItemId = itemId,
                ItemCategory = category,
                ItemDescription = desc,
                ItemName = itemName
            };

            dataProvider.AddItem(newItem);

            dataProvider.RemoveItem(newItem);
            var itemsFromFile = dataProvider.GetItems().ToList();
            itemsFromFile.Should().NotContain(newItem);
        }

        [Test]
        public void AddCategory_Should_Work()
        {
            var dataProvider = new FileDataProvider();
            var categoryName = "TestCategoryName";

            dataProvider.AddCategory(categoryName);

            var categoriesFromFile = dataProvider.GetCategories().ToList();
            categoriesFromFile.Should().ContainSingle(category => category == categoryName);
        }

        [Test]
        public void RemoveCategory_Should_Work()
        {
            var dataProvider = new FileDataProvider();
            var categoryName = "TestCategoryName";

            dataProvider.AddCategory(categoryName);
            dataProvider.RemoveCategory(categoryName);

            var categoriesFromFile = dataProvider.GetCategories().ToList();
            categoriesFromFile.Should().NotContain(categoryName);
        }

        [Test]
        public void RemoveItems_ShouldRemove_manyItems()
        {
            var dataProvider = new FileDataProvider();
            List<string> testItemNames = new List<string>() { "testName1", "testName2", "testName3", "testName4" };
            List<string> testItemDesc = new List<string>() { "testDesc1", "testDesc2", "testDesc3", "testDesc4" };
            List<string> testItemCategories = new List<string>() { "testCategory", "testCategory", "testCategory", "testCategory" };
            List<int> testIDs = new List<int>() { 123, 456, 789, 321 };

            for (int i = 0; i < testItemNames.Count; i++)
            {
                var newItem = new Item()
                {
                    ItemName = testItemNames[i],
                    ItemDescription = testItemDesc[i],
                    ItemCategory = testItemCategories[i],
                    ItemId = testIDs[i],
                };

                dataProvider.AddItem(newItem);
            }

            var itemsToFind = dataProvider.GetItems().Where(item => item.ItemCategory == "testCategory");

            dataProvider.RemoveItems(itemsToFind.ToList());
            var itemsFromFile = dataProvider.GetItems().ToList();
            itemsFromFile.Should().BeEmpty();
        }
    }
}