using Medicines.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Medicines.DataProcessor.ExportDtos
{
    [XmlType("Patient")]
    public class ExportPatientsDto
    {
        [XmlAttribute]
        public string Gender { get; set; } = null!;

        [XmlElement]
        public string Name { get; set; } = null!;

        [XmlElement]
        public string AgeGroup { get; set; } = null!;

        [XmlArray]
        public ExportMedicinesDto[] Medicines { get; set; } = null!;

    }
}
