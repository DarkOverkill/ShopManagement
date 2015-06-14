using System.Data.Entity;

namespace BS.Bussnies.Managers.Abstract
{
    public class AbstractManager
    {
        private readonly string _connctionString;

        public AbstractManager(string connctionString)
        {
            _connctionString = connctionString;
        }

        protected DbContext CreateDbContext()
        {
            return new DbContext(_connctionString);
        }
    }
}
