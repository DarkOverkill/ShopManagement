using System.Collections.Generic;
using BSProject;

namespace BS.Bussnies.Managers.Abstract
{
    public interface IProducerManager : IManager
    {
        IEnumerable<Producers> GetAll();
    }
}
