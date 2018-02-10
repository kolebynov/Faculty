using System;
using System.Collections.Generic;
using System.Text;

namespace Faculty.Web.Model
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public NameValuePair<Guid> Cathedra { get; set; }
        public NameValuePair<Guid> Subject { get; set; }
    }
}