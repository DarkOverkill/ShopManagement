using System.Collections.Generic;
using BSProject;

namespace BS.Bussnies.Managers.Abstract
{
    public interface IPriceManager : IManager
    {
        IEnumerable<Price> GetAll();
    }
}
