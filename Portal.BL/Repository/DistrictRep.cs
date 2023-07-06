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
    public class DistrictRep : IDistrictRep
    {

        private readonly ApplicationContext db;

        public DistrictRep(ApplicationContext db)
        {
            this.db = db;
        }
        public async Task<IEnumerable<District>> GetAsync(Expression<Func<District, bool>> filter = null)
        {
            if (filter != null)
                return
                    await db.District.Where(filter).Include("City").ToListAsync();
            else
                return
                     await db.District.ToListAsync();
        }
    }
}
