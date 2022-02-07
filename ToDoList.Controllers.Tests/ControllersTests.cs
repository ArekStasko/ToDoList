using NUnit.Framework;
using FluentAssertions;
using ToDoList.Controllers.Factories;
using System;
using System.Linq;

namespace ToDoList.Controllers.Tests
{
    public class Tests
    {
        [SetUp]
        public void BeforeEveryTest()
        {
            var dataProvider = Factory.NewDataProviderInstance();

            var allActivities = dataProvider.GetActivities().ToList();
            foreach (var activity in allActivities)
            {
                dataProvider.RemoveActivity(activity);
            }
        }

        [Test]
        public void AddActivity_Should_AddNewActivity()
        {
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var activity = Factory.NewActivityInstance();
            activity._id = activityID;
            activity.Category = category;
            activity.Description = desc;
            activity.Title = activityName;
            activity.EndDate = deadlineDate;

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            actControllers.AddNewActivity(activity);
            var activities = actControllers.GetActivities();

            activities.Should().Contain(x => x._id == activity._id);
        }

        [Test]
        public void StartActivity_Should_ChangeActivityToStart()
        {
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var activity = Factory.NewActivityInstance();
            activity._id = activityID;
            activity.Category = category;
            activity.Description = desc;
            activity.Title = activityName;
            activity.EndDate = deadlineDate;

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            activity.IsActive.Should().BeFalse();
            actControllers.AddNewActivity(activity);
            actControllers.StartActivity(activity._id);

            var testActivity = actControllers.GetActivityByID(activity._id);
            testActivity.IsActive.Should().BeTrue();
        }

        [Test]
        public void SetActivityAsDone_Should_SetActivityAsDoneWhenItIsActive()
        {
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var activity = Factory.NewActivityInstance();
            activity._id = activityID;
            activity.Category = category;
            activity.Description = desc;
            activity.Title = activityName;
            activity.EndDate = deadlineDate;

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            activity.IsDone.Should().BeFalse();
            actControllers.AddNewActivity(activity);

            actControllers.StartActivity(activity._id);
            actControllers.SetActivityAsDone(activity._id);

            var testActivity = actControllers.GetActivityByID(activity._id);
            testActivity.IsDone.Should().BeTrue();
        }

        [Test]
        public void SetActivityAsDone_ShouldNot_SetActivityAsDoneWhenItIsInactive()
        {
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var activity = Factory.NewActivityInstance();
            activity._id = activityID;
            activity.Category = category;
            activity.Description = desc;
            activity.Title = activityName;
            activity.EndDate = deadlineDate;

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            activity.IsDone.Should().BeFalse();
            actControllers.AddNewActivity(activity);

            actControllers.SetActivityAsDone(activity._id);

            var testActivity = actControllers.GetActivityByID(activity._id);
            testActivity.IsDone.Should().BeFalse();
        }

        [Test]
        public void EditActivity_Should_EditActivityTitle()
        {
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var activity = Factory.NewActivityInstance();
            activity._id = activityID;
            activity.Category = category;
            activity.Description = desc;
            activity.Title = activityName;
            activity.EndDate = deadlineDate;

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);
            actControllers.AddNewActivity(activity);

            var addedActivity = actControllers.GetActivityByID(activity._id);
            addedActivity.Title = "New Test Title";
            actControllers.EditActivity(addedActivity);
            var testActivity = actControllers.GetActivityByID(activity._id);
            testActivity.Title.Should().Be("New Test Title");
        }

        [Test]
        public void Delete_Should_DeleteOneActivity()
        {
            int activityID = 1;
            string category = "TestCategory";
            string desc = "TestDescription";
            string activityName = "TestItemName";
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var activity = Factory.NewActivityInstance();
            activity._id = activityID;
            activity.Category = category;
            activity.Description = desc;
            activity.Title = activityName;
            activity.EndDate = deadlineDate;

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);
            actControllers.AddNewActivity(activity);
            actControllers.GetActivities().Should().Contain(x => x._id == activity._id);
            actControllers.DeleteActivity(activity._id);
            var testActivities = actControllers.GetActivities();
            testActivities.Should().NotContain(x => x._id == activity._id);
        }

        [Test]
        public void GetInactiveElements_Should_ReturnIncactiveElements()
        {
            string[] activitiesNames = new string[] { "testName1", "testName2", "testName3", "testName4" };
            string[] activitiesDesc = new string[] { "testDesc1", "testDesc2", "testDesc3", "testDesc4" };
            string[] activitiesCategories = new string[] { "testCategory1", "testCategory2", "testCategory3", "testCategory4" };
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            for (int i = 0; i< activitiesNames.Length; i++)
            {
                var activity = Factory.NewActivityInstance();
                activity._id = i;
                activity.Category = activitiesCategories[i];
                activity.Description = activitiesDesc[i];
                activity.Title = activitiesNames[i];
                activity.EndDate = deadlineDate;

                actControllers.AddNewActivity(activity);
            }

            actControllers.StartActivity(0);
            var testActivities = actControllers.GetInactiveActivities();
            testActivities.Should().NotContain(x => x._id == 0);
        }

        [Test]
        public void GetActiveElements_Should_ReturnActiveElements()
        {
            string[] activitiesNames = new string[] { "testName1", "testName2", "testName3", "testName4" };
            string[] activitiesDesc = new string[] { "testDesc1", "testDesc2", "testDesc3", "testDesc4" };
            string[] activitiesCategories = new string[] { "testCategory1", "testCategory2", "testCategory3", "testCategory4" };
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            for (int i = 0; i < activitiesNames.Length; i++)
            {
                var activity = Factory.NewActivityInstance();
                activity._id = i;
                activity.Category = activitiesCategories[i];
                activity.Description = activitiesDesc[i];
                activity.Title = activitiesNames[i];
                activity.EndDate = deadlineDate;

                actControllers.AddNewActivity(activity);
            }

            actControllers.StartActivity(1);
            var testActivities = actControllers.GetActiveActivities();
            testActivities.Should().Contain(x => x.IsActive == true);
        }

        [Test]
        public void GetActivitiesByCategory_Should_ReturnActivitiesByCategory()
        {
            string[] activitiesNames = new string[] { "testName1", "testName2", "testName3", "testName4" };
            string[] activitiesDesc = new string[] { "testDesc1", "testDesc2", "testDesc3", "testDesc4" };
            string[] activitiesCategories = new string[] { "testCategory1", "testCategory", "testCategory", "testCategory4" };
            var deadlineDate = new DateTime(2016, 05, 20, 05, 50, 0);

            var view = new MockView();
            var actControllers = Factory.NewActControllersInstance(view);

            for (int i = 0; i < activitiesNames.Length; i++)
            {
                var activity = Factory.NewActivityInstance();
                activity._id = i;
                activity.Category = activitiesCategories[i];
                activity.Description = activitiesDesc[i];
                activity.Title = activitiesNames[i];
                activity.EndDate = deadlineDate;

                actControllers.AddNewActivity(activity);
            }

            var testActivities = actControllers.GetActivitiesByCategory("testCategory");
            testActivities.Should().Contain(x => x.Category == "testCategory");

        }
    }
}