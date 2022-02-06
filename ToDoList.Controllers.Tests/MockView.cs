using System;
using System.Collections.Generic;

namespace ToDoList.Controllers.Tests
{
    public class MockView : IView
    {
        public void RunApp()
        {
            
        }

        protected void PrintActivity(string[] activityData)
        {
            return;
        }

        protected void PrintActivityDesc()
        {
            return;
        }

        public void PrintCategories(IEnumerable<string> categories)
        {
           return;
        }

        public void DisplayMessage(string msg)
        {
           return;
        }

        public void ErrorMessage(string msg)
        {
           return;
        }

        public void ClearView() => Console.Clear();
        public string? GetData() => Console.ReadLine();

        public string GetStringValue()
        {
            string? providedData = GetData();

            while (String.IsNullOrEmpty(providedData))
            {
                DisplayMessage("-You can't add empty data-");
                providedData = GetData();
            }

            return providedData;
        }

        public int GetNumericValue()
        {
            string? NumVal = GetData();
            while (!Int32.TryParse(NumVal, out int n) || string.IsNullOrEmpty(NumVal) || NumVal == "0")
            {
                DisplayMessage("Input must be non 0 number");
                NumVal = GetData();
            }
            return Int32.Parse(NumVal);
        }

        public int GetID()
        {
            DisplayMessage("Provide activity ID");
            return GetNumericValue();
        }

    }
}
