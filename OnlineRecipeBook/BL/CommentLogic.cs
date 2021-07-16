using System.Collections.Generic;
using BLInterfaces;
using DALInterfaces;
using DataValidator;

namespace BL
{
    class CommentLogic : ICommentLogic
    {
        ICommentDAL _DAO;


        public CommentLogic(ICommentDAL CommentDAL) => _DAO = CommentDAL;

        public string CreateComment(int recipeId, int userId, string text)
        {
            var validator = new CommentValidator();

            if (validator.ValidateComment(text).ValidationPassed())
            {
                if (_DAO.CreateComment(recipeId, userId, text))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateComment(text);
        }

        public bool DislikeTheComment(int commentId, int userId)
               => _DAO.DislikeTheComment(commentId, userId);

        public int FindCommentLocation(int commentId)
               => _DAO.FindCommentLocation(commentId);

        public string GetCommentAuthor(int userId)
        {
            throw new System.NotImplementedException();
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

        public bool LikeTheComment(int commentId, int userId)
               => _DAO.LikeTheComment(commentId, userId);

        public bool RemoveEntity(int entityId)
               => _DAO.RemoveEntity(entityId);

        public string UpdateComment(int CommentId, string text)
        {
            var validator = new CommentValidator();

            if (validator.ValidateComment(text).ValidationPassed())
            {
                if (_DAO.UpdateComment(CommentId, text))
                    return Constants.allOk;
                else
                    return Constants.serverError;
            }
            else
                return validator.ValidateComment(text);
        }
    }
}
