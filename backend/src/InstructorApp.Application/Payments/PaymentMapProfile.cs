using AutoMapper;
using InstructorApp.Entities;
using InstructorApp.Payments.Dto;

namespace InstructorApp.Payments
{
    public class PaymentMapProfile : Profile
    {
        public PaymentMapProfile()
        {
            CreateMap<Payment, PaymentDto>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<PaymentDto, Payment>();
        }
    }
}
