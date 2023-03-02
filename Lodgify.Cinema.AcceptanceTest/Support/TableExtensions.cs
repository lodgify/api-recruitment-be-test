using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace Lodgify.Cinema.AcceptanceTest.Core
{
    public static class TableExtensions
    {
        public const string NULL_VALUE = "null";

        public static T ToObject<T>(this Table table, int rowIndex = 0) where T : class, new()
        {
            if (table == null || table.RowCount == 0)
                return default(T);

            var returnObject = new T();

            foreach (var prop in typeof(T).GetProperties())
            {
                try
                {
                    var value = table.Rows[rowIndex][prop.Name];
                    SetProperty(returnObject, prop, value);
                }
                catch (System.IndexOutOfRangeException)
                {
                    SetProperty(returnObject, prop, default);
                }
            }

            return returnObject;
        }

        private static void SetProperty<T>(T obj, PropertyInfo prop, object value) where T : class, new()
        {
            if (obj != null && value != null)
            {
                string stringValue = value.ToString().ToLower();

                if (stringValue == "true" || stringValue == "false")
                    prop.SetValue(obj, bool.Parse(stringValue));
                else if (Guid.TryParse(stringValue, out Guid guidresult))
                {
                    prop.SetValue(obj, guidresult);
                }
                else if (string.Compare(stringValue, NULL_VALUE, true) == 0)
                    prop.SetValue(obj, null);
                else if (stringValue.ToLower() == "yesterday")
                    prop.SetValue(obj, DateTime.Now.AddDays(-1));
                else if (stringValue.ToLower() == "today")
                    prop.SetValue(obj, DateTime.Now);
                else if (stringValue.ToLower() == "tomorrow")
                    prop.SetValue(obj, DateTime.Now.AddDays(+1));
                else if (new Regex(@"^\d$").IsMatch(stringValue))
                    prop.SetValue(obj, Convert.ToInt32(stringValue));
                else if (int.TryParse(stringValue, out int number))
                    prop.SetValue(obj, Convert.ToInt32(stringValue));
                else
                    prop.SetValue(obj, value);
            }
            else
                prop.SetValue(obj, value);
        }

        public static IEnumerable<T> ToObjectList<T>(this Table table) where T : class, new()
        {
            for (int i = 0; i < table.RowCount; i++)
                yield return table.ToObject<T>(i);
        }
    }
}
