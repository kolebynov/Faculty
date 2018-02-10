using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.Web.Model
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public NameValuePair<Guid> Group { get; set; }
    }
}