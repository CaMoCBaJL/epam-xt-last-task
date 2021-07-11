namespace DataValidator
{
    public class CommentValidator
    {
        public string ValidateComment(string commentText)
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
