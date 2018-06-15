using System.ComponentModel.DataAnnotations;

namespace Faculty.EFCore.Domain
{
    [DisplayColumn(nameof(Name))]
    public abstract class BaseLookup : BaseEntity
    {
        [MaxLength(250)]
        [Required(ErrorMessage = "Имя обязательно для заполнения")]
        public string Name { get; set; }
    }
}