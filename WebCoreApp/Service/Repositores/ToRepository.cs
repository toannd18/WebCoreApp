using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext.WebCoreApp;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class ToRepository : IToRepository
    {
        private readonly EFContext _db;

        public ToRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.TblTo.FindAsync(ma);
                _db.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TblTo>> GetAll()
        {
            return await _db.TblTo.OrderBy(m=>m.MaTo).ThenBy(m=>m.MaBp).ToListAsync();
        }

        public async Task<TblTo> GetByMa(string ma)
        {

            return await _db.TblTo.FindAsync(ma);
        }

        public async Task<bool> Save(TblTo tbl,bool update)
        {
            try
            {
                _db.Entry(tbl).State = update ? EntityState.Modified : EntityState.Added;
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