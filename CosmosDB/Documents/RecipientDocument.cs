using Newtonsoft.Json;

namespace CosmosDB.Documents
{
    public class RecipientDocument: BaseDocument
    {
        [JsonProperty(PropertyName = "phonenumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "timezone")]
        public string TimeZone { get; set; }
        [JsonProperty(PropertyName = "timezoneCode")]
        public string TimeZoneCode { get; set; }
        [JsonProperty(PropertyName = "isDaylightSavings")]
        public bool IsDaylightSavings { get; set; }
        [JsonProperty(PropertyName = "utcOffset")]
        public sbyte UtcOffset { get; set; }

        public RecipientDocument(string phoneNumber, string timeZone, string timezoneCode, bool isDaylightSavings, sbyte utcOffset )
            => (PhoneNumber, TimeZone, TimeZoneCode, IsDaylightSavings, UtcOffset)  = (phoneNumber, timeZone, timezoneCode, isDaylightSavings, utcOffset);

        public RecipientDocument() { }
    }
}
