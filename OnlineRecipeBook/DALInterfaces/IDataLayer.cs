using Entities;
using System.Collections.Generic;

namespace DALInterfaces
{
    public interface IDataLayer
    {
        bool RemoveEntity(int entityId);
    }
}
