using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Student : BaseLookup
    {
        public string FirstName { get; set; }

        public Guid GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }
    }
}