using CAPSTONEPROJECT.Ultils;

using Newtonsoft.Json;

using System;


namespace CAPSTONEPROJECT.DataModels.CheckDataModel
{
    public class CheckResponseModel
    {
        public string EmployeeID { get; set; }
        public string ProfileImage { get; set; }
        public string FullName { get; set; }

        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy\"T:00:00:00\"")]
        public DateTime? Birthday { get; set; }

        public int CheckTimeHours { get; set; }
        public double CheckTimeMin { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime? WorkingDate { get; set; }
        public string WorkplaceID { get; set; }
        public string Address { get; set; }


    }
}
