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

            CreateMap<TestResult, TestResultDetails>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Patient.Gender))
                .ForMember(dest => dest.AgeInMonths, opt =>
                    opt.MapFrom(src =>
                        (DateTime.UtcNow.Year - src.Patient.BirthDate.Year) * 12 +
                        (DateTime.UtcNow.Month - src.Patient.BirthDate.Month)));

            CreateMap<RegisterDto, UserModel>();
        }

    }
}
