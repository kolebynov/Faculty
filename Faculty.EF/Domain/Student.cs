using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Student : BaseLookup
    {
        public string FirstName { get; set; }
        public Guid GroupId { get; set; }

        public Group Group { get; set; }
    }
}