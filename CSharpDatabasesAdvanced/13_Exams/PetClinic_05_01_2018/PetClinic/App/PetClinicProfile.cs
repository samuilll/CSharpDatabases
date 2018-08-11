namespace PetClinic.App
{
    using AutoMapper;
    using PetClinic.DataProcessor.Dtos;
    using PetClinic.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            CreateMap<Procedure, ProcedureToExportDto>()
                .ForMember(dest => dest.Passport, src => src.MapFrom(p => p.Animal.PassportSerialNumber))
                .ForMember(dest => dest.OwnerNumber, src => src.MapFrom(p => p.Animal.Passport.OwnerPhoneNumber))
                .ForMember(dest => dest.DateTime, src => src.MapFrom(p => p.DateTime.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.AnimalAids, src => src.MapFrom(p =>p.ProcedureAnimalAids));


            CreateMap<ProcedureAnimalAid, AnimalAidToExportDto>()
              .ForMember(dest => dest.Name, src => src.MapFrom(paa => paa.AnimalAid.Name))
              .ForMember(dest => dest.Price, src => src.MapFrom(paa => paa.AnimalAid.Price));

        }
    }
}
