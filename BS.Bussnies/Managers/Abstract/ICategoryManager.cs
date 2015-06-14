using System.Collections.Generic;
using BSProject;

namespace BS.Bussnies.Managers.Abstract
{
    public interface ICategoryManager : IManager
    {
        IEnumerable<Categoryes> GetAll();
    }
}
