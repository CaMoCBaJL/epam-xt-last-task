namespace WebPL.Models
{
    public static class DataToAddAndUpdateComment
    {
        public static int CommentId { get; set; }

        public static string OperationResult { get; set; }

        public static int UserId { get; set; }

        public static string Text { get; set; }

        public static int RecipeId { get; set; }

        public static void Reset()
        {
            OperationResult = null;

            UserId = 0;

            Text = null;

            RecipeId = 0;
        }
    }
}