using System.Collections.Generic;
using BSProject;

namespace BS.Bussnies.Managers.Abstract
{
    public interface IProductManager : IManager
    {
        IEnumerable<Products> GetAll();
    }
}
