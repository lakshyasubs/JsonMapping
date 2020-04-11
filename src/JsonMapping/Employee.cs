using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMapping
{
    public class Employee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeptId { get; set; }
        public dynamic AdditionalData { get; set; }
    }
}


/*
 * 
 * Expected output
 * 
 *{
	"Id": "q2334",
	"Name": "John Tudor",
	"DeptId": "D1010",
	"AdditionalData": {
		"name": "John Tudor",
		"deptNo": "D1010",
		"projects": [{
				"id": "prj01",
				"name": "Project 1",
				"manager": "Manager 1"
			}, {
				"id": "prj02",
				"name": "Project 2",
				"manager": "Manager 2"
			}
		],
		"transport": {
			"vehicleType": "car",
			"vehicleNumber": "ABC123"
		},
		"transport1": [{
				"vehicleType": "car",
				"vehicleNumber": "ABC123"
			}, {
				"vehicleType": "bike",
				"vehicleNumber": "biek123"
			}
		],
		"attributes_x_access_id": "ADS7889DSD"
	}
}


 * 
 * 
 * 
 */
