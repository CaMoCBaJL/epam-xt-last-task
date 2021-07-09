using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public abstract class CommonEntity
    {
        public int Id { get; set; }

        public CommonEntity(int id) => Id = id;
    }
}
