using AutoMapper;
using Repositories.EntityModels;
using WebAPI.ViewModels;

namespace WebAPI
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<CustomerAccount, CustomerVM>();
            CreateMap<CustomerVM, CustomerAccount>();

        }
    }
}
