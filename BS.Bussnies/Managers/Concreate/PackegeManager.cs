using System.Collections.Generic;
using System.Linq;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using System.Data.Entity;

namespace BS.Bussnies.Managers.Concreate
{
    public class PackegeManager : AbstractManager, IPackegeManager
    {
        public PackegeManager(string cs) : base(cs) { }

        public IEnumerable<Packeges> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Packeges>().ToList();
            }
        }
    }
}
