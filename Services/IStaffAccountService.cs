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
                account.StaffId = AutoGenId.AutoGenerateId();
                account.Role = 1;
                account.IsActive = true;
                account.IsDelete = false;
                var check = repository.GetAll(x => x.Username == account.Username & x.IsActive == true);
                if(check != null)
                {
                    throw new Exception("Username Exist!!!");
                }
                else
                {
                    return await repository.Add(account);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }        
        }

        public Task<bool> Delete(string id)
        {
            try
            {
                var account = repository.Get(id).Result;
                if(account != null)
                {
                    account.IsDelete = false;
                    return repository.Update(id, account);
                }
                else
                {
                    throw new Exception("Not Found Account");
                }
            }
            catch(Exception e)
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

        public Task<StaffAccount> GetStaffAccount(string id)
        {
            try
            {
                var account = repository.Get(id);
                if(account == null)
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
                var accountStaff = repository.Get(account.StaffId).Result;
                if( accountStaff == null)
                {
                    throw new Exception("Not Found Account");
                }
                else
                {
                    accountStaff.Email = account.Email;
                    accountStaff.Password = account.Password;
                    return await repository.Update(account.StaffId, accountStaff);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
