using DataContext.WebCoreApp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly EFContext _db;

        public FunctionRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.Functions.FindAsync(ma);
                _db.Entry(tbl).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Functions>> GetAll()
        {
            var data = await _db.Functions.Where(m => m.ParentId == null)
                                .OrderBy(m => m.DisplayOrder).Select(m => new Functions
                                {
                                    Id = m.Id,
                                    Name = m.Name,
                                    IconCss = m.IconCss,
                                    Url = m.Url,
                                    DisplayOrder = m.DisplayOrder,
                                    Status = m.Status,
                                    InverseParent = _db.Functions.Where(t => t.ParentId == m.Id).OrderBy(t => t.DisplayOrder).ToList()
                                }).ToListAsync();

            return data;
        }

        public async Task<Functions> GetByMa(string ma)
        {
            return await _db.Functions.FindAsync(ma);
        }

        public async Task<bool> Save(Functions tbl)
        {
            try
            {
                await _db.Functions.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Functions tbl)
        {
            try
            {
                _db.Entry(tbl).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}