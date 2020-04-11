using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMapping
{
    public static class JsonData
    {
        public static string EmployeeJsonData = @"{
					'id': 'A300111',
					'employeeName': 'John Tudor',
					'x_access_id': 'ADS7889DSD',
					'x_grx_yfg': 'goestoAdditonalData',
					'dept': {
						'id': 'D1010',
						'name': 'IT'
					},
					'additionalData':{
						'hasAccessToLevel4':'Yes',
						'eligibleForVouchers':'No',
						
						
					    },
					'transport': { 'vehicleType':'car', 'vehicleNumber':'ABC123', 'vehicleFuel':'Petrol', 'onloan':'true' },
					'transport1': [
									{ 'vehicleType':'car', 'vehicleNumber':'ABC123', 'vehicleFuel':'Petrol', 'onloan':'true' },
									{ 'vehicleType':'bike', 'vehicleNumber':'biek123', 'vehicleFuel':'Petrol', 'onloan':'false' }
								],
					'projects': [{
							'id': 'prj01',
							'name': 'Project 1',
							'manager': 'Manager 1'
							
									
								
						}, {
							'id': 'prj02',
							'name': 'Project 2',
							'manager': 'Manager 2'
						}
					]

				}";
    }
}
