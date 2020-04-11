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
            JObject jsonData = JObject.Parse(jsonDataString);            

            var employee = new Employee
            {
                Id = "q2334",
                Name = (string)jsonData.SelectToken("$.employeeName"),
                DeptId = (string)jsonData.SelectToken("$.dept.id"),
               
            };

            employee.AdditionalData = GetAdditionalData(jsonData);

            var jsonStringReturned = JsonConvert.SerializeObject(employee);
        }

        public static ExpandoObject GetAdditionalData(JObject jsonData)
        {
            JArray mappingArray = JArray.Parse(AdditionalDataMapping.Mapping);
            var additionalDataDynamicObject = new ExpandoObject();
            foreach (var token in mappingArray)
            {
                var target = token.SelectToken("$.target").Value<string>();
                var src = token.SelectToken("$.src").Value<string>();
                var srcFields = token.SelectToken("$.fields")?.Value<string>();

                ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, MapTargetSourceFields(jsonData, src, srcFields));
            }

            return additionalDataDynamicObject;
        }

        public static object MapTargetSourceFields(JObject jsonData, string src, string fields)
        {
            var jsonSrcData = jsonData.SelectToken(src);
            if (fields == null)
            {
                return jsonSrcData;
            }
          
            if (jsonSrcData.Type == JTokenType.Array)
            {
                var jsonArrayData = (JArray)jsonSrcData;
                return MapTargetSrcJArray(jsonArrayData, fields);

            }
            else
            {
                var srcJObject = (JObject)jsonSrcData;
                return MapTargetSrcJObject(srcJObject, fields);
            }
        }

        public static JArray MapTargetSrcJArray(JArray jsonArrayData, string fields)
        {
            var additionalItem = new JArray();
            foreach (var dataObject in jsonArrayData)
            {
                additionalItem.Add(MapTargetSrcJObject((JObject)dataObject, fields));
               
            }
            return additionalItem;
        }

        public static JObject MapTargetSrcJObject(JObject src, string srcFields)
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
