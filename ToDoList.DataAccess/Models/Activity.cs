
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
        public bool IsActive { get => DeadlineDate > DateTime.Now;  }
        private TimeSpan TimeExceeded { get; set; }

        private bool _isDone = false;
        public bool IsDone { get => _isDone; 
            set
            {
                _isDone = value;
                TimeExceeded = DeadlineDate - DateTime.Now;
            }
        } 

        public string GetTimeToDeadline()
        {
            if (!_isDone)
            {
                TimeSpan dateDifference = (DeadlineDate - DateTime.Now);
                return $"Time To Deadline: {dateDifference.Days} Days  {dateDifference.Hours} Hours  {dateDifference.Minutes} Minutes";
            } 
            else
            {
                return $"Exceeded time: {TimeExceeded.Days} Days  {TimeExceeded.Hours} Hours  {TimeExceeded.Minutes} Minutes";
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