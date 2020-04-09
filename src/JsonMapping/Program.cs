using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace JsonMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            MapJson();
            Console.WriteLine("Hello World!");
        }

        public static void MapJson()
        {
            var jsonDataString = JsonData.EmployeeJsonData;
            var mappingString = AdditionalDataMapping.Mapping;
            JObject jsonData = JObject.Parse(jsonDataString);
            JArray mappingArray = JArray.Parse(mappingString);

            var employee = new Employee
            {
                Id = "q2334",
                Name = (string)jsonData.SelectToken("$.employeeName"),
                DeptId = (string)jsonData.SelectToken("$.dept.id"),
               
            };

            var additionalDataDynamicObject = new ExpandoObject();
            foreach (var token in mappingArray)
            {
                var target = token.SelectToken("$.target").Value<string>();
                var src = token.SelectToken("$.src").Value<string>();
                ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, jsonData.SelectToken(src));

            }

            employee.AdditionalData = additionalDataDynamicObject;

            var jsonStringReturned = JsonConvert.SerializeObject(employee);
        }
    }
}
