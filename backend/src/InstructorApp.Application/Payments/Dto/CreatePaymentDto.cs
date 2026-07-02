using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace InstructorApp.Payments.Dto
{
    [AutoMapTo(typeof(Entities.Payment))]
    public class CreatePaymentDto
    {
        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public int SessionsPurchased { get; set; }

        public int PaymentMethod { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public long MemberId { get; set; }

    }
}