using AutoMapper;
using BoardGameBrowserAPI.Data;
using BoardGameBrowserAPI.Models.BoardGame;
using BoardGameBrowserAPI.Models.Category;
using BoardGameBrowserAPI.Models.Designer;
using BoardGameBrowserAPI.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace BoardGameBrowserAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() 
        {
            CreateMap<BoardGame, BoardGameDTO>().ReverseMap();
            CreateMap<BoardGame, GetBoardGameDTO>().ReverseMap();
            CreateMap<BoardGame, CreateBoardGameDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, GetCategoryListDTO>().ReverseMap();

            CreateMap<Designer, DesignerDTO>().ReverseMap();
            CreateMap<Designer, GetDesignerDTO>().ReverseMap();
            CreateMap<Designer, CreateDesignerDTO>().ReverseMap();
            CreateMap<Designer, GetDesignerListDTO>().ReverseMap();

            CreateMap<IdentityUser, APIUserDTO>().ReverseMap();
        }
    }
}
