namespace ExpensesAPI.Dto
{
    public class SectionDto
    {
        public int Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; } = string.Empty;
    }
}
