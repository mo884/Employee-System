using Microsoft.EntityFrameworkCore;
using Portal.BL.Interface;
using Portal.DAL.Database;
using Portal.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Repository
{
    public class CityRep : ICityRep
    {

        private readonly ApplicationContext db;

        public CityRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<City>> GetAsync(Expression<Func<City, bool>> filter = null)
        {
            if (filter != null)
                return
                    await db.City.Where(filter).Include("Country").ToListAsync();
            else
                return
                     await db.City.ToListAsync();
        }
    }
}
