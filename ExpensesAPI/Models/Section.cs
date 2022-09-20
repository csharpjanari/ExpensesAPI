namespace ExpensesAPI.Models
{
    public class Section
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Purchase> Purchases { get; set; }    
    }
}
