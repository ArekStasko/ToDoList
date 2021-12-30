﻿
namespace ToDoList.DataAccess.Models
{
    public class Activity
    {
        public string ActivityName { get; set; } = "noneName";
        public string ActivityCategory { get; set; } = "noneCategory";
        public string ActivityDescription { get; set; } = "noneDescription";
        public int ActivityID { get; set; }

        public string[] ConvertToDataRow()
        {
            return new[] { ActivityID.ToString(), ActivityCategory,ActivityName, ActivityDescription };
        }

        protected bool Equals(Activity other)
        {
            return ActivityID == other.ActivityID;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((Activity)obj);
        }

        public override int GetHashCode()
        {
            return ActivityID;
        }
    }
}