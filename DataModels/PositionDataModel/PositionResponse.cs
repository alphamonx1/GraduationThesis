using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.DataModels.PositionDataModel
{
    public class PositionResponse
    {
        public string PositionID { get; set; }
        public string PositionName { get; set; }
        public bool? DelFlag { get; set; }
    }
}
