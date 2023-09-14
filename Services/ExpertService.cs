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
    public interface IExpertService
    {
        Task<Expert> Get(string id);
        IEnumerable<Expert> GetAll();
        Task<bool> Add(Expert expert);
        Task<bool> Update(Expert expert);
        Task<bool> Delete(string id);
        Task<bool> ResetPassword(string phoneNum, string newPassword);
    }
    public class ExpertService : IExpertService
    {
        private readonly IExpertRepository expertRepository;
        public ExpertService(IExpertRepository expertRepository)
        {
            this.expertRepository = expertRepository;
        }

        public IEnumerable<Expert> GetAll()
        {
            try
            {
                return expertRepository.GetAll(x => x.IsDelete == false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<Expert> Get(string id)
        {
            try
            {
                return expertRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Add(Expert expert)
        {
            try
            {
                if (expertRepository.GetAll(x => x.ExpertId == expert.ExpertId && x.IsDelete == false && x.IsActive == false).Any())
                {
                    throw new Exception("ID exist");
                }
                expert.ExpertId = AutoGenId.AutoGenerateId();
                expert.IsDelete = false;
                expert.IsActive = false;
                return expertRepository.Add(expert);
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
                var expert = expertRepository.Get(id);
                if (expert != null)
                {
                    expert.Result.IsDelete = true;
                    return expertRepository.Update(id, expert.Result);
                }
                else
                {
                    throw new Exception("Not Found Expert");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Update(Expert item)
        {
            try
            {
                var expert = expertRepository.Get(item.ExpertId).Result;
                if (expertRepository.GetAll(x => x.ExpertId != item.ExpertId && x.Username == item.Username && x.IsDelete == false).Any())
                {
                    throw new Exception("Name exist");
                }
                expert.Email = item.Email;
                expert.GoogleId = item.GoogleId;
                expert.FacebookId = item.FacebookId;
                expert.PhoneNum = item.PhoneNum;
                expert.Avatar = item.Avatar;
                expert.DateOfBirth = item.DateOfBirth;
                expert.Gender = item.Gender;
                expert.Username = item.Username;
                expert.Password = item.Password;
                expert.Name = item.Name;
                expert.Title = item.Title;
                expert.Position = item.Position;
                expert.WorkUnit = item.Position;
                expert.Description = item.Description;
                expert.ProfessionalQualification = item.Position;
                expert.WorkProgress = item.WorkProgress;
                expert.Achievements = item.Achievements;
                expert.IsActive = false;
                expert.IsDelete = false;
                return expertRepository.Update(expert.ExpertId, item);
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
                var exp = expertRepository.GetAll(x => x.PhoneNum == phoneNum).FirstOrDefault();
                if (exp != null)
                {
                    exp.Password = newPassword;
                    return await expertRepository.Update(exp.ExpertId, exp);
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
