using CommonInterfaces;
using BLInterfaces;
using DALInterfaces;
using SqlDAL;
using BL;

namespace Dependencies
{
    public class DependencyResolver
    {
        public IDALDependencyResolver DAL => new DALDependencyResolver();

        public IBLDependencyResolver BL => new LogicDependencyProvider(DAL);

        public IAuthentificator Authentificator => DAL.Authentificator;
    }
}
