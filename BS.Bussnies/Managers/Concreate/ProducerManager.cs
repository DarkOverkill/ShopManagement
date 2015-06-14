using System.Collections.Generic;
using System.Linq;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using System.Data.Entity;

namespace BS.Bussnies.Managers.Concreate
{
    public class ProducerManager : AbstractManager, IProducerManager
    {
        public ProducerManager(string cs) : base(cs) { }

        public IEnumerable<Producers> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Producers>().ToList();
            }
        }
    }
}
