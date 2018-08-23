using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Data;
using WebCoreApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebCoreApp.Service.Repositores
{
    public class BPRepository : IBPRepository
    {
        private readonly EFContext _db;

        public BPRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.TblBp.FindAsync(ma);
                _db.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TblBp>> GetAll()
        {
            
            return await _db.TblBp.OrderBy(m=>m.MaBp).ToListAsync();
        }

        public async Task<TblBp> GetByMa(string ma)
        {
            return await _db.TblBp.FindAsync(ma);
        }

        public async Task<bool> Save(TblBp tbl)
        {
            try
            {
                _db.Entry(tbl).State =  EntityState.Added;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(TblBp tbl)
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