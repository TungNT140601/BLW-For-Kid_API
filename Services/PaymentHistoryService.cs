using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentHistoryService
    {
        Task<IEnumerable<PaymentHistory>> GetAllPaymentHistory(string? privateCode, DateTime? startDate, DateTime? endDate);
        Task<PaymentHistory> GetPaymentHistory(string id);
        Task<PaymentHistory> AddPaymentHistory(string cusId, string premiumId, string privateCode);
        Task<bool> UpdatePaymentHistory(string paymentId);
    }
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private readonly IPaymentHistoryRepository historyRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly IPremiumPackageRepository premiumPackageRepository;
        public PaymentHistoryService(IPaymentHistoryRepository historyRepository, ICustomerRepository customerRepository, IPremiumPackageRepository premiumPackageRepository)
        {
            this.historyRepository = historyRepository;
            this.customerRepository = customerRepository;
            this.premiumPackageRepository = premiumPackageRepository;
        }

        public async Task<PaymentHistory> AddPaymentHistory(string cusId, string premiumId, string privateCode)
        {
            try
            {
                var cus = await customerRepository.Get(cusId);
                var premium = await premiumPackageRepository.Get(premiumId);
                if (premium == null)
                {
                    throw new Exception("Not Found Premium Package");
                }
                var paymentHistory = new PaymentHistory
                {
                    PaymentId = AutoGenId.AutoGenerateId(),
                    CustomerId = cusId,
                    PackageId = premiumId,
                    Amount = premium.PackageAmount,
                    CreateDate = DateTime.Now,
                    PayTime = DateTime.Now,
                    PaymentStatus = false,
                    PrivateCode = privateCode,
                };
                var check = await historyRepository.Add(paymentHistory);
                if (check)
                {
                    return paymentHistory;
                }
                else
                {
                    throw new Exception("Create Payment Error");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaymentHistory> GetPaymentHistory(string id)
        {
            try
            {
                var payment = await historyRepository.Get(id);
                if (payment != null)
                {
                    payment.Customer = await customerRepository.Get(payment.CustomerId);
                    payment.Package = await premiumPackageRepository.Get(payment.PackageId);

                    return payment;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PaymentHistory>> GetAllPaymentHistory(string? privateCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var payments = new List<PaymentHistory>();
                if (startDate != null)
                {
                    if (endDate == null)
                    {
                        throw new Exception("Please Select EndDate");
                    }
                    else
                    {
                        if (privateCode != null)
                        {
                            payments = historyRepository.GetAll(x
                                => x.PrivateCode == privateCode && (startDate == null || (startDate != null && x.PayTime.Value.Date >= startDate.Value.Date)) && (endDate == null || (endDate != null && x.PayTime.Value.Date <= endDate.Value.Date)))
                                .OrderByDescending(x => x.PayTime).ToList();
                        }
                        else
                        {
                            payments = historyRepository.GetAll(x
                                => (startDate == null || (startDate != null && x.PayTime.Value.Date >= startDate.Value.Date)) && (endDate == null || (endDate != null && x.PayTime.Value.Date <= endDate.Value.Date)))
                                .OrderByDescending(x => x.PayTime).ToList();
                        }
                    }
                }
                else
                {
                    if (privateCode != null)
                    {
                        payments = historyRepository.GetAll(x
                            => x.PrivateCode == privateCode)
                            .OrderByDescending(x => x.PayTime).ToList();
                    }
                    else
                    {
                        payments = historyRepository.GetAll(x
                            => 1 == 1)
                            .OrderByDescending(x => x.PayTime).ToList();
                    }
                }
                if (payments != null)
                {
                    foreach (var pay in payments)
                    {
                        pay.Customer = await customerRepository.Get(pay.CustomerId);
                        pay.Package = await premiumPackageRepository.Get(pay.PackageId);
                    }
                }
                return payments;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdatePaymentHistory(string paymentId)
        {
            try
            {
                var payment = await historyRepository.Get(paymentId);
                if (payment == null)
                {
                    throw new Exception("Not Found Payment");
                }
                if (payment.PaymentStatus)
                {
                    throw new Exception("Payment update status error");
                }
                else
                {
                    var check = false;
                    var cus = await customerRepository.Get(payment.CustomerId);
                    var premium = await premiumPackageRepository.Get(payment.PackageId);
                    //if(cus.WasTried == false && cus.IsTried == false)
                    //{

                    //}
                    if (cus.LastEndPremiumDate == null || cus.LastEndPremiumDate <= DateTime.Now)
                    {
                        payment.StartDate = DateTime.Now;
                        payment.EndDate = DateTime.Now.AddMonths(premium.PackageMonth);
                        payment.PaymentStatus = true;
                        if (await historyRepository.Update(payment.PaymentId, payment))
                        {
                            cus.LastStartPremiumDate = payment.StartDate;
                            cus.LastEndPremiumDate = payment.EndDate;
                            cus.IsPremium = true;
                            check = await customerRepository.Update(cus.CustomerId, cus);
                        }
                    }
                    else
                    {
                        payment.StartDate = cus.LastEndPremiumDate;
                        payment.EndDate = cus.LastEndPremiumDate.Value.AddMonths(premium.PackageMonth);
                        payment.PaymentStatus = true;
                        if (await historyRepository.Update(payment.PaymentId, payment))
                        {
                            cus.LastEndPremiumDate = payment.EndDate;
                            cus.IsPremium = true;
                            check = await customerRepository.Update(cus.CustomerId, cus);
                        }
                    }
                    if (check)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Update Payment Error");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
