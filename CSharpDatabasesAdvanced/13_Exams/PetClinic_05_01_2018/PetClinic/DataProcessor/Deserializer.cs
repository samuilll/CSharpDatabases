namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using PetClinic.Data;
    using PetClinic.DataProcessor.Dtos;
    using PetClinic.Models;

    public class Deserializer
    {

        private const string ErrorMessage = "Error: Invalid data.";
        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var importedAnimalAids = JsonConvert.DeserializeObject<AnimalAid[]>(jsonString);

            var validAnimalAids = new List<AnimalAid>();

            foreach (var importedAnimalAid in importedAnimalAids)
            {
                if (!IsValid(importedAnimalAid) || validAnimalAids.Any(aa=>aa.Name==importedAnimalAid.Name))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validAnimalAid = new AnimalAid()
                {
                    Name = importedAnimalAid.Name,
                    Price = importedAnimalAid.Price
                };

                validAnimalAids.Add(validAnimalAid);

                sb.AppendLine($"Record {validAnimalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(validAnimalAids);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n','\r');
        }


        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var sb = new StringBuilder();

            var importedAnimals = JsonConvert.DeserializeObject<AnimalToImportDto[]>(jsonString, new IsoDateTimeConverter { DateTimeFormat = "dd-MM-yyyy"});

            var validPassports = new List<Passport>();

            var validAnimals = new List<Animal>();

            var validAnimalAids = new List<AnimalAid>();

            foreach (var importedAnimal in importedAnimals)
            {
                if (!IsValid(importedAnimal) || !IsValid(importedAnimal.Passport) || validPassports.Any(p=>p.SerialNumber==importedAnimal.Passport.SerialNumber) )
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validPassport = new Passport()
                {
                    OwnerName = importedAnimal.Passport.OwnerName,
                    OwnerPhoneNumber = importedAnimal.Passport.OwnerPhoneNumber,
                    RegistrationDate = importedAnimal.Passport.RegistrationDate,
                    SerialNumber = importedAnimal.Passport.SerialNumber,
                };

               var validAnimal = new Animal()
                {
                    Age = importedAnimal.Age,
                    Type = importedAnimal.Type,
                    Name = importedAnimal.Name,
                    Passport  = validPassport
                };

                validPassport.Animal = validAnimal;
                validAnimals.Add(validAnimal);

                validPassports.Add(validPassport);

                sb.AppendLine($"Record {validAnimal.Name} Passport №: {validPassport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(validAnimals);

            context.Passports.AddRange(validPassports);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');

        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(VetToImportDto[]),new XmlRootAttribute("Vets"));

            var importedVets = (VetToImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var validVets = new List<Vet>();

            ;

            foreach (var importedVet in importedVets)
            {
                if (!IsValid(importedVet) || validVets.Any(v=>v.PhoneNumber==importedVet.PhoneNumber))
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validVet = new Vet()
                {
                     Age = importedVet.Age,
                     PhoneNumber = importedVet.PhoneNumber,
                     Name = importedVet.Name,
                     Profession = importedVet.Profession
                };

                validVets.Add(validVet);

                sb.AppendLine($"Record {validVet.Name} successfully imported.");
            }

            context.Vets.AddRange(validVets);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ProcedureToImportDto[]), new XmlRootAttribute("Procedures"));

            var importedProcedures = (ProcedureToImportDto[])serializer.Deserialize(new StringReader(xmlString));

            var validProcedures = new List<Procedure>();

            var proceduresAids = new List<ProcedureAnimalAid>();

            var vets = context.Vets.ToList();

            var animalAids = context.AnimalAids.ToList();

            var animals = context.Animals.ToList();

            foreach (var importedProcedure in importedProcedures)
            {
                var date = DateTime.ParseExact(importedProcedure.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture);

                var vet = GetVetOrNull(vets, importedProcedure.Vet);

                var animal = GetAnimalOrNull(animals, importedProcedure.Animal);

                if (vet == null || animal==null)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                List<AnimalAid> validAids = GetAidsOrNull(animalAids, importedProcedure.AnimalAids);

                if (validAids==null)
                {
                    sb.AppendLine(ErrorMessage);

                    continue;
                }

                var validProcedure = new Procedure()
                {
                    Animal = animal,
                    Vet = vet,
                    DateTime = date,               
                };

                List<ProcedureAnimalAid> validProceduresAnimalAids = CreateProceduresAids(validAids,validProcedure);

                validProcedure.ProcedureAnimalAids = validProceduresAnimalAids;

                validProcedures.Add(validProcedure);

                proceduresAids.AddRange(validProceduresAnimalAids);

                sb.AppendLine("Record successfully imported.");

            }

            context.Procedures.AddRange(validProcedures);

            context.ProceduresAnimalAids.AddRange(proceduresAids);

            context.SaveChanges();

            return sb.ToString().TrimEnd('\n', '\r');
        }

        private static List<ProcedureAnimalAid> CreateProceduresAids(List<AnimalAid> validAids, Procedure procedure)
        {
            var validProcedureAnimalAids = new List<ProcedureAnimalAid>();

            foreach (var aid in validAids)
            {
                var validProcedureAnimalAid = new ProcedureAnimalAid()
                {
                    AnimalAid = aid,
                    Procedure = procedure
                };

                validProcedureAnimalAids.Add(validProcedureAnimalAid);
            }
            return validProcedureAnimalAids;
        }

        private static Animal GetAnimalOrNull(List<Animal> animals, string animalSerailNum)
        {
            return animals.FirstOrDefault(a => a.PassportSerialNumber == animalSerailNum);
        }

        private static List<AnimalAid> GetAidsOrNull(List<AnimalAid> animalAids, AnimalAidFoProcedureDto[] animalAidsToCheck)
        {
            var validAids = new List<AnimalAid>();

            foreach (var aid in animalAidsToCheck)
            {
                var validAid = animalAids.FirstOrDefault(a=>a.Name==aid.Name);

                if (validAid==null || validAids.Any(a=>a.Name==validAid.Name))
                {
                    return null;
                }

                validAids.Add(validAid);
            }

            return validAids;
        }

        private static Vet GetVetOrNull(List<Vet> vets, string vetName)
        {
            return vets.FirstOrDefault(v=>v.Name==vetName);
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj,validationContext,result,true);
        }
    }
}
