using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.EntityModels;
using Repositories.Ultilities;
using Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public IActionResult GetPaymentUrl(double price, string ipAddress)
        {
            var paymentInfo = configuration.GetSection("Payment");
            string url = paymentInfo["Url"];
            string returnUrl = paymentInfo["ReturnUrl"];
            string tmnCode = paymentInfo["TmnCode"];
            string hashSecret = paymentInfo["HashSecret"];
            price = Math.Round(price, 2) * 100;
            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", paymentInfo["vnp_Version"]); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", paymentInfo["vnp_Command"]); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", price + ""); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", paymentInfo["vnp_CurrCode"]); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", ipAddress); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", paymentInfo["vnp_Locale"]); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", $"Thanh toan goi premium - Gia tien: {price}vnd"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", paymentInfo["vnp_OrderType"]); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            string vnp_TxnRef = DateTime.Now.Ticks.ToString();
            pay.AddRequestData("vnp_TxnRef", vnp_TxnRef); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Ok(paymentUrl = paymentUrl);
        }
    }
}
