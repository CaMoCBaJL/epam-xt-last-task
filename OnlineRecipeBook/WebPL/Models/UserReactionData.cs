namespace WebPL.Models
{
    public static class UserReactionData
    {
        public static int Commentid { get; set; }

        public static int UserId { get; set; }

        public static bool? IsDislike { get; set; }

        public static void Reset()
        {
            Commentid = 0;

            UserId = 0;

            IsDislike = null;
        }
    }
}