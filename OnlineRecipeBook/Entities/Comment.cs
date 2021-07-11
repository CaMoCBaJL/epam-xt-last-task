using System;
using System.Collections.Generic;
using System.Linq;
namespace Entities
{
    public class Comment : CommonEntity
    {
        string Text { get; set; }

        int LikesCounter { get; set; }

        int DislikesCounter { get; set; }


        public Comment(int id, string text, int likesNum, int dislikesNum) : base(id)
        {
            Text = text;

            LikesCounter = likesNum;

            DislikesCounter = dislikesNum;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
