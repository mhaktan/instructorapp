using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using InstructorApp.Entities;
using InstructorApp.Payments.Dto;
using InstructorApp.Authorization;

namespace InstructorApp.Payments
{
    public class PaymentAppService : AsyncCrudAppService<
        Payment,
        PaymentDto,
        long,
        PagedPaymentResultRequestDto,
        CreatePaymentDto,
        PaymentDto>,
        IPaymentAppService
    {
        public PaymentAppService(IRepository<Payment, long> repository)
            : base(repository)
        {
            // Claim-based authorization (JwtPermissionChecker reads JWT "permission" claims)
            GetPermissionName = PermissionNames.Payment_Read;
            GetAllPermissionName = PermissionNames.Payment_Read;
            CreatePermissionName = PermissionNames.Payment_Create;
            UpdatePermissionName = PermissionNames.Payment_Update;
            DeletePermissionName = PermissionNames.Payment_Delete;
        }

        protected override IQueryable<Payment> CreateFilteredQuery(PagedPaymentResultRequestDto input)
        {
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Id.ToString().Contains(input.Keyword) ||
                    (x.Notes != null && x.Notes.Contains(input.Keyword)))
                .WhereIf(!input.Notes.IsNullOrWhiteSpace(), x => x.Notes != null && x.Notes.Contains(input.Notes))
                .WhereIf(input.PaymentDate.HasValue, x => x.PaymentDate == input.PaymentDate.Value)
                .WhereIf(input.Amount.HasValue, x => x.Amount == input.Amount.Value)
                .WhereIf(input.SessionsPurchased.HasValue, x => x.SessionsPurchased == input.SessionsPurchased.Value)
                .WhereIf(input.PaymentMethod.HasValue, x => x.PaymentMethod == (PaymentMethod)input.PaymentMethod.Value)
                .WhereIf(input.MemberId.HasValue, x => x.MemberId == input.MemberId.Value);
        }
    }
}
