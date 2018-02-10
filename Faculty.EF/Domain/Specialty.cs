using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Specialty : BaseLookup
    {
        public Guid FacultyId { get; set; }

        public Faculty Faculty { get; set; }
    }
}