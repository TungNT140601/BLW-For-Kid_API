﻿using Repositories.EntityModels;
using Repositories.Repository;
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
        Task<bool> Delete(Expert exp);
        Task<bool> ResetPassword(string expertID, string oldPassword, string newPassword);
        Task<Expert> LoginExpert(string username, string password);
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
                if (expertRepository.GetAll(x => x.ExpertId != expert.ExpertId && x.Username == expert.Username && x.IsDelete == false).Any())
                {
                    throw new Exception("Name exist");
                }
                expert.ExpertId = AutoGenId.AutoGenerateId();
                expert.IsDelete = false;
                expert.IsActive = true;
                return expertRepository.Add(expert);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> Delete(Expert exp)
        {
            try
            {
                var expert = expertRepository.Get(exp.ExpertId);
                if (expert != null)
                {
                    expert.Result.IsDelete = true;
                    return expertRepository.Update(exp.ExpertId, expert.Result);
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
                expert.Email = item.Email;
                expert.PhoneNum = item.PhoneNum;
                expert.Avatar = item.Avatar;
                expert.DateOfBirth = item.DateOfBirth;
                expert.Gender = item.Gender;
                expert.Name = item.Name;
                expert.Title = item.Title;
                expert.Position = item.Position;
                expert.WorkUnit = item.Position;
                expert.Description = item.Description;
                expert.ProfessionalQualification = item.Position;
                expert.WorkProgress = item.WorkProgress;
                expert.Achievements = item.Achievements;
                return expertRepository.Update(expert.ExpertId, expert);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ResetPassword(string expertID, string oldPassword, string newPassword)
        {
            try
            {
                var exp = await expertRepository.Get(expertID);
                if (exp != null)
                {
                    if(exp.Password != oldPassword)
                    {
                        throw new Exception("Mật khẩu không đúng");
                    }
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

        public async Task<Expert> LoginExpert(string username, string password)
        {
            try
            {
                var exp = expertRepository.GetAll(x => x.Username == username && x.Password == password).FirstOrDefault();
                if (exp == null)
                {
                    exp = new Expert
                    {
                        ExpertId = AutoGenId.AutoGenerateId(),
                        Username = username,
                        Password = password,
                        IsActive = true,
                        IsDelete = false,
                       
                        
                    };
                    if (await expertRepository.Add(exp))
                    {
                        return exp;
                    }
                }
                else
                {
                    if (exp.IsActive == false)
                    {
                        throw new Exception("Tài khoản bị khóa");
                    }
                    if (exp.IsDelete == true)
                    {
                        throw new Exception("Tài khoản bị xóa khỏi hệ thống");
                    }
                    
                    if (await expertRepository.Update(exp.ExpertId, exp))
                    {
                        return exp;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
