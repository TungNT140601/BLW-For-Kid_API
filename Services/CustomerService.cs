using Repositories.EntityModels;
using Repositories.Repository.Interface;
using Repositories.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        Task<CustomerAccount> Get(string id);
        IEnumerable<CustomerAccount> GetAll();
        Task<bool> Add(CustomerAccount account);
        Task<bool> Update(CustomerAccount account);
        Task<bool> Delete(string id);
        Task<CustomerAccount> LoginEmail(string email, string ggId, string fullname, string avatar);
        Task<CustomerAccount> LoginFacebook(string fbId, string fullname, string avatar);
        Task<CustomerAccount> LoginPhone(string phone, string password);
        Task<CustomerAccount> RegisPhone(string phone, string password, string fullname);
        bool CheckPhoneExist(string phone);
        Task<bool> ChangePassword(string cusId, string oldPassword, string newPassword);
        Task<bool> ResetPassword(string phoneNum, string newPassword);
    }
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public Task<bool> Add(CustomerAccount account)
        {
            try
            {
                return customerRepository.Add(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                var cus = customerRepository.Get(id);
                if (cus == null)
                {
                    throw new Exception("Không tìm thấy tài khoản");
                }
                else
                {
                    return customerRepository.Delete(cus);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<CustomerAccount> Get(string id)
        {
            try
            {
                return customerRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<CustomerAccount> GetAll()
        {
            try
            {
                return customerRepository.GetAll(x => x.IsActive == true && x.IsDelete == false).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Update(CustomerAccount account)
        {
            try
            {
                var cus = customerRepository.Get(account.CustomerId).Result;
                if (cus == null)
                {
                    throw new Exception("Không tìm thấy tài khoản");
                }
                else
                {
                    if (customerRepository.GetAll(x => x.PhoneNum == cus.PhoneNum && x.CustomerId != cus.CustomerId) != null)
                    {
                        throw new Exception("Số điện thoại đã được sử dụng");
                    }
                    cus.Avatar = account.Avatar;
                    cus.Fullname = account.Fullname;
                    cus.DateOfBirth = account.DateOfBirth;
                    cus.Gender = account.Gender;
                    cus.PhoneNum = account.PhoneNum;
                    return customerRepository.Update(cus.CustomerId, cus);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CustomerAccount> LoginEmail(string email, string ggId, string fullname, string avatar)
        {
            try
            {
                var cus = customerRepository.GetAll(x => x.Email == email && x.GoogleId == ggId).FirstOrDefault();
                if (cus == null)
                {
                    cus = new CustomerAccount
                    {
                        CustomerId = AutoGenId.AutoGenerateId(),
                        Email = email,
                        GoogleId = ggId,
                        IsActive = true,
                        IsDelete = false,
                        IsPremium = false,
                        WasTried = false,
                        IsTried = false,
                        Fullname = fullname,
                        Avatar = avatar,
                        LastLogin = DateTime.Now,
                    };
                    if (await customerRepository.Add(cus))
                    {
                        return cus;
                    }
                }
                else
                {
                    if (cus.IsActive == false)
                    {
                        throw new Exception("Tài khoản bị khóa");
                    }
                    if (cus.IsDelete == true)
                    {
                        throw new Exception("Tài khoản bị xóa khỏi hệ thống");
                    }
                    cus.LastLogin = DateTime.Now;
                    if (await customerRepository.Update(cus.CustomerId, cus))
                    {
                        return cus;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CustomerAccount> LoginFacebook(string fbId, string fullname, string avatar)
        {
            try
            {
                var cus = customerRepository.GetAll(x => x.FacebookId == fbId).FirstOrDefault();
                if (cus == null)
                {
                    cus = new CustomerAccount
                    {
                        CustomerId = AutoGenId.AutoGenerateId(),
                        FacebookId = fbId,
                        IsActive = true,
                        IsDelete = false,
                        IsPremium = false,
                        WasTried = false,
                        IsTried = false,
                        Fullname = fullname,
                        Avatar = avatar,
                        LastLogin = DateTime.Now,
                    };
                    if (await customerRepository.Add(cus))
                    {
                        return cus;
                    }
                }
                else
                {
                    if (cus.IsActive == false)
                    {
                        throw new Exception("Tài khoản bị khóa");
                    }
                    if (cus.IsDelete == true)
                    {
                        throw new Exception("Tài khoản bị xóa khỏi hệ thống");
                    }
                    cus.LastLogin = DateTime.Now;
                    if (await customerRepository.Update(cus.CustomerId, cus))
                    {
                        return cus;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CustomerAccount> LoginPhone(string phone, string password)
        {
            try
            {
                var cus = customerRepository.GetAll(x => x.PhoneNum == phone && x.Password == password).FirstOrDefault();
                if (cus == null)
                {
                    throw new Exception("Sai thông tin đăng nhập");
                }
                else
                {
                    if (cus.IsActive == false)
                    {
                        throw new Exception("Tài khoản bị khóa");
                    }
                    if (cus.IsDelete == true)
                    {
                        throw new Exception("Tài khoản bị xóa khỏi hệ thống");
                    }
                    cus.LastLogin = DateTime.Now;
                    if (await customerRepository.Update(cus.CustomerId, cus))
                    {
                        return cus;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CustomerAccount> RegisPhone(string phone, string password, string fullname)
        {
            try
            {
                var cus = customerRepository.GetAll(x => x.PhoneNum == phone).FirstOrDefault();
                if (cus == null)
                {
                    cus = new CustomerAccount
                    {
                        CustomerId = AutoGenId.AutoGenerateId(),
                        Password = password,
                        PhoneNum = phone,
                        Fullname = fullname,
                        LastLogin = DateTime.Now,
                        IsActive = true,
                        IsDelete = false,
                        IsPremium = false,
                        WasTried = false,
                        IsTried = false,
                    };
                    if (await customerRepository.Add(cus))
                    {
                        return cus;
                    }
                }
                else
                {
                    throw new Exception("Số điện thoại đã được sử dụng");
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool CheckPhoneExist(string phone)
        {
            try
            {
                var cus = customerRepository.GetAll(x => x.PhoneNum == phone).FirstOrDefault();
                return cus != null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ChangePassword(string cusId, string oldPassword, string newPassword)
        {
            try
            {
                var cus = customerRepository.Get(cusId).Result;
                if (cus != null)
                {
                    if (cus.Password == oldPassword)
                    {
                        cus.Password = newPassword;
                        return await customerRepository.Update(cus.CustomerId, cus);
                    }
                    else
                    {
                        throw new Exception("Mật khẩu cũ không chính xác");
                    }
                }
                else
                {
                    throw new Exception("Không tìm thấy tài khoản");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ResetPassword(string phoneNum, string newPassword)
        {
            try
            {
                var cus = customerRepository.GetAll(x => x.PhoneNum == phoneNum).FirstOrDefault();
                if (cus != null)
                {
                    cus.Password = newPassword;
                    return await customerRepository.Update(cus.CustomerId, cus);
                }
                else
                {
                    throw new Exception("Không tìm thấy tài khoản");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
