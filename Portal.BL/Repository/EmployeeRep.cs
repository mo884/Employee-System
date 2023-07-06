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
    public class EmployeeRep : IEmployeeRep
    {

        private readonly ApplicationContext db;

        public EmployeeRep(ApplicationContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Employee>> GetAsync(Expression<Func<Employee, bool>> filter = null)
        {
            if (filter != null)
                return
                    await db.Employee
                            .Where(filter)
                            .Include("Department")
                            .Include("District")
                            .ToListAsync();
            else
               return
                    await db.Employee
                            .Include("Department")
                            .Include("District")
                            .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchAsync(Expression<Func<Employee, bool>> filter)
        {
            var data = await db.Employee
                                .Where(filter)
                                .Include("Department")
                                .Include("District")
                                .ToListAsync();
            return data;
        }


        public async Task<Employee> GetByIdAsync(Expression<Func<Employee, bool>> filter = null)
        {
            var data = await db.Employee
                                .Where(filter)
                                .Include("Department")
                                .Include("District")
                                .FirstOrDefaultAsync();
            return data;
        }

        public async Task<Employee> CreateAsync(Employee obj)
        {
            obj.CreationDate = DateTime.Now;
            await db.Employee.AddAsync(obj);
            await db.SaveChangesAsync();

            var result = await db.Employee.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task UpdateAsync(Employee obj)
        {
            obj.UpdateDate = DateTime.Now;
            obj.IsUpdated = true;

            db.Entry(obj).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee obj)
        {
            var OldData = db.Employee.Find(obj.Id);
            //OldData.IsDeleted = true;
            //OldData.DeleteDate = DateTime.Now;
            db.Employee.Remove(OldData);
            await db.SaveChangesAsync();
        }


    }

    
}
