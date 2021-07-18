namespace WebPL.Models
{
    public static class DataToAddAndUpdateRecipe
    {
        public static string OperationResult { get; set; }

        public static string RecipeTitle { get; set; }

        public static string RecipeIngridients { get; set; }

        public static string CookingProcess { get; set; }

        public static int CreatorId { get; set; }


        public static void Reset()
        {
            OperationResult = null;

            RecipeTitle = null;

            RecipeIngridients = null;

            CookingProcess = null;

            CreatorId = 0;
        }
    }
}