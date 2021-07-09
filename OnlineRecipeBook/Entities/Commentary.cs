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


        public Commentary(int id, string text) : base(id)
        {
            Text = text;

            LikesCounter = 0;

            DislikesCounter = 0;
        }

        public void LikeTheComment() => LikesCounter++;

        public void DislikeTheComment() => DislikesCounter++;

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
