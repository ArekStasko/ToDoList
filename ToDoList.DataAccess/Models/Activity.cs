
namespace ToDoList.DataAccess.Models
{
    public class Activity
    {
        public string ActivityName { get; set; } = "noneName";
        public string ActivityCategory { get; set; } = "noneCategory";
        public string ActivityDescription { get; set; } = "noneDescription";
        public int ActivityID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DeadlineDate { get; set; }

        public string GetTimeToDeadline()
        {
            string dateDifference = (DeadlineDate - StartDate).ToString();
            string[] dateDifferences = dateDifference.Split('.');
            return dateDifferences.Length > 1 ? 
                $"Days: {dateDifferences[0]}  Time: {dateDifferences[1]}" :
                $"Time: {dateDifferences[0]}";
        }

        public string[] ConvertToDataRow()
        {
            return new[] { 
                ActivityID.ToString(), 
                ActivityCategory,ActivityName, 
                ActivityDescription, 
                StartDate.ToString("MM/dd/yyyy HH:mm"),
                DeadlineDate.ToString("MM/dd/yyyy HH:mm")
            };
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