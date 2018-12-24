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
    public class IsoJointRepository : IIsoJointRepository
    {
        private readonly EFContext _db;

        public IsoJointRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(Guid ma)
        {
            try
            {
                var tbl = await _db.IsoJoints.FindAsync(ma);
                _db.IsoJoints.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ExistJoint(string joint, string drawName)
        {
            return _db.IsoJoints.Any(m => m.Joint == joint & m.DrawName == drawName);
        }

        public Task<IEnumerable<IsoJoint>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IsoJoint> GetByMa(Guid ma)
        {
            return await _db.IsoJoints.FindAsync(ma);
        }

        public async Task<(IEnumerable<IsoJoint>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir, string isoMetric)
        {
            var data = _db.IsoJoints.Where(m => m.DrawName == isoMetric).AsQueryable();
            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.Joint.ToUpper().Contains(search.ToUpper())
                || m.TypeJoint.ToUpper().Contains(search.ToUpper())
                || m.Rev.ToUpper().Contains(search.ToUpper())
                || m.Heate1.ToUpper().Contains(search.ToUpper())
                || m.Heate2.ToUpper().Contains(search.ToUpper())
                || m.SF.ToString() == search
                || m.Status.ToString() == search);
            }
            //if (!string.IsNullOrEmpty(oderId))
            //{
            //    switch (oderId)
            //    {
            //        case "3":
            //            data = (oderDir == "desc" ? data.OrderByDescending(m => m.Unit) : data.OrderBy(m => m.Unit));
            //            break;

            //        case "4":
            //            data = (oderDir == "desc" ? data.OrderByDescending(m => m.PipeClass) : data.OrderBy(m => m.PipeClass));
            //            break;

            //        case "5":
            //            data = (oderDir == "desc" ? data.OrderByDescending(m => m.Line) : data.OrderBy(m => m.Line));
            //            break;

            //        case "6":
            //            data = (oderDir == "desc" ? data.OrderByDescending(m => m.Type) : data.OrderBy(m => m.Type));
            //            break;

            //        case "7":
            //            data = (oderDir == "desc" ? data.OrderByDescending(m => m.Material) : data.OrderBy(m => m.Material));
            //            break;

            //        default:
            //            data = (oderDir == "desc" ? data.OrderByDescending(m => m.DrawName) : data.OrderBy(m => m.DrawName));
            //            break;
            //    }
            // }

            int filterRecord = await data.CountAsync();
            var model = await data.OrderBy(m => m.Joint).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<IEnumerable<IsoJoint>> GetWelding(string isoName, bool weldingDate)
        {
            var tbl = _db.IsoJoints.AsQueryable();
            if (weldingDate)
            {
                tbl = tbl.Where(m => m.DrawName == isoName & m.WeldingDate.HasValue).OrderBy(m => m.Joint);
            }
            else
            {
                tbl = tbl.Where(m => m.DrawName == isoName & !m.WeldingDate.HasValue).OrderBy(m => m.Joint);
            }

            return await tbl.ToListAsync();
        }

        public async Task<bool> Save(IsoJoint tbl)
        {
            try
            {
                tbl.Id = new Guid();
                await _db.IsoJoints.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(IsoJoint tbl)
        {
            try
            {
                _db.IsoJoints.Update(tbl);
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