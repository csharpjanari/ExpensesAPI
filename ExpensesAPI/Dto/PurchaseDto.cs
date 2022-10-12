namespace ExpensesAPI.Dto
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public string NameOfSection { get; set; } = string.Empty;
        [MaxLength(50)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public double HowMany { get; set; }
        public string Сurrency { get; set; } = string.Empty;
        public DateTime WasBought { get; set; }
        [Required]
        public int SectionId { get; set; }
    }
}
