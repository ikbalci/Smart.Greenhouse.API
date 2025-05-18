using System;
using System.ComponentModel.DataAnnotations;

namespace Smart.Greenhouse.API.Core.DTOs
{
    public class ReportRequestDto
    {
        [Required]
        [RegularExpression("daily|weekly|monthly", ErrorMessage = "Timeframe must be 'daily', 'weekly', or 'monthly'")]
        public string? Timeframe { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
    }
} 