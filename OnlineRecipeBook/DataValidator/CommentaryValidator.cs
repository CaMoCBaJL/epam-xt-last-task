namespace DataValidator
{
    public class CommentaryValidator
    {
        public string ValidateCommentary(string commentText)
        => ValidateCommentText(commentText);

        string ValidateCommentText(string text)
        {
            if (string.IsNullOrEmpty(text.Trim()))
                return "Text" + Constants.stringIsEmpty;
            else
                return Constants.allOk;
        }
    }
}
