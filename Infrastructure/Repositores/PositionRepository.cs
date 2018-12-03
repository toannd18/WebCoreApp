using DataContext.WebCoreApp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class PositionRepository : IPositionRepository
    {
        private readonly EFContext _db;

        public PositionRepository(EFContext db)
        {
            _db = db;
        }

        public Task<bool> Delete(string ma)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TblCv>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<TblCv> GetByMa(string ma)
        {
            return await _db.TblCv.FindAsync(ma);
        }

        public Task<bool> Save(TblCv tbl)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TblCv tbl)
        {
            throw new NotImplementedException();
        }
    }
}