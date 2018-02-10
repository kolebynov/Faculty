using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.Web.Model
{
    public class SpecialtyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public NameValuePair<Guid> Faculty { get; set; }
    }
}