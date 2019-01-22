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
    public class IsometricRepository : IIsometricRepository
    {
        private readonly EFContext _db;

        public IsometricRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.Isometrics.FindAsync(ma);
                _db.Isometrics.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Exist(string isoName, Guid project)
        {
            return await _db.Isometrics.AnyAsync(m => m.DrawName == isoName && m.Project == project);
        }

        public async Task<IEnumerable<Isometric>> GetAll()
        {
            return await _db.Isometrics.OrderBy(m => m.DrawName).ToListAsync();
        }

        public async Task<Isometric> GetByMa(string ma)
        {
            return await _db.Isometrics.FindAsync(ma);
        }

        public async Task<(IEnumerable<Isometric>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir)
        {
            var data = _db.Isometrics.AsQueryable();
            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.DrawName.ToUpper().Contains(search.ToUpper())
                || m.Unit.ToUpper().Contains(search.ToUpper())
                || m.Line.ToUpper().Contains(search.ToUpper())
                || m.Material.ToUpper().Contains(search.ToUpper())
                || m.Type.ToUpper().Contains(search.ToUpper())
                || m.PipeClass.ToUpper().Contains(search.ToUpper()));
            }
            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "3":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Unit) : data.OrderBy(m => m.Unit));
                        break;

                    case "4":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.PipeClass) : data.OrderBy(m => m.PipeClass));
                        break;

                    case "5":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Line) : data.OrderBy(m => m.Line));
                        break;

                    case "6":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Type) : data.OrderBy(m => m.Type));
                        break;

                    case "7":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Material) : data.OrderBy(m => m.Material));
                        break;

                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.DrawName) : data.OrderBy(m => m.DrawName));
                        break;
                }
            }

            int filterRecord = await data.CountAsync();
            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<bool> Save(Isometric tbl)
        {
            try
            {
                await _db.Isometrics.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Isometric tbl)
        {
            try
            {
                _db.Isometrics.Update(tbl);
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