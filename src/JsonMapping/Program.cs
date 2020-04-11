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
            foreach (var token in mappingArray)
            {
                var target = token.SelectToken("$.target").Value<string>();
                var src = token.SelectToken("$.src").Value<string>();
                var srcFields = token.SelectToken("$.fields")?.Value<string>();
              
                if (srcFields != null)
                {
                    var jsonSrcFields = jsonData.SelectTokens(src);
                    JArray addtionalItem = new JArray(); 
                    foreach (var jsonSrcField in jsonSrcFields)
                    {
                        if (jsonSrcField.Type == JTokenType.Array)
                        {
                            var jsonArraySrcFields = (JArray)jsonSrcField;
                            foreach (var jObject in jsonArraySrcFields)
                            {
                                var jObj = (JObject)jObject;
                                var targetObject = new JObject();
                                addtionalItem.Add(targetObject);
                                foreach (var srcField in srcFields.Split(','))
                                {
                                    targetObject.Add(jObj.Property(srcField));
                                }
                            }
                            ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, addtionalItem);

                        }
                        else
                        {
                            var srcJObject = (JObject)jsonSrcField;
                            var targetObject = new JObject();
                            foreach (var srcField in srcFields.Split(','))
                            {
                                targetObject.Add(srcJObject.Property(srcField));
                            }
                            ((IDictionary<string, object>)additionalDataDynamicObject).Add(target, targetObject);
                        }
                       
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
    }
}
