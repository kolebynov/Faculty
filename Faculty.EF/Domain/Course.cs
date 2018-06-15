using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faculty.EFCore.Domain
{
    public class Course : BaseEntity
    {
        [Required]
        public Guid? CathedraId { get; set; }

        [Required]
        public Guid? SubjectId { get; set; }

        [ForeignKey(nameof(CathedraId))]
        public Cathedra Cathedra { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}