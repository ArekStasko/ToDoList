using System.Collections.Generic;

namespace ToDoList.DataAccess.Models
{
    public class Item
    {
        public string ItemName { get; set; }
        public string ItemCategory { get; set; }
        public string ItemDescription { get; set; }
        public int ItemId { get; set; }

        public string[] ConvertToDataRow()
        {
            return new[] { ItemId.ToString(), ItemCategory, ItemName, ItemDescription };
        }

        protected bool Equals(Item other)
        {
            return ItemId == other.ItemId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((Item)obj);
        }

        public override int GetHashCode()
        {
            return ItemId;
        }
    }
}