using Portal.BL.Models;
using Portal.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BL.Interface
{
    public interface IDepaertmentRep
    {
        Task<IEnumerable<Department>> GetAsync();
        Task<Department> GetByIdAsync(int id);
        Task CreateAsync(Department obj);
        Task UpdateAsync(Department obj);
        Task DeleteAsync(int id);

    }
}
