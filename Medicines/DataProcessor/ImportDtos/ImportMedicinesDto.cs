using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ImportDtos
{
    [XmlType("Medicine")]
    public class ImportMedicinesDto
    {
        [XmlAttribute("category")]
        public string Category { get; set; } = null!;

        [Required]
        [MaxLength(150)]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0.01, 1000.00)]
        public decimal Price { get; set; }

        [Required]
        public string ProductionDate { get; set; } = null!;

        [Required]
        public string ExpiryDate { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Producer { get; set; } = null!;
    }
}
