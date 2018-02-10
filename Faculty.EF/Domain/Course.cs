using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Course : BaseEntity
    {
        public Guid CathedraId { get; set; }
        public Guid SubjectId { get; set; }

        public Cathedra Cathedra { get; set; }
        public Subject Subject { get; set; }
    }
}