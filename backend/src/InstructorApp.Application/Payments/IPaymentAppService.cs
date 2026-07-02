using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using InstructorApp.Payments.Dto;

namespace InstructorApp.Payments
{
    public interface IPaymentAppService : IAsyncCrudAppService<
        PaymentDto,
        long,
        PagedPaymentResultRequestDto,
        CreatePaymentDto,
        PaymentDto>
    {
    }
}
