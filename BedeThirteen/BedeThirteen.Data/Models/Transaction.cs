namespace BedeThirteen.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using BedeThirteen.Data.Models.Abstract;

    public class Transaction : Entity
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [MaxLength(150)]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        public Guid TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
