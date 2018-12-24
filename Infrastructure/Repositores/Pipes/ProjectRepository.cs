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
    public class ProjectRepository : IProjectRepository
    {
        private readonly EFContext _db;

        public ProjectRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(Guid ma)
        {
            try
            {
                var tbl = await GetByMa(ma);
                _db.Projects.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Exist(string Name)
        {
            return await _db.Projects.AnyAsync(m => m.Name == Name);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _db.Projects.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<Project> GetByMa(Guid ma)
        {
            return await _db.Projects.FindAsync(ma);
        }

        public async Task<(IEnumerable<Project>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir)
        {
            var data = _db.Projects.AsQueryable();
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

        public async Task<bool> Save(Project tbl)
        {
            try
            {
                await _db.Projects.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Project tbl)
        {
            try
            {
                _db.Projects.Update(tbl);
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