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

            CreateMap<Rating, RatingVM>().ReverseMap();

            CreateMap<PremiumPackage, PremiumPackage >().ReverseMap();

            CreateMap<Meal, MealVM>().ReverseMap();

            CreateMap<Favorite, FavoriteVM>().ReverseMap();

            CreateMap<Age, AgeVM>().ReverseMap();
        }


    }
}
