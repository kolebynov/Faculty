using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Cathedra : BaseLookup
    {
        public Guid FacultyId { get; set; }

        public Faculty Faculty { get; set; }
    }
}