using Entities;
using System.Collections.Generic;

namespace DALInterfaces
{
    public interface IDataLayer
    {
        IEnumerable<CommonEntity> GetEntities();

        bool RemoveEntity(int entityId);
    }
}
