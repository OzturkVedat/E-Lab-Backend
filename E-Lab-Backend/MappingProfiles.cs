using AutoMapper;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;

namespace E_Lab_Backend
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TestResultDto, TestResult>().ReverseMap();

            CreateMap<NewTestResultDto, TestResult>();

            CreateMap<CheckManualDto,TestResultDetails>().ReverseMap();

            CreateMap<TestResult, TestResultDetails>()
            .ForMember(dest => dest.BirthDate,
                       opt => opt.MapFrom(src => src.Patient.BirthDate));

            CreateMap<RegisterDto, UserModel>();
        }

    }
}
