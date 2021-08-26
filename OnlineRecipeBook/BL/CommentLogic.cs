using System.Collections.Generic;
using System.Linq;
using BLInterfaces;
using DALInterfaces;
using DataValidator;

namespace BL
{
    class CommentLogic : ICommentLogic
    {
        ICommentDAL _DAO; // usually abbreviations are also written in PascalCase (Dal, Dao), but here it would be even camelCase (it's a private field)


        public CommentLogic(ICommentDAL CommentDAL) => _DAO = CommentDAL;

        public string CreateComment(int recipeId, int userId, string text) // why don't you use models (entities)?
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

        // these methods are about the same thing, group them together (and if likes/dislikes are about +1/-1 you could have only one method)
        public bool LikeTheComment(int commentId, int userId)
               => _DAO.LikeTheComment(commentId, userId);

        public bool DislikeTheComment(int commentId, int userId)
               => _DAO.DislikeTheComment(commentId, userId);

        public int FindCommentLocation(int commentId)
               => _DAO.FindCommentLocation(commentId);

        public string GetCommentAuthor(int userId)
        {
            throw new System.NotImplementedException(); // not good
        }

        public List<string> GetEntities() // or GetComments maybe?
        {
            return _DAO.GetEntities().Select(c => c.ToString()).ToList();
        }


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
