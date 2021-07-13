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
            StringBuilder result = new StringBuilder();

            result.Append(Text + DelimiterConstant.myDelimiter);

            result.Append(LikesCounter + DelimiterConstant.myDelimiter);

            result.Append(DislikesCounter + DelimiterConstant.myDelimiter);

            result.Append(Id + DelimiterConstant.myDelimiter);

            return result.ToString();
        }
    }
}
