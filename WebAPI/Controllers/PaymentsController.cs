using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Ultilities;
using Services;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private IWebHostEnvironment Environment;
        private readonly IMapper mapper;
        private readonly IPaymentHistoryService paymentHistoryService;
        private readonly IPremiumPackageService premiumPackageService;
        public PaymentsController(IWebHostEnvironment env, IPremiumPackageService premiumPackageService, IPaymentHistoryService paymentHistoryService, IMapper mapper)
        {
            Environment = env;
            this.premiumPackageService = premiumPackageService;
            this.paymentHistoryService = paymentHistoryService;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Get([Required] string id)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Not Login"
                    });
                }

                if (role != CommonValues.CUSTOMER)
                {
                    throw new Exception("Role Denied");
                }

                var premium = await premiumPackageService.Get(id);
                if (premium == null)
                {
                    throw new Exception("Not Found Premium Package");
                }

                var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                string wwwPath = this.Environment.WebRootPath;

                string contentPath = this.Environment.ContentRootPath;

                var amount = (int)premium.PackageAmount;

                var imagePath = "";
                byte[] b1 = null;
                switch (amount)
                {
                    case 79000:
                        {
                            imagePath = Path.Combine(wwwPath, "Momo_QR_Payment", "premium_79000.jpg");
                            b1 = System.IO.File.ReadAllBytes(imagePath);
                            break;
                        }
                    case 299000:
                        {
                            imagePath = Path.Combine(wwwPath, "Momo_QR_Payment", "premium_299000.jpg");
                            b1 = System.IO.File.ReadAllBytes(imagePath);
                            break;
                        }
                    default: throw new Exception($"Not Found QR code with {amount} price");
                }
                return Ok(new
                {
                    ImageQR = File(b1, "image/jpeg"),
                    PrivateCode = AutoGenId.GenerateRandomString(),
                });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Message = ex.Message,
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([Required] string packageId,[Required] string privateCode)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Not Login"
                    });
                }

                if (role != CommonValues.CUSTOMER)
                {
                    throw new Exception("Role Denied");
                }

                var premium = await premiumPackageService.Get(packageId);
                if (premium == null)
                {
                    throw new Exception("Not Found Premium Package");
                }

                var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                string wwwPath = this.Environment.WebRootPath;

                string contentPath = this.Environment.ContentRootPath;

                var payment = await paymentHistoryService.AddPaymentHistory(cusId, packageId, privateCode);

                if (payment != null)
                {
                    return Ok(new
                    {
                        Status = "Success",
                        Message = "Create Payment Success"
                    });
                }
                else
                {
                    throw new Exception("Create Payment Fail");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    Message = ex.Message,
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPayments(string? privateCode, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Not Login"
                    });
                }

                if (role != CommonValues.ADMIN || role != CommonValues.STAFF)
                {
                    throw new Exception("Role Denied");
                }

                var payments = paymentHistoryService.GetAllPaymentHistory(privateCode, startDate, endDate);
                var paymentVMs = new List<PaymentHistoryAllVM>();
                foreach (var payment in payments)
                {
                    paymentVMs.Add(mapper.Map<PaymentHistoryAllVM>(payment));
                }
                return Ok(new
                {
                    Status = "Success",
                    Data = payments,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePaymentStatus([Required] string paymentId)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Not Login"
                    });
                }

                if (role != CommonValues.ADMIN || role != CommonValues.STAFF)
                {
                    throw new Exception("Role Denied");
                }

                var check = await paymentHistoryService.UpdatePaymentHistory(paymentId);
                if (!check)
                {
                    throw new Exception("Update Status Fail");
                }
                return Ok(new
                {
                    Status = "Success",
                    Message = "Update Status Success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetDetailPayment(string paymentId)
        {
            try
            {
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (role == null)
                {
                    return Unauthorized(new
                    {
                        Message = "Not Login"
                    });
                }

                if (role != CommonValues.ADMIN || role != CommonValues.STAFF)
                {
                    throw new Exception("Role Denied");
                }

                var payment = await paymentHistoryService.GetPaymentHistory(paymentId);
                if (payment == null)
                {
                    throw new Exception("Not Found Payment");
                }
                return Ok(new
                {
                    Status = "Success",
                    Data = mapper.Map<PaymentHistoryVM>(payment)
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                });
            }
        }
    }
}
