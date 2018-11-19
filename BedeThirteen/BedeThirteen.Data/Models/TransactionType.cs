namespace BedeThirteen.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BedeThirteen.Data.Models.Abstract;

    public class TransactionType : GUIDEntity
    {
        public TransactionType()
        {
            this.Transactions = new HashSet<Transaction>();
        }

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
