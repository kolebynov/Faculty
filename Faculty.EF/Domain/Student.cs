using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Faculty.EFCore.Domain
{
    public class Student : BaseLookup
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия обязательна для заполнения")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Группа обязательна для заполнения")]
        public Guid? GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public Group Group { get; set; }
    }
}