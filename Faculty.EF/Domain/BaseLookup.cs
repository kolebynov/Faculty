using System.ComponentModel.DataAnnotations;

namespace Faculty.EFCore.Domain
{
    [DisplayColumn(nameof(Name))]
    public abstract class BaseLookup : BaseEntity
    {
        [MaxLength(250)]
        public string Name { get; set; }
    }
}