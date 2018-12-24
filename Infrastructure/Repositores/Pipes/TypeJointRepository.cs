using DataContext.WebCoreApp;
using DataContext.WebCoreApp.Pipe;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;

namespace WebCoreApp.Infrastructure.Repositores.Pipes
{
    public class TypeJointRepository : ITypeJointRepository
    {
        private readonly EFContext _db;

        public TypeJointRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.TypeJoints.FindAsync(ma);
                _db.Entry(tbl).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TypeJoint>> Find(string id)
        {
            return await _db.TypeJoints.Where(m => m.Type.ToUpper().Contains(id.ToUpper())).ToListAsync();
        }

        public async Task<IEnumerable<TypeJoint>> GetAll()
        {
            return await _db.TypeJoints.OrderBy(m => m.Type).ToListAsync();
        }

        public async Task<TypeJoint> GetByMa(string ma)
        {
            return await _db.TypeJoints.FindAsync(ma);
        }

        public async Task<(IEnumerable<TypeJoint>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir)
        {
            var data = _db.TypeJoints.AsQueryable();
            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.Type.ToUpper().Contains(search.ToUpper())
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
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Type) : data.OrderBy(m => m.Type));
                        break;
                }
            }

            int filterRecord = await data.CountAsync();
            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<bool> Save(TypeJoint tbl)
        {
            try
            {
                await _db.TypeJoints.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(TypeJoint tbl)
        {
            try
            {
                _db.TypeJoints.Update(tbl);
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