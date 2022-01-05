
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
        public bool IsActive { get { return DeadlineDate > DateTime.Now; } }
        public bool IsDone { get; set; } = false;
        
        public string GetTimeToDeadline()
        {
            if (!IsDone)
            {
                TimeSpan dateDifference = (DeadlineDate - DateTime.Now);
                return $"{dateDifference.Days} Days  {dateDifference.Hours} Hours  {dateDifference.Minutes} Minutes";
            } 
            else
            {
                return "This activity is done";
            }

        }

        public string[] ConvertToDataRow()
        {
            return new[] {
                ActivityID.ToString(),
                ActivityCategory,ActivityName,
                ActivityDescription,
                StartDate.ToString("MM/dd/yyyy HH:mm"),
                DeadlineDate.ToString("MM/dd/yyyy HH:mm"),
                IsDone ? "1" : "0"
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