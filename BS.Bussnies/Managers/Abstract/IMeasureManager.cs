using System.Collections.Generic;
using BSProject;

namespace BS.Bussnies.Managers.Abstract
{
    public interface IMeasureManager : IManager
    {
        IEnumerable<Measures> GetAll();
    }
}
