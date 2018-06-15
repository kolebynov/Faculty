using Microsoft.AspNetCore.Identity;
using System;

namespace Faculty.EFCore.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string Name { get; set; }
    }
}