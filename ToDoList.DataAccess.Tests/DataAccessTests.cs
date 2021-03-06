using NUnit.Framework;
using FluentAssertions;
using System.Linq;
using System;
using ToDoList.DataAccess.Models;

namespace ToDoList.DataAccess.Tests.Unit
{
    [TestFixture]
    public class DataAccessTests
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            var dataProvider = new DataProvider();

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
        public void AddActivity_Should_Work()
        {
            var dataProvider = new DataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            IActivity newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName,
                EndDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);

            var activitiesFromFile = dataProvider.GetActivities().ToList();
            activitiesFromFile.Should().ContainSingle(activity => activity._id == activityID);
            var itemToAssert = activitiesFromFile.Single(activity => activity._id == activityID);
            itemToAssert.Category.Should().Be(category);
            itemToAssert.Description.Should().Be(desc);
            itemToAssert.Title.Should().Be(activityName);
            itemToAssert.EndDate.Should().Be(deadlineDate);
        }

        [Test]
        public void UpdateActivity_Should_Work()
        {
            var dataProvider = new DataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            IActivity newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName,
                EndDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);
            var activitiesFromFile = dataProvider.GetActivities().ToList();
            var activityToAssert = activitiesFromFile.Single(activity => activity._id == activityID);
            activityToAssert.IsDone = true;

            dataProvider.UpdateActivity(activityToAssert);

            activitiesFromFile = dataProvider.GetActivities().ToList();
            activityToAssert = activitiesFromFile.Single(activity => activity._id == activityID);
            activityToAssert.IsDone.Should().BeTrue();            
        }

        [Test]
        public void RemoveActivity_Should_Work()
        {
            var dataProvider = new DataProvider();
            var activityID = 2;
            var category = "TestCategory";
            var desc = "TestDescription";
            var activityName = "TestItemName";

            var newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName
            };

            dataProvider.AddActivity(newActivity);

            dataProvider.RemoveActivity(newActivity);
            var activitiesFromFile = dataProvider.GetActivities().ToList();
            activitiesFromFile.Should().NotContain(newActivity);
        }

        [Test]
        public void AddCategory_Should_Work()
        {
            var dataProvider = new DataProvider();
            var categoryName = "TestCategoryName";

            dataProvider.AddCategory(categoryName);

            var categoriesFromFile = dataProvider.GetCategories().ToList();
            categoriesFromFile.Should().ContainSingle(category => category == categoryName);
        }

        [Test]
        public void RemoveCategory_Should_Work()
        {
            var dataProvider = new DataProvider();
            var categoryName = "TestCategoryName";

            dataProvider.AddCategory(categoryName);
            dataProvider.RemoveCategory(categoryName);

            var categoriesFromFile = dataProvider.GetCategories().ToList();
            categoriesFromFile.Should().NotContain(categoryName);
        }

        [Test]
        public void RemoveActivities_ShouldRemove_manyActivities()
        {
            var dataProvider = new DataProvider();
            string[] activitiesNames = new string[] { "testName1", "testName2", "testName3", "testName4" };
            string[] activitiesDesc = new string[] { "testDesc1", "testDesc2", "testDesc3", "testDesc4" };
            string[] activitiesCategories = new string[] { "testCategory", "testCategory", "testCategory", "testCategory" };

            for (int i = 0; i < activitiesNames.Length; i++)
            {
                var newActivity = new Activity()
                {
                    Title = activitiesNames[i],
                    Description = activitiesDesc[i],
                    Category = activitiesCategories[i],
                    _id = i,
                };

                dataProvider.AddActivity(newActivity);
            }

            var activitiesToFind = dataProvider.GetActivities().Where(activity => activity.Category == "testCategory");

            dataProvider.RemoveActivity(activitiesToFind.ToList());
            var activitiesFromFile = dataProvider.GetActivities().ToList();
            activitiesFromFile.Should().BeEmpty();
        }

        [Test]
        public void GetActiveActivities_ShouldReturn_ActiveActivities()
        {
            var dataProvider = new DataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName,
                EndDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);

            var activity = dataProvider.GetActivityByID(activityID);

            activity.IsActive = true;
            dataProvider.UpdateActivity(activity);

            var activities = dataProvider.GetActiveActivities();
            activity = activities.Where(act => act._id == activityID).First();
            activity.IsActive.Should().BeTrue();
        }

        [Test]
        public void GetInactiveActivities_ShouldReturn_InactiveActivities()
        {
            var dataProvider = new DataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var startDate = new DateTime(2015, 05, 20, 05, 50, 0);
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName,
                EndDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);

            var activities = dataProvider.GetInactiveActivities();
            var activity = activities.First(act => act._id == activityID);
            activity.IsActive.Should().BeFalse();
        }

        [Test]
        public void GetActivitiesByID_ShouldReturn_ActivitiesByID()
        {
            var dataProvider = new DataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName,
                EndDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);

            var activity = dataProvider.GetActivityByID(activityID);
            activity.Should().NotBeNull();
        }

        [Test]
        public void GetActivitiesByCategory_ShouldReturn_ActivitiesByCategory()
        {
            var dataProvider = new DataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var newActivity = new Activity()
            {
                _id = activityID,
                Category = category,
                Description = desc,
                Title = activityName,
                EndDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);

            var activities = dataProvider.GetActivitiesByCategory(category);
            activities.Should().Contain(act => act.Category == category);
        }

    }
}