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

            CreateMap<TestResult, TestResultDetails>()
                .ForMember(dest => dest.AgeInMonths, opt =>
                    opt.MapFrom(src =>
                        (DateTime.UtcNow.Year - src.Patient.BirthDate.Year) * 12 +
                        (DateTime.UtcNow.Month - src.Patient.BirthDate.Month)));

            CreateMap<RegisterDto, UserModel>();
        }

    }
}
