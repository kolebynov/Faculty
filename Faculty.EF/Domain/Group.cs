﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Faculty.EFCore.Domain
{
    public class Group : BaseLookup
    {
        [Required(AllowEmptyStrings = false)]
        public Guid SpecialtyId { get; set; }

        [ForeignKey(nameof(SpecialtyId))]
        public Specialty Specialty { get; set; }
    }
}