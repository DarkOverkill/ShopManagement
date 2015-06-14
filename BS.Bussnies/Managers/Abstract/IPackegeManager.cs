using System.Collections.Generic;
using BSProject;

namespace BS.Bussnies.Managers.Abstract
{
    public interface IPackegeManager : IManager
    {
        IEnumerable<Packeges> GetAll();
    }
}
