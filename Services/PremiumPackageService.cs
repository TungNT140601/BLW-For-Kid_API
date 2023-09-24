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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PremiumPackage> Get(string id)
        {
            try
            {
                var pre = await repository.Get(id);
                if (pre != null)
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


        public async Task<bool> Add(PremiumPackage premiumPackage)
        {
            try
            {
                if (string.IsNullOrEmpty(premiumPackage.PackageName))
                {
                    throw new Exception("PackageName cannot be empty!!!");
                }
                else if (premiumPackage.PackageAmount < 0)
                {
                    throw new Exception("PackageAmount cannot small than 0!!!");
                }
                else if (premiumPackage.PackageDiscount < 0)
                {
                    throw new Exception("PackageDiscount cannot small than 0!!!");
                }
                else if (premiumPackage.PackageMonth < 0)
                {
                    throw new Exception("PackageMonth cannot small than 0!!!");
                }
                else
                {
                    premiumPackage.PackageId = AutoGenId.AutoGenerateId();
                    premiumPackage.IsDelete = false;
                    return await repository.Add(premiumPackage);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                var pre = await repository.Get(id);
                if (pre != null)
                {
                    pre.IsDelete = true;
                    return await repository.Update(id, pre);
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

        public async Task<bool> Update(PremiumPackage premiumPackage)
        {
            try
            {
                var pre = await repository.Get(premiumPackage.PackageId);
                if (pre != null)
                {

                    if (string.IsNullOrEmpty(premiumPackage.PackageName))
                    {
                        throw new Exception("PackageName cannot be empty!!!");
                    }
                    else if (premiumPackage.PackageAmount < 0)
                    {
                        throw new Exception("PackageAmount cannot small than 0!!!");
                    }
                    else if (premiumPackage.PackageDiscount < 0)
                    {
                        throw new Exception("PackageDiscount cannot small than 0!!!");
                    }
                    else if (premiumPackage.PackageMonth < 0)
                    {
                        throw new Exception("PackageMonth cannot small than 0!!!");
                    }
                    else
                    {
                        pre.PackageName = premiumPackage.PackageName;
                        pre.PackageAmount = premiumPackage.PackageAmount;
                        pre.PackageDiscount = premiumPackage.PackageDiscount;
                        pre.PackageMonth = premiumPackage.PackageMonth;

                        return await repository.Update(premiumPackage.PackageId, pre);
                    }
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
