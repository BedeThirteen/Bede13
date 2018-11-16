namespace BedeThirteen.Data.Models.Contracts
{
    using System;

    public interface IEditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
