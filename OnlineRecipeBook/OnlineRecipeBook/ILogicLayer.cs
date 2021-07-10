using System.Collections.Generic;

namespace OnlineRecipeBook
{
    public interface ILogicLayer
    {
        List<string> GetEntities();

        bool RemoveEntity(int entityId);
    }
}
