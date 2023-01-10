using System;

namespace CAPSTONEPROJECT.DataModels.ShiftDataModel
{
    public class ShiftResponseModel
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int StartTimeHours { get; set; }
        public int StartTimeMin { get; set; }
        public int EndTimeHours { get; set; }
        public int EndTimeMin { get; set; }
        public bool? DelFlag { get; set; }


    }
}
