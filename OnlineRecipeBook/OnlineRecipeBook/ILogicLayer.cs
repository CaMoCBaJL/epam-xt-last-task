using System.Collections.Generic;

namespace BLInterfaces
{
    public interface ILogicLayer
    {
        List<string> GetEntities();

        bool RemoveEntity(int entityId);
    }
}
