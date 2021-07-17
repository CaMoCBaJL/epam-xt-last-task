namespace WebPL.Models
{
    public static class DataToAddComment
    {
        public static string AddingResult { get; set; }

        public static int UserId { get; set; }

        public static string Text { get; set; }

        public static int RecipeId { get; set; }

        public static void Reset()
        {
            AddingResult = null;

            UserId = 0;

            Text = null;

            RecipeId = 0;
        }
    }
}