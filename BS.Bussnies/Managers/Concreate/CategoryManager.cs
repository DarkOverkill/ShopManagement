using System.Collections.Generic;
using System.Linq;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using System.Data.Entity;

namespace BS.Bussnies.Managers.Concreate
{
    public class CategoryManager : AbstractManager, ICategoryManager
    {
        public CategoryManager(string cs) : base(cs) { }

        public IEnumerable<Categoryes> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Categoryes>().ToList();
            }
        }
    }
}
