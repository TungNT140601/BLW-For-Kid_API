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
        public async Task<bool> AddAge(Age age)
        {
            try
            {
                if (string.IsNullOrEmpty(age.AgeName))
                {
                    throw new Exception("AgeName cannot be empty!!!");
                }
                else
                {
                    age.AgeId = AutoGenId.AutoGenerateId();
                    age.IsDelete = false;
                    return await repository.Add(age);
                }               
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
                    if (string.IsNullOrEmpty(age.AgeName))
                    {
                        throw new Exception("AgeName cannot be empty!!!");
                    }
                    else
                    {
                        check.AgeName = age.AgeName;
                        return await repository.Update(age.AgeId, check);
                    }            
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

        public async Task<Age> GetAge(string id)
        {
            try
            {
                var check = await repository.Get(id);
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
