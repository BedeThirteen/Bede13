namespace BedeThirteen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BedeThirteen.Data.Models.Contracts;

    public class User : IEditable, IDeletable
    {
        public User()
        {
            this.CreditCards = new HashSet<CreditCard>();
        }

        // temp
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        // temp
        public string UserName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Balance { get; set; }

        [Required]
        public Guid CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]

        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]

        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        public ICollection<CreditCard> CreditCards { get; private set; }
    }
}
