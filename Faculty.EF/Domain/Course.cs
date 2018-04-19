using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Course : BaseEntity
    {
        public Guid CathedraId { get; set; }

        public Guid SubjectId { get; set; }

        [ForeignKey(nameof(CathedraId))]
        public Cathedra Cathedra { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}