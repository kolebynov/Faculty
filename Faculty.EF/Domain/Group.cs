using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Group : BaseLookup
    {
        public Guid SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }
    }
}