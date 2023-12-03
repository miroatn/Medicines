using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Pharmacy")]
    public class ImportPharmaciesDto
    {
        [XmlAttribute("non-stop")]
        public string IsNonStop { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(14)]
        [RegularExpression("^\\(\\d{3}\\)\\s{1}\\d{3}\\-\\d{4}$")]
        public string PhoneNumber { get; set; } = null!;

        [XmlArray("Medicines")]
        public ImportMedicinesDto[] Medicines { get; set; }
    }
}
