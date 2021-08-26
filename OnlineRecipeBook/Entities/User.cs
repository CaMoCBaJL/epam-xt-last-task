using System.Text;
using CommonConstants;

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
            StringBuilder result = new StringBuilder();

            // By the way, you don't open <p> tag, only closing it with the wrong slash. Browsers may fix some of them for you, but you shouldn't think that they must.
            result.Append(UserName + DelimiterConstant.myDelimiter);

            result.Append(Age + DelimiterConstant.myDelimiter);

            return result.ToString();
        }
    }
}
