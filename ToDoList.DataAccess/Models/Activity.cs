
namespace ToDoList.DataAccess.Models
{
    public class Activity : IActivity
    {
        public int _id { get; set; }
        public string Title { get; set; } = "none Title";
        public string Category { get; set; } = "none Category";
        public string Description { get; set; } = "none Description";
        public DateTime EndDate { get; set; }
        private TimeSpan ExceededTime { get; set; }
        private bool _isActive = false;
        public bool IsActive { get => _isActive; set => _isActive = value; }
        private bool _isDone = false;
        public bool IsDone { get => _isDone; 
            set
            {
                _isDone = value;
                ExceededTime = EndDate - DateTime.Now;
            }
        } 

        public string GetTimeToDeadline()
        {
            if (_isDone)
                return $"Exceeded time: {ExceededTime.Days} Days  {ExceededTime.Hours} Hours  {ExceededTime.Minutes} Minutes";

            TimeSpan dateDifference = (EndDate - DateTime.Now);
            return $"Time To Deadline: {dateDifference.Days} Days  {dateDifference.Hours} Hours  {dateDifference.Minutes} Minutes";
        }
        
        public string[] ConvertToDataRow() => new[] 
               {
                _id.ToString(),
                Category,
                Title,
                Description,
                EndDate.ToString("MM/dd/yyyy HH:mm"),
                _isActive ? "1" : "0",
                _isDone ? "1" : "0"
               };


        protected bool Equals(Activity other) => _id == other._id;

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

        public override int GetHashCode() => _id;
    }
}