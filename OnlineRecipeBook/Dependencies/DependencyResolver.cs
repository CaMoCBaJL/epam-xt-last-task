using CommonInterfaces;
using BLInterfaces;
using DALInterfaces;
using SqlDAL;
using BL;

namespace Dependencies
{
    public class DependencyResolver
    {

        #region Singleton
        private static DependencyResolver _instance;

        public static DependencyResolver Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DependencyResolver();

                return _instance;
            }
        }
        #endregion

        public IDALDependencyResolver DAL => new DALDependencyResolver();

        public IBLDependencyResolver BL => new LogicDependencyProvider(DAL);

        public IAuthentificator Authentificator => DAL.Authentificator;
    }
}
