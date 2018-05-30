using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Faculty.EFCore.Domain
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string Name { get; set; }
    }
}
