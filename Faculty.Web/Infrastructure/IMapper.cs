using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Web.Infrastructure
{
    public interface IMapper
    {
        V Map<T, V>(T src);
    }
}
