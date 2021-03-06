﻿using System.ComponentModel.DataAnnotations;

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

        [Required]
        public int ArchiveKey { get; set; }
    }
}
