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
    public interface IPremiumPackageService
    {
        IEnumerable<PremiumPackage> GetAll();
        Task<PremiumPackage> Get(string id);
        Task<bool> Add(PremiumPackage premiumPackage);
        Task<bool> Update(PremiumPackage premiumPackage);
        Task<bool> Delete(string id);
    }

    public class PremiumPackageService : IPremiumPackageService
    {
        private readonly IPremiumPackageRepository repository;
        public PremiumPackageService(IPremiumPackageRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<PremiumPackage> GetAll()
        {
            try
            {
                return repository.GetAll(x => x.IsDelete == false);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<PremiumPackage> Get(string id)
        {
            try
            {
                var pre = repository.Get(id);
                if(pre != null)
                {
                    return pre;
                }
                else
                {
                    throw new Exception("Not Found Premium Package!!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Task<bool> Add(PremiumPackage premiumPackage)
        {
            try
            {
                premiumPackage.PackageId = AutoGenId.AutoGenerateId();
                premiumPackage.IsDelete = false;

                return repository.Add(premiumPackage);
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
                var pre = repository.Get(id).Result;
                if( pre != null)
                {
                    pre.IsDelete = true;
                    return repository.Update(id, pre);
                }
                else
                {
                    throw new Exception("Not Found Premium Package!!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Update(PremiumPackage premiumPackage)
        {
            try
            {
                var pre = repository.Get(premiumPackage.PackageId).Result;
                if (pre != null)
                {
                    pre.PackageName = premiumPackage.PackageName;
                    pre.PackageAmount = premiumPackage.PackageAmount;
                    pre.PackageDiscount = premiumPackage.PackageDiscount;
                    pre.PackageMonth = premiumPackage.PackageMonth;
                    
                    return repository.Update(premiumPackage.PackageId, pre);
                }
                else
                {
                    throw new Exception("Not Found Premium Package!!!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
