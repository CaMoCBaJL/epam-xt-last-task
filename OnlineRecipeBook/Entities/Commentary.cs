using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Commentary : CommonEntity
    {
        string Text { get; set; }

        int LikesCounter { get; set; }

        int DislikesCounter { get; set; }


        public Commentary(int id, string text, int likesNum, int dislikesNum) : base(id)
        {
            Text = text;

            LikesCounter = likesNum;

            DislikesCounter = dislikesNum;
        }

        public void LikeTheComment() => LikesCounter++;

        public void DislikeTheComment() => DislikesCounter++;

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
