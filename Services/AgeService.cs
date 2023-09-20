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
    public interface IAgeService
    {
        IEnumerable<Age> GetAllAge();
        Task<Age> GetAge(string id);
        Task<bool> AddAge(Age age);
        Task<bool> UpdateAge(Age age);
        Task<bool> DeleteAge(string id);
    }

    public class AgeService : IAgeService
    {
        private readonly IAgeRepository repository;

        public AgeService(IAgeRepository repository)
        {
            this.repository = repository;
        }
        public Task<bool> AddAge(Age age)
        {
            try
            {
                age.AgeId = AutoGenId.AutoGenerateId();
                age.IsDelete = false;
                return repository.Add(age);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAge(Age age)
        {
            try
            {
                var check = await repository.Get(age.AgeId);
                if (check != null)
                {
                    check.AgeName = age.AgeName;
                    return await repository.Update(age.AgeId, check);
                }
                else
                {
                    throw new Exception("Not Found Age");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAge(string id)
        {
            try
            {
                var check = await repository.Get(id);
                if (check != null)
                {
                    check.IsDelete = true;
                    return await repository.Update(id, check);
                }
                else
                {
                    throw new Exception("Not Found Age");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Age> GetAge(string id)
        {
            try
            {
                var check = repository.Get(id);
                if (check != null)
                {
                    return check;
                }
                else
                {
                    throw new Exception("Not Found Age");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Age> GetAllAge()
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
    }
}
