using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faculty.EFCore.Domain
{
    public class Cathedra : BaseLookup
    {
        [Required]
        public Guid? FacultyId { get; set; }

        [ForeignKey(nameof(FacultyId))]
        public Faculty Faculty { get; set; }
    }
}