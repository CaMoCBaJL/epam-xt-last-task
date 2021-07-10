using System.Collections.Generic;
using BLInterfaces;
using DALInterfaces;
using DataValidator;

namespace BL
{
    class CommentLogic : ICommentaryLogic
    {
        ICommentaryDAL _DAO;


        public CommentLogic(ICommentaryDAL commentaryDAL) => _DAO = commentaryDAL;

        public string CreateCommentary(int recipeId, int userId, string text)
        {
            var validator = new CommentaryValidator();

            if (validator.ValidateCommentary(text).ValidationPassed())
            {
                if (_DAO.CreateCommentary(recipeId, userId, text))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateCommentary(text);
        }

        public List<string> GetEntities()
        {
            List<string> result = new List<string>();

            foreach (var comment in _DAO.GetEntities())
            {
                result.Add(comment.ToString());
            }

            return result;
        }

        public bool RemoveEntity(int entityId)
               => _DAO.RemoveEntity(entityId);

        public string UpdateCommentary(int commentaryId, string text)
        {
            var validator = new CommentaryValidator();

            if (validator.ValidateCommentary(text).ValidationPassed())
            {
                if (_DAO.UpdateCommentary(commentaryId, text))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateCommentary(text);
        }
    }
}
