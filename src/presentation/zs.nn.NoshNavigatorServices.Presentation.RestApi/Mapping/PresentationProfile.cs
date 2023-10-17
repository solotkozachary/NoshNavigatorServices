using AutoMapper;
using zs.nn.NoshNavigatorServices.Application.Recipe.Commands;
using zs.nn.NoshNavigatorServices.Presentation.RestApi.Models.Requests;

namespace zs.nn.NoshNavigatorServices.Presentation.RestApi.Mapping
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<CreateRecipeRequest, CreateRecipeCommand>();
            CreateMap<RecipeInstructionStepRequest, RecipeInstructionStep>();
            CreateMap<RecipeIngredientRequest, RecipeIngredient>();
        }
    }
}
