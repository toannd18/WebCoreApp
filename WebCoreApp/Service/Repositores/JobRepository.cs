using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataContext.WebCoreApp;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class JobRepository : IJobRepository
    {
        private readonly EFContext _db;
        public JobRepository(EFContext db)
        {
            _db = db;
        }
        public Task<bool> Delete(int ma)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblJob>> GetAll()
        {
            return await _db.TblJob.ToListAsync();
        }

        public Task<TblJob> GetByMa(int ma)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblJob>> GetJobList(string to, string bp)
        {
            return await _db.TblJob.Where(m => m.MaTo == to && m.MaBp == bp).ToListAsync();
        }

        public Task<bool> Save(TblJob tbl)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(TblJob tbl)
        {
            throw new NotImplementedException();
        }
    }
}
