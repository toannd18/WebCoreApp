using AutoMapper;
using DataContext.WebCoreApp.Pipe;
using WebCoreApp.Areas.Pipes.Models;
using WebCoreApp.Infrastructure.ViewModels.Pipe;

namespace WebCoreApp.Mapping
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<Welder, WelderViewModel>().ReverseMap();

            CreateMap<MaterialPipe, MaterialViewModel>().ReverseMap();

            CreateMap<TypeJoint, TypeJointViewModel>().ReverseMap();

            CreateMap<Isometric, IsometricViewModel>().ReverseMap();

            CreateMap<IsoJoint, JointViewModel>()
                .ForMember(dest => dest.IsoName, opt => opt.MapFrom(src => src.DrawName)).ReverseMap();

            CreateMap<WeldingViewModel, IsoJoint>()
                .ForMember(dest => dest.DrawName, opts => opts.Ignore()).ReverseMap();

            CreateMap<Project, ProjectViewModel>().ReverseMap();
           
        }
    }
}