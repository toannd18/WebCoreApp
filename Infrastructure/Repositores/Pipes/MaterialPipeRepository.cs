using DataContext.WebCoreApp;
using DataContext.WebCoreApp.Pipe;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;

namespace WebCoreApp.Infrastructure.Repositores.Pipes
{
    public class MaterialPipeRepository : IMaterialPipeRepository
    {
        private readonly EFContext _db;

        public MaterialPipeRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.MaterialPipes.FindAsync(ma);
                _db.MaterialPipes.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<MaterialPipe>> Find(string code)
        {
            return await _db.MaterialPipes.Where(m => m.Name.ToUpper().Contains(code.ToUpper())).ToListAsync();
        }

        public async Task<IEnumerable<MaterialPipe>> GetAll()
        {
            return await _db.MaterialPipes.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<MaterialPipe> GetByMa(string ma)
        {
            return await _db.MaterialPipes.FindAsync(ma);
        }

        public async Task<(IEnumerable<MaterialPipe>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir)
        {

            var data = _db.MaterialPipes.AsQueryable();
            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.Name.ToUpper().Contains(search.ToUpper())
                || m.Description.ToUpper().Contains(search.ToUpper()));
            }
            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "2":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Description) : data.OrderBy(m => m.Description));
                        break;

                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Name) : data.OrderBy(m => m.Name));
                        break;
                }
            }

            int filterRecord = await data.CountAsync();
            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<bool> Save(MaterialPipe tbl)
        {
            try
            {
                await _db.MaterialPipes.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(MaterialPipe tbl)
        {
            try
            {
                _db.MaterialPipes.Update(tbl);
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