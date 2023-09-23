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
    public interface IStaffAccountService
    {
        Task<StaffAccount> GetStaffAccount(string id);
        IEnumerable<StaffAccount> GetAllStaffAccount();
        Task<bool> Add(StaffAccount account);
        Task<bool> Update(StaffAccount account);
        Task<bool> Delete(string id);
        Task<bool> ChangePwdStaff(string staffId, string oldPwd, string newPwd);
        Task<StaffAccount> CheckLogin(string username, string password);
    }

    public class StaffAccountService : IStaffAccountService
    {
        private readonly IStaffAccountRepository repository;

        public StaffAccountService(IStaffAccountRepository repositoryStaffAccount)
        {
            repository = repositoryStaffAccount;
        }
        public async Task<bool> Add(StaffAccount account)
        {
            try
            {
                var check = repository.GetAll(x => x.Username == account.Username && x.IsActive == true && x.IsDelete == false);
                if (check != null)
                {
                    throw new Exception("Username Exist!!!");
                }
                else
                {
                    if (string.IsNullOrEmpty(account.Email))
                    {
                        throw new Exception("Email cannot be empty!!!");
                    }
                    else if (string.IsNullOrEmpty(account.Username))
                    {
                        throw new Exception("Username cannot be empty!!!");
                    }
                    else if (string.IsNullOrEmpty(account.Password))
                    {
                        throw new Exception("Password cannot be empty!!!");
                    }
                    else if (string.IsNullOrEmpty(account.Fullname))
                    {
                        throw new Exception("Fullname cannot be empty!!!");
                    }
                    else
                    {
                        account.StaffId = AutoGenId.AutoGenerateId();
                        account.Role = 1;
                        account.IsActive = true;
                        account.IsDelete = false;
                        return await repository.Add(account);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var account = await repository.Get(id);
                if (account != null)
                {
                    account.IsDelete = false;
                    return await repository.Update(id, account);
                }
                else
                {
                    throw new Exception("Not Found Account");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<StaffAccount> GetAllStaffAccount()
        {
            try
            {
                return repository.GetAll(x => x.IsDelete == false);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StaffAccount> GetStaffAccount(string id)
        {
            try
            {
                var account = await repository.Get(id);
                if (account == null)
                {
                    throw new Exception("Not Found Account");
                }
                return account;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> Update(StaffAccount account)
        {
            try
            {
                var accountStaff = await repository.Get(account.StaffId);
                if (accountStaff == null)
                {
                    throw new Exception("Not Found Account");
                }
                else
                {
                    if (string.IsNullOrEmpty(account.Fullname))
                    {
                        throw new Exception("FullName cannot be empty!!!");
                    }
                    else
                    {
                        accountStaff.Fullname = account.Fullname;
                        return await repository.Update(account.StaffId, accountStaff);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ChangePwdStaff(string staffId, string oldPwd, string newPwd)
        {
            try
            {
                var staffAccount = await repository.Get(staffId);
                if (staffAccount != null)
                {
                    if (staffAccount.Password == oldPwd)
                    {
                        staffAccount.Password = newPwd;
                        return await repository.Update(staffAccount.StaffId, staffAccount);
                    }
                    else
                    {
                        throw new Exception("Old Password incorrect!!!");
                    }
                }
                else
                {
                    throw new Exception("Not Found Account");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StaffAccount> CheckLogin(string username, string password)
        {
            try
            {
                var account = await repository.Get(username);
                if (account == null)
                {
                    throw new Exception("Not Found Account");
                }
                else
                {
                    if(account.Password != password)
                    {
                        throw new Exception("Password Incorrect!!!");
                    }
                }
                return account;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
