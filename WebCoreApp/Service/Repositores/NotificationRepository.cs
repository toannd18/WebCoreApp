using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataContext.WebCoreApp;
using WebCoreApp.Models;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly EFContext _db;

        public NotificationRepository(EFContext db)
        {
            _db = db;
        }

        public Task<bool> Delete(long ma)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TblNotifications>> GetAll()
        {
            return await _db.TblNotifications.ToListAsync();
        }

        public async Task<TblNotifications> GetByMa(long ma)
        {
            return await _db.TblNotifications.FindAsync(ma);
        }

        public async Task<(IEnumerable<ViewNotificationModel>, int TotalRecords)> GetNotification(string username)
        {
            var model = await (from n in _db.TblNotifications
                               join u1 in _db.AppUser on n.SendId equals u1.UserName
                               join u2 in _db.AppUser on n.ReceiveId equals u2.UserName
                               where !n.Status && u2.UserName==username
                               
                               select new ViewNotificationModel
                               {
                                   Id = n.Id,
                                   NameReceiveId = u2.FullName,
                                   NameSend = u1.FullName,
                                   Notification = n.Notification,
                                   Date = n.Date,
                                   Url = n.Url,
                                   Status = n.Status
                               }).OrderByDescending(m=>m.Date).ToListAsync();
             return (model.Take(3).ToList(), model.Count);
        }

        public async Task<bool> Save(TblNotifications tbl)
        {
            try
            {
                _db.Entry(tbl).State = EntityState.Added;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SaveList(List<TblNotifications> model)
        {
            try
            {
                await _db.AddRangeAsync(model);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(TblNotifications tbl)
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