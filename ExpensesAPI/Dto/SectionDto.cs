namespace ExpensesAPI.Dto
{
    public class SectionDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
