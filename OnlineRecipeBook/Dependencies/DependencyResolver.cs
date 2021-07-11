using CommonInterfaces;
using BLInterfaces;
using DALInterfaces;
using SqlDAL;
using BL;

namespace Dependencies
{
    public class DependencyResolver
    {
        IDALDependencyResolver _DAL => new DALDependencyResolver();

        IBLDependencyResolver _BL => new LogicDependencyProvider(_DAL);

        IAuthentificator Authentificator => _DAL.Authentificator;
    }
}
