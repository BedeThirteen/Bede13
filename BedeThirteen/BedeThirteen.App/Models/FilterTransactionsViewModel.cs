using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BedeThirteen.App.Models
{
    public class FilterTransactionsViewModel
    {
        [Required]
        public string FilterBy { get; set; }

        public string FilterCriteria { get; set; }

        public string AditionalCriteria { get; set; }

        [Required]
        public int PageSize { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        public string SortBy { get; set; }
    }
}
