using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace InstructorApp.Entities
{
    [Table("Payments")]
    public class Payment : FullAuditedEntity<long>
    {
        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public int SessionsPurchased { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        [MaxLength(500)]
        public string Notes { get; set; }

        public long MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public virtual Member Member { get; set; }

    }
}