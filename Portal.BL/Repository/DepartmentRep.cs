using Microsoft.EntityFrameworkCore;
using Portal.BL.Interface;
using Portal.BL.Models;
using Portal.DAL.Database;
using Portal.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Repository
{
    public class DepartmentRep : IDepaertmentRep
    {

        private readonly ApplicationContext db;

        public DepartmentRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Department>> GetAsync()
        {
            var data = await db.Department.ToListAsync();
            return data;
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            var data = await db.Department.Where(x => x.Id == id).FirstOrDefaultAsync();
            return data;

        }

        public async Task CreateAsync(Department obj)
        {
            await db.Department.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var OldData = db.Department.Find(id);
            db.Department.Remove(OldData);
            await db.SaveChangesAsync();
        }
    }
}
