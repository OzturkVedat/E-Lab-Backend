using AutoMapper;
using E_Lab_Backend.Dto;
using E_Lab_Backend.Models;

namespace E_Lab_Backend
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<TestResultDto, TestResultPatient>().ReverseMap();
            CreateMap<NewTestResultDto, TestResultPatient>().ReverseMap();
        }

    }
}
