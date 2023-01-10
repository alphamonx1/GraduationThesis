

using Newtonsoft.Json.Converters;

namespace CAPSTONEPROJECT.Ultils
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
