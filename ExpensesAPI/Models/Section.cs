namespace ExpensesAPI.Models
{
    public class Section
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public ICollection<Purchase> Purchases { get; set; }    
    }
}
