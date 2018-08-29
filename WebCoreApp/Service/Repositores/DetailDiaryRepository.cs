using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreApp.Areas.Diaries.Models;
using DataContext.WebCoreApp;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class DetailDiaryRepository : IDetailDiaryRepository
    {
        private readonly EFContext _db;

        public DetailDiaryRepository(EFContext db)
        {
            _db = db;
        }

        public void Calculator(int ma)
        {
            DbFunctions dbF = null;
            DateTime date = DateTime.Now;
            var cal = (from d in _db.TblDailyDetail
                       join j in _db.TblJob on d.JobId equals j.Id
                       where d.DailyId == ma
                       group d by new { d.JobId, j.TenJob } into g
                       select new
                       {
                           JobId = g.Key.JobId,
                           Ten_Job = g.Key.TenJob,
                           Sum = (g.Sum(m => SqlServerDbFunctionsExtensions.DateDiffMinute(dbF, date.Add(m.FormTime), date.Add(m.ToTime)))) * 100 / 480
                       }).ToList();
            string Total_Job = "";
            if (cal.Count > 0)
            {
                foreach (var item in cal)
                {
                    Total_Job = Total_Job + "- " + item.Ten_Job + "(" + item.Sum + "%)</br>";
                }
            }
            if (!string.IsNullOrWhiteSpace(Total_Job))
            {
                var model = _db.TblDaily.Find(ma);
                model.TotalJob = Total_Job;
            };
            _db.SaveChanges();
        }

        public async Task<bool> Delete(long ma)
        {
            try

            {
                TblDailyDetail tbl = new TblDailyDetail();
                tbl.Id = ma;
                _db.Entry(tbl).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                Calculator(tbl.DailyId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TblDailyDetail>> GetAll()
        {
            return await _db.TblDailyDetail.ToListAsync();
        }

        public async Task<TblDailyDetail> GetByMa(long ma)
        {
            return await _db.TblDailyDetail.FindAsync(ma);
        }

        public async Task<(IEnumerable<ViewDetailDiaryModel>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir, int Id)
        {
            var data = (from dd in _db.TblDailyDetail
                        join d in _db.TblDaily on dd.DailyId equals d.Id
                        join j in _db.TblJob on dd.JobId equals j.Id
                        where d.Id == Id
                        select new ViewDetailDiaryModel
                        {
                            Id = dd.Id,
                            FromTime = dd.FormTime,
                            ToTime = dd.ToTime,
                            Content_Job = dd.ContentJob,
                            Method = dd.Method,
                            Result = dd.Result,
                            DailyId = d.Id,
                            JobId = dd.JobId,
                            Total_Job = d.TotalJob,
                            Comment1 = dd.Comment1,
                            Comment2 = dd.Comment2,
                            Comment3 = dd.Comment3,
                            Level_1 = dd.Level1,
                            Level_2 = dd.Level2,
                            Level_3 = dd.Level3,
                            Ten_Job = j.TenJob
                        });

            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "0":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.FromTime) : data.OrderBy(m => m.FromTime));
                        break;

                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.ToTime) : data.OrderBy(m => m.ToTime));
                        break;
                }
            }

            int filterRecord = await data.CountAsync();

            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<bool> Save(TblDailyDetail tbl)
        {
            try
            {
                _db.Entry(tbl).State = EntityState.Added;
                await _db.SaveChangesAsync();
                Calculator(tbl.DailyId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveCommentAll(ViewCommentModel tbl, int display)
        {
            try
            {
                IEnumerable<TblDailyDetail> model = await _db.TblDailyDetail.Where(m => m.DailyId == tbl.idDaily).ToListAsync();
                foreach (var item in model)
                {
                    if (display == 3)
                    {
                        item.Level1 = tbl.level;
                        item.Comment1 = tbl.comment;
                    }
                    else if (display == 4)
                    {
                        item.Level2 = tbl.level;
                        item.Comment2 = tbl.comment;
                    }
                    else if (display > 4)
                    {
                        item.Level3 = tbl.level;
                        item.Comment3 = tbl.comment;
                    }
                }
               
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(TblDailyDetail tbl)
        {
            try
            {
                _db.Entry(tbl).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                Calculator(tbl.DailyId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}