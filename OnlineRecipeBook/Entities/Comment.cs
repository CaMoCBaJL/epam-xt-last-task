using System.Text;
using CommonConstants;

namespace Entities
{
    public class Comment : CommonEntity
    {
        public string Text { get; set; }

        public int LikesCounter { get; set; }

        public int DislikesCounter { get; set; }


        public Comment(int id, string text, int likesNum, int dislikesNum) : base(id)
        {
            Text = text;

            LikesCounter = likesNum;

            DislikesCounter = dislikesNum;
        }

        public override string ToString()
        {
            // this should output a simple string. Making HTML for comment is work for PL that is related to Web (Razor)
            // What if you decide to switch to be a Web API (returning JSONs/SOAP/Protobuf messages/whatever else)? 
            // You would need to change ToString in every entity for your usage (and 100 entities is a real number)
            // But most probably you would need to stop using ToString to transform your entities and start using actual models (classes)

            StringBuilder result = new StringBuilder();

            result.Append(Text + DelimiterConstant.myDelimiter);

            result.Append(LikesCounter + DelimiterConstant.myDelimiter);

            result.Append(DislikesCounter + DelimiterConstant.myDelimiter);

            result.Append(Id + DelimiterConstant.myDelimiter);

            return result.ToString();
        }
    }
}
