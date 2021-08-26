namespace DataValidator
{
    public class CommentValidator
    {
        public string ValidateComment(string commentText)
        => ValidateCommentText(commentText);

        string ValidateCommentText(string text) // 'private' access modifier (even VS should show a squiggly line)
        {
            if (string.IsNullOrEmpty(text.Trim())) // string.IsNullOrWhitespace is the same
                return "Text" + Constants.stringIsEmpty;
            else
                return Constants.allOk; // better to return a special value, when there are no errors (null if no errors, string with the error if there is an error)
        }
    }
}
