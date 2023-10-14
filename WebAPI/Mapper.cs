using AutoMapper;
using Repositories.EntityModels;
using WebAPI.ViewModels;

namespace WebAPI
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<CustomerAccount, CustomerVM>().ReverseMap();

            CreateMap<Ingredient, IngredientVM>().ReverseMap();
            CreateMap<Ingredient, IngredientUpdateVM>().ReverseMap();
            CreateMap<Ingredient, IngredientDeleteVM>().ReverseMap();

            CreateMap<Direction, DirectionVM>().ReverseMap();

            CreateMap<Recipe, RecipeVM>().ReverseMap();
            CreateMap<Recipe, RecipeAllVM>().ReverseMap();

            CreateMap<IngredientOfRecipe, IngredientOfRecipeVM>().ReverseMap();

            CreateMap<Expert, ExpertVM>();
            CreateMap<ExpertVM, Expert>();

            CreateMap<StaffAccount, StaffAccountVM>().ReverseMap();
            CreateMap<StaffAccount, StaffAccountAddVM>().ReverseMap();
            CreateMap<StaffAccount, StaffAccountUpdateVM>().ReverseMap();
            CreateMap<StaffAccount, ChangePwdStaffAccountVM>().ReverseMap();

            CreateMap<Rating, RatingVM>().ReverseMap();
            CreateMap<Rating, RatingAllVM>().ReverseMap();


            CreateMap<PremiumPackage, PremiumPackageVM >().ReverseMap();
            CreateMap<PremiumPackage, PremiumPackageAddVM >().ReverseMap();
            CreateMap<PremiumPackage, PremiumPackageUpdateVM >().ReverseMap();


            CreateMap<Meal, MealAddVM>().ReverseMap();

            CreateMap<PremiumPackage, PremiumPackage>().ReverseMap();


            CreateMap<Meal, MealAddVM>().ReverseMap();
            CreateMap<Meal, MealUpdateVM>().ReverseMap();
            CreateMap<Meal, MealDeleteVM>().ReverseMap();

            CreateMap<Favorite, FavoriteVM>().ReverseMap();

            CreateMap<Age, AgeVM>().ReverseMap();
            CreateMap<Age, AgeUpdateVM>().ReverseMap();

            CreateMap<PaymentHistory, PaymentHistoryVM>().ReverseMap();
            CreateMap<PaymentHistory, PaymentHistoryAllVM>().ReverseMap();
        }
    }
}
