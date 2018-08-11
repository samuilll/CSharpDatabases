namespace PetClinic.DataProcessor
{
    using System;
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos;
    using Microsoft.EntityFrameworkCore;
    using System.Xml.Serialization;
    using System.IO;
    using System.Xml;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                .OrderBy(a => a.Age)
                .ThenBy(a => a.PassportSerialNumber)
                .Select(a => new
                {
                    OwnerName = a.Passport.OwnerName,
                    AnimalName = a.Name,
                    Age = a.Age,
                    SerialNumber = a.PassportSerialNumber,
                    RegisteredOn = a.Passport.RegistrationDate.ToString("dd-MM-yyyy")
                }
                )
                .ToList();

            var jsonString = JsonConvert.SerializeObject(animals);

            return jsonString;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            //var procedures = context.Procedures
            //    .OrderBy(p=>p.DateTime)
            //    .Select(p=> new ProcedureToExportDto
            //    {
            //        Passport = p.Animal.PassportSerialNumber,
            //        OwnerNumber = p.Animal.Passport.OwnerPhoneNumber,
            //        DateTime = p.DateTime.ToString("dd-MM-yyyy"),
            //        AnimalAids = p.ProcedureAnimalAids.Select(paa=> new AnimalAidToExportDto
            //        {
            //             Name = paa.AnimalAid.Name,
            //             Price = paa.AnimalAid.Price
            //        }
            //        )
            //        .ToArray()               
            //    }
            //    )
            //    .OrderBy(p=>p.Passport)
            //    .ToArray();

            var procedures = context.Procedures
               .OrderBy(p => p.DateTime)
               .ProjectTo<ProcedureToExportDto>()
               .OrderBy(p => p.Passport)
               .ToArray();

            var xmlNameSpaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

            var serializer = new XmlSerializer(typeof(ProcedureToExportDto[]), new XmlRootAttribute("Procedures"));

            var writer = new StringWriter();

            serializer.Serialize(writer, procedures, xmlNameSpaces);

            return writer.ToString();

        }
    }
}
