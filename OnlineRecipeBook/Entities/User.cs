using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class User : CommonEntity
    {
        public string UserName { get; }

        public int Age { get; }


        public User() { }

        public User(int id, string userName, int age) : base(id)
        {
            UserName = userName;

            Age = age;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
