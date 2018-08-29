using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCoreApp.Areas.Reports.Models;
using WebCoreApp.Data;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class ReportDiaryRepository : IReportDiaryRepository
    {
        private readonly EFContext _db;

        public ReportDiaryRepository(EFContext db)
        {
            _db = db;
        }

        public IQueryable<ViewReportDiaryModel> GetData(DateTime dateFrom, DateTime dateTo)
        {
            DbFunctions dbF = null;
            DateTime date = DateTime.Now;
            var data =
                from p in _db.TblBp
                join u in _db.AppUser on p.MaBp equals u.MaBp
                join t in _db.TblTo on u.MaTo equals t.MaTo into gt
                from subt in gt.DefaultIfEmpty()

                join gd in (from d in _db.TblDaily
                            where d.Date >= dateFrom && d.Date <= dateTo
                            select d
                            ) on u.UserName equals gd.UserName 
                                    into gu from subd in gu.DefaultIfEmpty()

                join gdd in (from dd in _db.TblDailyDetail
                            group dd by dd.DailyId into g
                            select new
                            {
                                DailyId = g.Key,
                                Total_Work = (g.Sum(m => SqlServerDbFunctionsExtensions.DateDiffMinute(dbF, date.Add(m.FormTime), date.Add(m.ToTime)))),
                            }) on subd.Id equals gdd.DailyId into gud
                from subdd in gud.DefaultIfEmpty()
                where u.MaBp !="BGD"
                select new ViewReportDiaryModel
                {
                    UserName = u.UserName,
                    FullName = u.FullName,
                    Ten_phong = p.TenBp,
                    Ten_to = subt.TenTo,
                    Total_Work = subdd.Total_Work > 0 ? subdd.Total_Work : 0,
                    Total_Date = subd.Id > 0 ? subd.Id : 0
                };

            return data;
        }
    }
}