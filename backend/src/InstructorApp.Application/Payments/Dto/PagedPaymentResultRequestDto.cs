using System;
using Abp.Application.Services.Dto;

namespace InstructorApp.Payments.Dto
{
    public class PagedPaymentResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }
        public long? MemberId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? Amount { get; set; }
        public int? SessionsPurchased { get; set; }
        public int? PaymentMethod { get; set; }
        public string Notes { get; set; }
    }
}
