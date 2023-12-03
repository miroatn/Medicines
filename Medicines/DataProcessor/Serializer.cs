namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ExportDtos;
    using Medicines.Utilities;
    using Newtonsoft.Json;
    using System.Globalization;
    using System.Text.Json;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            XmlHelper helper = new XmlHelper();

            ExportPatientsDto[] patientsDtos = context.Patients
                .ToArray()
                .Where(p => p.PatientsMedicines.Any(pm => pm.Medicine.ProductionDate > dateTime))
                .Select(p => new ExportPatientsDto
                {
                    Gender = Enum.GetName(p.Gender.GetType(), p.Gender).ToLower(),
                    Name = p.FullName,
                    AgeGroup = Enum.GetName(p.AgeGroup.GetType(), p.AgeGroup),
                    Medicines = p.PatientsMedicines.Where(pm => pm.Medicine.ProductionDate > dateTime)
                    .OrderByDescending(m => m.Medicine.ExpiryDate)
                    .ThenBy(m => m.Medicine.Price)
                    .Select(pm => new ExportMedicinesDto
                    {
                        //Category = Enum.GetName(pm.Medicine.Category.GetType(), pm.Medicine.Category).ToLower(),
                        Category = pm.Medicine.Category.ToString("g"),
                        Name = pm.Medicine.Name,
                        Price = pm.Medicine.Price.ToString("F2"),
                        Producer = pm.Medicine.Producer,
                        BestBefore = pm.Medicine.ExpiryDate.ToString("yyyy-MM-dd")
                    })
                    .ToArray()

                })
                .OrderByDescending(p => p.Medicines.Count())
                .ThenBy(p => p.Name)
                .ToArray();

            return helper.Serialize(patientsDtos, "Patients");
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            var medicines = context.Medicines
                .ToArray()
                .Where(m => (int)m.Category == medicineCategory && m.Pharmacy.IsNonStop)
                .Select(m => new
                {
                    m.Name,
                    Price = m.Price.ToString("F2"),
                    Pharmacy = new
                    {
                        Name = m.Pharmacy.Name,
                        PhoneNumber = m.Pharmacy.PhoneNumber
                    }
                }).OrderBy(m => m.Price)
                .ThenBy(m => m.Name)
                .ToArray();

            return JsonConvert.SerializeObject(medicines, Formatting.Indented);
        }
    }
}
