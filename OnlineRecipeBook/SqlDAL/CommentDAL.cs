using System;
using System.Collections.Generic;
using DALInterfaces;
using Entities;

namespace SqlDAL
{
    public class CommentDAL : ICommentaryDAL
    {
        public bool CreateCommentary(int recipeId, int userId, string text)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Commentary> GetEntities()
        {
            throw new NotImplementedException();
        }

        public bool RemoveEntity(int entityId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCommentary(int commentaryId, string text)
        {
            throw new NotImplementedException();
        }
    }
}
