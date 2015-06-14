using System.Collections.Generic;
using System.Linq;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using System.Data.Entity;

namespace BS.Bussnies.Managers.Concreate
{
    public class MeasureManager : AbstractManager, IMeasureManager
    {
        public MeasureManager(string cs) : base(cs) { }

        public IEnumerable<Measures> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Measures>().ToList();
            }
        }
    }
}
