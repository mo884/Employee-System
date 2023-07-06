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
    public class CountryRep : ICountryRep
    {

        private readonly ApplicationContext db;

        public CountryRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<Country>> GetAsync(Expression<Func<Country, bool>> filter = null)
        {
            if (filter != null)
                return
                    await db.Country.Where(filter).ToListAsync();
            else
                return
                     await db.Country.ToListAsync();
        }
    }
}
