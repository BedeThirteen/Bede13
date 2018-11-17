namespace BedeThirteen.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BedeThirteen.Data.Models.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser, IEditable, IDeletable
    {
        public User()
        {
            this.CreditCards = new HashSet<CreditCard>();
        }

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
