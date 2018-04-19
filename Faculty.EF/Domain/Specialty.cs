using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Specialty : BaseLookup
    {
        public Guid FacultyId { get; set; }

        [ForeignKey(nameof(FacultyId))]
        public Faculty Faculty { get; set; }
    }
}