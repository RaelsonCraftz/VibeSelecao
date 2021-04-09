using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vibe.Domain.Base
{
    public class Entity
    {
        public int Id { get; set; }
    }

    public class Entity<T>
    {
        public T Id { get; set; }
    }
}
