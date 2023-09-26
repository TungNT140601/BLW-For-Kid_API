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

            CreateMap<Direction, DirectionVM>().ReverseMap();

            CreateMap<Recipe, RecipeVM>().ReverseMap();

            CreateMap<IngredientOfRecipe, IngredientOfRecipeVM>().ReverseMap();

            CreateMap<Expert, ExpertVM>();
            CreateMap<ExpertVM, Expert>();

            CreateMap<StaffAccount, StaffAccountVM>().ReverseMap();
            CreateMap<StaffAccount, StaffAccountAddVM>().ReverseMap();
            CreateMap<StaffAccount, StaffAccountUpdateVM>().ReverseMap();
            CreateMap<StaffAccount, ChangePwdStaffAccountVM>().ReverseMap();

            CreateMap<Rating, RatingVM>().ReverseMap();


            CreateMap<PremiumPackage, PremiumPackageVM >().ReverseMap();
            CreateMap<PremiumPackage, PremiumPackageUpdateVM >().ReverseMap();

<<<<<<< HEAD
            CreateMap<Meal, MealAddVM>().ReverseMap();
=======
            CreateMap<PremiumPackage, PremiumPackage>().ReverseMap();


            CreateMap<Meal, MealVM>().ReverseMap();
>>>>>>> d6dac1dfcf847358057d714b4c32cef98bcb2f36
            CreateMap<Meal, MealUpdateVM>().ReverseMap();
            CreateMap<Meal, MealDeleteVM>().ReverseMap();

            CreateMap<Favorite, FavoriteVM>().ReverseMap();

            CreateMap<Age, AgeVM>().ReverseMap();
            CreateMap<Age, AgeUpdateVM>().ReverseMap();
        }
    }
}
