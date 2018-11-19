namespace BedeThirteen.Data.Models.Abstract
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using BedeThirteen.Data.Models.Contracts;

    public class Entity : IEditable, IDeletable
    { 
        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]

        public DateTime? DeletedOn { get; set; }

        [DataType(DataType.DateTime)]

        public DateTime? CreatedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }
    }
}
