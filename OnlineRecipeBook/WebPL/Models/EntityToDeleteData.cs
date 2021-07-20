namespace WebPL.Models
{
    public static class EntityToDeleteData
    {
        public static string SourcePage { get; set; }

        public static int EntityId { get; set; }

        public static bool ConfirmationResult { get; set; }

        public static string EntityName { get; set; }

        public static string DeletionResult { get; set; }

        public static void Reset()
        {
            SourcePage = null;

            EntityId = 0;

            ConfirmationResult = false;

            EntityName = null;

            DeletionResult = null;
        }
    }
}