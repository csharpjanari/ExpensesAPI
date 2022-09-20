
namespace ExpensesAPI.Models
{
    public class Purchase
    {
        [Key, JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Сurrency { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTime WasBought { get; set; }
        public Section Sections { get; set; }
        public int SectionId { get; set; }
    }
}
