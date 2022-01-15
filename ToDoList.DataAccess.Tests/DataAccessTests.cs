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
            var dataProvider = new FileDataProvider();

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
            var dataProvider = new FileDataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var startDate = new DateTime(2015, 05, 20, 05, 50, 0);
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var newActivity = new Activity()
            {
                ActivityID = activityID,
                ActivityCategory = category,
                ActivityDescription = desc,
                ActivityName = activityName,
                StartDate = startDate,
                DeadlineDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);

            var activitiesFromFile = dataProvider.GetActivities().ToList();
            activitiesFromFile.Should().ContainSingle(x => x.ActivityID == activityID);
            var itemToAssert = activitiesFromFile.Single(x => x.ActivityID == activityID);
            itemToAssert.ActivityCategory.Should().Be(category);
            itemToAssert.ActivityDescription.Should().Be(desc);
            itemToAssert.ActivityName.Should().Be(activityName);
            itemToAssert.StartDate.Should().Be(startDate);
            itemToAssert.DeadlineDate.Should().Be(deadlineDate);
        }

        [Test]
        public void UpdateActivity_Should_Work()
        {
            var dataProvider = new FileDataProvider();
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var startDate = new DateTime(2015, 05, 20, 05, 50, 0);
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var newActivity = new Activity()
            {
                ActivityID = activityID,
                ActivityCategory = category,
                ActivityDescription = desc,
                ActivityName = activityName,
                StartDate = startDate,
                DeadlineDate = deadlineDate
            };

            dataProvider.AddActivity(newActivity);
            var activitiesFromFile = dataProvider.GetActivities().ToList();
            var activityToAssert = activitiesFromFile.Single(x => x.ActivityID == activityID);
            activityToAssert.IsDone = true;

            dataProvider.UpdateActivity(activityToAssert);

            activitiesFromFile = dataProvider.GetActivities().ToList();
            activityToAssert = activitiesFromFile.Single(x => x.ActivityID == activityID);
            activityToAssert.IsDone.Should().BeTrue();            
        }

        [Test]
        public void RemoveActivity_Should_Work()
        {
            var dataProvider = new FileDataProvider();
            var activityID = 2;
            var category = "TestCategory";
            var desc = "TestDescription";
            var activityName = "TestItemName";

            var newActivity = new Activity()
            {
                ActivityID = activityID,
                ActivityCategory = category,
                ActivityDescription = desc,
                ActivityName = activityName
            };

            dataProvider.AddActivity(newActivity);

            dataProvider.RemoveActivity(newActivity);
            var activitiesFromFile = dataProvider.GetActivities().ToList();
            activitiesFromFile.Should().NotContain(newActivity);
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
        public void RemoveActivities_ShouldRemove_manyActivities()
        {
            var dataProvider = new FileDataProvider();
            string[] activitiesNames = new string[] { "testName1", "testName2", "testName3", "testName4" };
            string[] activitiesDesc = new string[] { "testDesc1", "testDesc2", "testDesc3", "testDesc4" };
            string[] activitiesCategories = new string[] { "testCategory", "testCategory", "testCategory", "testCategory" };

            for (int i = 0; i < activitiesNames.Length; i++)
            {
                var newActivity = new Activity()
                {
                    ActivityName = activitiesNames[i],
                    ActivityDescription = activitiesDesc[i],
                    ActivityCategory = activitiesCategories[i],
                    ActivityID = i,
                };

                dataProvider.AddActivity(newActivity);
            }

            var activitiesToFind = dataProvider.GetActivities().Where(activity => activity.ActivityCategory == "testCategory");

            dataProvider.RemoveActivity(activitiesToFind.ToList());
            var activitiesFromFile = dataProvider.GetActivities().ToList();
            activitiesFromFile.Should().BeEmpty();
        }

    }
}