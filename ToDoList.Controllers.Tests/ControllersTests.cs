using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System;
using ToDoList.Controllers.Factories;
using ToDoList.Controllers.Activities;
using ToDoList.Controllers.Categories;

namespace ToDoList.Controllers.Tests
{
    public class ControllersTests
    {
        [SetUp]
        public void Setup()
        {
            IActivitiesControllers dataProvider = Factory.NewActControllersInstance();

            var allActivities = dataProvider.GetActivities().ToList();
            foreach (var activity in allActivities)
            {
                dataProvider.RemoveActivity(activity);
            }

            var allCategories = dataProvider.GetCategories().ToList();
            foreach (var category in allCategories)
            {
                dataProvider.RemoveCategory(category);
            }
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}