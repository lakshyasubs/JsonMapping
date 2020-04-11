using System;
using System.Collections.Generic;
using System.Text;

namespace JsonMapping
{
    public static class AdditionalDataMapping
    {
        public static string Mapping = @"[{
						'target': 'name',
						'src': '$.employeeName',


					}, {
						'target': 'deptNo',
						'src': '$.dept.id',
					}, {
						'target': 'projects',
						'src': '$.projects',
					},
					{
						'target': 'transport',
						'src': '$.transport',
						'fields':'vehicleType,vehicleNumber'
					},
{
						'target': 'transport1',
						'src': '$.transport1',
						'fields':'vehicleType,vehicleNumber'
					},
					{
						'target': 'attributes_x_access_id',
						'src': '$.x_access_id',
					}
				]
				";
    }
}
