using System.Collections.Generic;
using System.Linq;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using System.Data.Entity;

namespace BS.Bussnies.Managers.Concreate
{
    public class PriceManager : AbstractManager, IPriceManager
    {
        public PriceManager(string cs) : base(cs) { }

        public IEnumerable<Price> GetAll()
        {
            using(DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Price>().ToList();
            }
        }
    }
}
