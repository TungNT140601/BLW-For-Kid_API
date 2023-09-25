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

            CreateMap<Ingredient, IngredientVM>();
            CreateMap<IngredientVM, Ingredient>();

            CreateMap<Expert, ExpertVM>();
            CreateMap<ExpertVM, Expert>();

            CreateMap<StaffAccount, StaffAccountVM>().ReverseMap();
            CreateMap<StaffAccount, StaffAccountAddVM>().ReverseMap();
            CreateMap<StaffAccount, StaffAccountUpdateVM>().ReverseMap();
            CreateMap<StaffAccount, ChangePwdStaffAccountVM>().ReverseMap();

            CreateMap<Rating, RatingVM>().ReverseMap();
            CreateMap<Rating, RatingUpdateVM>().ReverseMap();

            CreateMap<PremiumPackage, PremiumPackageVM >().ReverseMap();
            CreateMap<PremiumPackage, PremiumPackageUpdateVM >().ReverseMap();

            CreateMap<Meal, MealVM>().ReverseMap();
            CreateMap<Meal, MealUpdateVM>().ReverseMap();

            CreateMap<Favorite, FavoriteVM>().ReverseMap();

            CreateMap<Age, AgeVM>().ReverseMap();
            CreateMap<Age, AgeUpdateVM>().ReverseMap();
        }
    }
}
