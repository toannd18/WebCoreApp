using DataContext.WebCoreApp;
using DataContext.WebCoreApp.Pipe;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces.Pipes;

namespace WebCoreApp.Infrastructure.Repositores.Pipes
{
    public class WelderRepository : IWelderRepository
    {
        private readonly EFContext _db;
        private readonly IHttpContextAccessor _httpContext;
        public WelderRepository(EFContext db, IHttpContextAccessor httpContext)
        {
            _db = db;
            _httpContext = httpContext;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.Welders.FindAsync(ma);
                _db.Welders.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Welder>> FindWelder(string id)
        {
            return await _db.Welders.Where(m => m.Id.ToUpper().Contains(id.ToUpper()) && m.Status).ToListAsync();
        }

        public async Task<IEnumerable<Welder>> GetAll()
        {
            return await _db.Welders.OrderBy(m => m.Id).ToListAsync();
        }

        public async Task<Welder> GetByMa(string ma)
        {
            return await _db.Welders.FindAsync(ma);
        }

        public async Task<(IEnumerable<Welder>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir)
        {
            var data = _db.Welders.AsQueryable();
            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.Id.ToUpper().Contains(search.ToUpper())
                || m.Name.ToUpper().Contains(search.ToUpper())
                || m.BrithDay.ToString("dd/MM/yyyy").Contains(search));
            }
            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "1":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Name) : data.OrderBy(m => m.Name));
                        break;
                    case "2":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.BrithDay) : data.OrderBy(m => m.BrithDay));
                        break;
                    case "3":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Status) : data.OrderBy(m => m.Status));
                        break;
                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Id) : data.OrderBy(m => m.Id));
                        break;
                }
            }

            int filterRecord = await data.CountAsync();
            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<bool> Save(Welder tbl)
        {
            try
            {
                string user = null;
                if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    user = _httpContext.HttpContext.User.Identity.Name;
                }
                tbl.UserCreated = user;
                tbl.UserUpdated = user;
                tbl.DateUpdated = DateTime.Now;
                tbl.DateCreated = DateTime.Now;
                _db.Entry(tbl).State = EntityState.Added;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Welder tbl)
        {
            try
            {
                string user = null;
                if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    user = _httpContext.HttpContext.User.Identity.Name;
                }
                var model = await GetByMa(tbl.Id);
                tbl.DateCreated = model.DateCreated;
                tbl.UserCreated = model.UserCreated;
                tbl.UserUpdated = user;
                tbl.DateUpdated = DateTime.Now;
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