using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Student : BaseLookup
    {
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public Guid GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }
    }
}