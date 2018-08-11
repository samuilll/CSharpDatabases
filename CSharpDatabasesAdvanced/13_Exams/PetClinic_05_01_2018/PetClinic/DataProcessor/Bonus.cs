namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using PetClinic.Data;
    using PetClinic.Models;

    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var sb = new StringBuilder();

            Vet vet = GetVetOrNull(context,phoneNumber);

            if (vet==null)
            {
                sb.AppendLine($"Vet with phone number {phoneNumber} not found!");

                return sb.ToString().TrimEnd('\r', '\n');
            }

            var oldProfession = vet.Profession;

            vet.Profession = newProfession;

            context.SaveChanges();

             sb.AppendLine($"{vet.Name}'s profession updated from {oldProfession} to {newProfession}.");

            return sb.ToString().TrimEnd('\r','\n');
        }

        private static Vet GetVetOrNull(PetClinicContext context, string phoneNumber)
        {
            return context.Vets.FirstOrDefault(v=>v.PhoneNumber==phoneNumber);
        }

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);

            var result = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, validationContext, result, true);
        }
    }
}
