using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

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
            // loop over the mapping file
            foreach (var token in mappingArray)
            {
                // get the mapping details
                var target = token.SelectToken("$.target").Value<string>();
                var src = token.SelectToken("$.src").Value<string>();
                var srcFields = token.SelectToken("$.fields")?.Value<string>();
              
                if (srcFields != null)
                {
                    var jsonSrcData = jsonData.SelectToken(src);
                    if (jsonSrcData.Type == JTokenType.Array)
                    {
                        var jsonArrayData = (JArray)jsonSrcData;
                        ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, GetTargetJArray(jsonArrayData, srcFields));

                    }
                    else
                    {
                        var srcJObject = (JObject)jsonSrcData;
                        ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, GetTargetJObject(srcJObject, srcFields));
                    }
                }
                else
                {
                     ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, jsonData.SelectToken(src));
                }
            }

            employee.AdditionalData = additionalDataDynamicObject;

            var jsonStringReturned = JsonConvert.SerializeObject(employee);
        }

        public static JArray GetTargetJArray(JArray jsonArrayData, string fields)
        {
            var additionalItem = new JArray();
            foreach (var dataObject in jsonArrayData)
            {
                additionalItem.Add(GetTargetJObject((JObject)dataObject, fields));
               
            }
            return additionalItem;
        }

        public static JObject GetTargetJObject(JObject src, string srcFields)
        {
            var targetObject = new JObject();
            foreach (var srcField in srcFields.Split(','))
            {
                targetObject.Add(src.Property(srcField));
            }
            return targetObject;
        }
    }
}
