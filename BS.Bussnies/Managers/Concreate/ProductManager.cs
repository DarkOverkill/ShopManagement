using System.Collections.Generic;
using System.Linq;
using BSProject;
using BS.Bussnies.Managers.Abstract;
using System.Data.Entity;

namespace BS.Bussnies.Managers.Concreate
{
    public class ProductManager : AbstractManager, IProductManager
    {
        public ProductManager(string cs) : base(cs) { }

        public IEnumerable<Products> GetAll()
        {
            using (DbContext ctx = this.CreateDbContext())
            {
                return ctx.Set<Products>().ToList();
            }
        }
    }
}
