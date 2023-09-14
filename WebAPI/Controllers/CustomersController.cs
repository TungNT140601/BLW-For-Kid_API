using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Repositories.EntityModels;
using Repositories.Ultilities;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.PaymentSecurity.MoMo;
using WebAPI.ViewModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        public CustomersController(ICustomerService customerService, IMapper mapper, IConfiguration configuration)
        {
            this.customerService = customerService;
            this.mapper = mapper;
            this.configuration = configuration;
        }
        private CustomerAccount Validate(CustomerVM customerVM)
        {
            var err = "";
            try
            {
                if (string.IsNullOrEmpty(customerVM.Fullname) && customerVM.Fullname.Trim() == "")
                {
                    err += ";Tên không được để trống";
                }
                if (customerVM.Gender == null)
                {
                    err += ";Vui lòng chọn giới tính";
                }
                return mapper.Map<CustomerAccount>(customerVM);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(err))
                {
                    throw new Exception(err);
                }
            }
            return null;
        }
        private string GenerateJwtToken(string id)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, id),
            new Claim(ClaimTypes.Role, "Customer")
        };
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost]
        public async Task<IActionResult> LoginEmail(CusLoginEmail cusLoginEmail)
        {
            try
            {
                var cus = customerService.LoginEmail(cusLoginEmail.Email, cusLoginEmail.GoogleSub, cusLoginEmail.Fullname, cusLoginEmail.Avatar).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
                });
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Data = new { },
                    Token = "",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginPhone(CusLoginPhone cusLoginPhone)
        {
            try
            {
                var cus = customerService.LoginPhone(cusLoginPhone.Phone, cusLoginPhone.Password).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
                });
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Data = new { },
                    Token = "",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> LoginFacebook(CusLoginFacebook cusLoginFacebook)
        {
            try
            {
                var cus = customerService.LoginFacebook(cusLoginFacebook.FacebookId, cusLoginFacebook.Fullname, cusLoginFacebook.Avatar).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
                });
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    Data = new { },
                    Token = "",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisPhone(CusRegisPhone cusRegisPhone)
        {
            try
            {
                var cus = customerService.RegisPhone(cusRegisPhone.Phone, cusRegisPhone.Password, cusRegisPhone.Fullname).Result;
                return Ok(new
                {
                    Status = "Login Success",
                    Data = new
                    {
                        Fullname = cus.Fullname,
                        Avatar = cus.Avatar,
                        IsPremium = cus.IsPremium,
                    },
                    Token = GenerateJwtToken(cus.CustomerId)
                });
            }
            catch (AggregateException ae)
            {
                //List<string> errmsg = new List<string>();
                //foreach (var innerException in ae.InnerExceptions)
                //{
                //    errmsg.Add(innerException.Message);
                //}
                return StatusCode(400, new
                {
                    Status = "Error",
                    Data = new { },
                    Token = "",
                    //ErrorMessage = errmsg
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            try
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Customer")
                {
                    var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    var check = customerService.ChangePassword(cusId, changePassword.OldPassword, changePassword.NewPassword).Result;
                    return check ? Ok(new
                    {
                        Status = "Change Success"
                    }) : Ok(new
                    {
                        Status = "Change Fail"
                    });
                }
                else
                {
                    return StatusCode(400, new
                    {
                        Status = "Error",
                        ErrorMessage = "Role Denied"
                    });
                }
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var check = customerService.ResetPassword(resetPassword.Phone, resetPassword.NewPassword).Result;
                return check ? Ok(new
                {
                    Status = "Reset Success"
                }) : Ok(new
                {
                    Status = "Reset Fail"
                });
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInfo(CustomerVM customerVM)
        {
            try
            {
                if (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value == "Customer")
                {
                    var cusId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    return await customerService.Update(Validate(customerVM)) ? Ok(new
                    {
                        Status = "Update Success"
                    }) : Ok(new
                    {
                        Status = "Update Fail"
                    });
                }
                else
                {
                    return StatusCode(400, new
                    {
                        Status = "Error",
                        ErrorMessage = "Role Denied"
                    });
                }
            }
            catch (AggregateException ae)
            {
                return StatusCode(400, new
                {
                    Status = "Error",
                    ErrorMessage = ae.InnerExceptions[0].Message
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetPaymentMomoUrl(string channel, string packageId, string ipAddress,long amount)
        {
            return Ok(GetUrlMomo(amount));
        }
        [HttpGet]
        public async Task<IActionResult> GetResultMomoPayment(string partnerCode
            ,string orderId,string requestId,long amount,string orderInfo, string orderType
            ,string transId,string resultCode,string message,string payType,long responseTime,string extraData, string signature)
        {
            return Ok(new
            {
                PartnerCode = partnerCode,
                orderId = orderId,
                requestId = requestId,
                amount = amount,
                orderInfo = orderInfo,
                orderType = orderType,
                transId = transId,
                resultCode = resultCode,
                message = message,
                payType = payType,
                responseTime = responseTime,
                extraData = extraData,
                signature = signature,
            });
        }
        private string GetUrlMomo(long amount)
        {
            var momo = configuration.GetSection("PaymentMomo");
            //request params need to request to MoMo system
            string endpoint = momo["endpoint"];
            string partnerCode = momo["partnerCode"];
            string accessKey = momo["accessKey"];
            string serectkey = momo["serectkey"];
            string orderInfo = "Demo Premium";
            string redirectUrl = momo["redirectUrl"];
            string ipnUrl = momo["ipnUrl"];
            string requestType = momo["requestType"];
            string lang = momo["lang"];

            //string amountS = amount + "";
            string orderId = Guid.NewGuid().ToString();
            string requestId = Guid.NewGuid().ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "accessKey=" + accessKey +
                "&amount=" + amount +
                "&extraData=" + extraData +
                "&ipnUrl=" + ipnUrl +
                "&orderId=" + orderId +
                "&orderInfo=" + orderInfo +
                "&partnerCode=" + partnerCode +
                "&redirectUrl=" + redirectUrl +
                "&requestId=" + requestId +
                "&requestType=" + requestType
                ;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "partnerName", "Test" },
                { "storeId", "MomoTestStore" },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderId },
                { "orderInfo", orderInfo },
                { "redirectUrl", redirectUrl },
                { "ipnUrl", ipnUrl },
                { "lang", lang },
                { "extraData", extraData },
                { "requestType", requestType },
                { "signature", signature }

            };
            string jsonToMomo = "Json request to MoMo: " + message.ToString();

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            string returnFromMomo = "Return from MoMo: " + jmessage.ToString();

            int.TryParse(jmessage.GetValue("resultCode").ToString(), out int resultCode);
            if (resultCode == 0)
            {
                return jmessage.GetValue("payUrl").ToString();
            }

            return "";
        }
    }
}
