using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCoreApp.Areas.Diaries.Models;
using DataContext.WebCoreApp;
using WebCoreApp.Extensions.Signlar;
using WebCoreApp.Service.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class DiaryRepository : IDiaryRepository
    {
        private readonly EFContext _db;
        private readonly IHttpContextAccessor _context;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<PTSCHub> _hubContext;

        public DiaryRepository(EFContext db, IHttpContextAccessor context, INotificationRepository notificationRepository, IHubContext<PTSCHub> hubContext)
        {
            _db = db;
            _context = context;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        public async Task<bool> Delete(int ma)
        {
            try
            {
                TblDaily tbl = new TblDaily();
                tbl.Id = ma;
                _db.Entry(tbl).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<TblDaily>> GetAll()
        {
            return await _db.TblDaily.OrderBy(m => m.Date).ToListAsync();
        }

        public async Task<TblDaily> GetByMa(int ma)
        {
            return await _db.TblDaily.FindAsync(ma);
        }

        public async Task<bool> Save(TblDaily tbl)
        {
            try
            {
                tbl.UserName = _context.HttpContext.User.Identity.Name.ToString();
                _db.Entry(tbl).State = EntityState.Added;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(TblDaily tbl)
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

        public async Task<(IEnumerable<ViewDiaryModel>, int totalRecords, int filterRecord)> GetTable(int length, int page, string search, string oderId, string oderDir, string maBp, string maTo)
        {
            string userBp = _context.HttpContext.User.FindFirst("Ma_BP").Value;
            string userTO = _context.HttpContext.User.FindFirst("Ma_To").Value;
            int userCV = int.Parse(_context.HttpContext.User.FindFirst("Display").Value);

            var data = (from d in _db.TblDaily
                        join u in _db.AppUser on d.UserName equals u.UserName
                        join c in _db.TblCv on u.MaCv equals c.MaCv
                        where (userTO == "" ? 1 == 1 : u.MaTo == userTO)
                                && (userCV > 5 ? 1 == 1 : u.MaBp == userBp)
                                && c.Display < userCV && d.StatusAutho1
                                && (maBp == "" ? 1 == 1 : u.MaBp == maBp)
                                && (maTo == "" ? 1 == 1 : u.MaTo == maTo)
                        join u1 in _db.AppUser on d.UserAutho1 equals u1.UserName into gu
                        from su1 in gu.DefaultIfEmpty()
                        orderby d.Date descending, u.UserName ascending
                        select new ViewDiaryModel
                        {
                            Id = d.Id,
                            Date = d.Date,
                            TotalJob = d.TotalJob,
                            UserName = d.UserName,
                            UserAutho1 = d.UserAutho1,
                            FullName = u.FullName,
                            FullName1 = su1.FullName,
                            StatusAutho1 = d.StatusAutho1
                        });

            int totalRecords = await data.CountAsync();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.UserName.ToLower().Contains(search.ToLower())
                                    || m.FullName.ToLower().Contains(search.ToLower())
                                    || m.UserAutho1.ToLower().Contains(search.ToLower())
                                    || m.FullName1.ToLower().Contains(search.ToLower()));
                if (DateTime.TryParse(search, out DateTime dt))
                {
                    data = data.Where(m => m.Date == dt);
                }
            }
            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "0":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Date) : data.OrderBy(m => m.Date));
                        break;

                    case "1":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.UserName) : data.OrderBy(m => m.UserName));
                        break;

                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.UserAutho1).ThenByDescending(m => m.StatusAutho1) : data.OrderBy(m => m.UserAutho1).ThenBy(m => m.StatusAutho1));
                        break;
                }
            }
            int filterRecord = await data.CountAsync();
            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<(IEnumerable<ViewDiaryModel>, int totalRecords, int filterRecord)> UserGetTable(int length, int page, string search, string oderId, string oderDir)
        {
            string user = _context.HttpContext.User.Identity.Name.ToString();

            var data = (from d in _db.TblDaily
                        join u in _db.AppUser on d.UserName equals u.UserName
                        join c in _db.TblCv on u.MaCv equals c.MaCv
                        where u.UserName == user
                        join u1 in _db.AppUser on d.UserAutho1 equals u1.UserName into gu
                        from su1 in gu.DefaultIfEmpty()
                        select new ViewDiaryModel
                        {
                            Id = d.Id,
                            Date = d.Date,
                            TotalJob = d.TotalJob,
                            UserName = d.UserName,
                            UserAutho1 = d.UserAutho1,
                            FullName = u.FullName,
                            FullName1 = su1.FullName,
                            StatusAutho1 = d.StatusAutho1
                        });

            int totalRecords = await data.CountAsync();

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.UserName.ToLower().Contains(search.ToLower())
                                    || m.FullName.ToLower().Contains(search.ToLower())
                                    || m.UserAutho1.ToLower().Contains(search.ToLower())
                                    || m.FullName1.ToLower().Contains(search.ToLower()));
                if (DateTime.TryParse(search, out DateTime dt))
                {
                    data = data.Where(m => m.Date == dt);
                }
            }

            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "0":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Date) : data.OrderBy(m => m.Date));
                        break;

                    case "1":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.UserName) : data.OrderBy(m => m.UserName));
                        break;

                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.UserAutho1).OrderByDescending(m => m.StatusAutho1) : data.OrderBy(m => m.UserAutho1).OrderBy(m => m.StatusAutho1));
                        break;
                }
            }

            int filterRecord = await data.CountAsync();

            var model = await data.Skip(page).Take(length).ToListAsync();

            return (model, totalRecords, filterRecord);
        }

        public async Task<ViewDiaryModel> GetViewByMa(int ma)
        {
            var data = await (from d in _db.TblDaily
                              join u in _db.AppUser on d.UserName equals u.UserName
                              join c in _db.TblCv on u.MaCv equals c.MaCv
                              join t in _db.TblTo on u.MaTo equals t.MaTo
                              join cv in _db.TblCv on u.MaCv equals cv.MaCv
                              join u1 in _db.AppUser on d.UserAutho1 equals u1.UserName into gu
                              from su1 in gu.DefaultIfEmpty()
                              where d.Id == ma
                              select new ViewDiaryModel
                              {
                                  Id = d.Id,
                                  Date = d.Date,
                                  TotalJob = d.TotalJob,
                                  UserName = d.UserName,
                                  UserAutho1 = d.UserAutho1,
                                  FullName = u.FullName,
                                  FullName1 = su1.FullName,
                                  StatusAutho1 = d.StatusAutho1,
                                  TenTo = t.TenTo,
                                  TenCV = cv.TenCv
                              }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<bool> SendDiary(int id)
        {
            try
            {
                List<TblNotifications> notifications = new List<TblNotifications>();
                string bp = _context.HttpContext.User.FindFirst("Ma_Bp").Value;
                string to = _context.HttpContext.User.FindFirst("Ma_To").Value;
                string user = _context.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
                int display = int.Parse(_context.HttpContext.User.FindFirst("Display").Value);
                DateTime nowDate = DateTime.Now;
                List<string> userReceive = new List<string>();
                TblDaily  tbl=await _db.TblDaily.FindAsync(id);
                if (display < 3)
                {
                    userReceive = await (from u in _db.AppUser
                                   join cv in _db.TblCv on u.MaCv equals cv.MaCv
                                   where u.MaTo == to && cv.Display==3
                                   select u.UserName).ToListAsync();
                    tbl.StatusAutho1 = true;
                   
                }
                else if(display>2)
                {
                    userReceive = await (from u in _db.AppUser
                                         join cv in _db.TblCv on u.MaCv equals cv.MaCv
                                         where cv.Display>display &&(display<4?u.MaBp==bp:1==1)
                                         select u.UserName).ToListAsync();
                    tbl.StatusAutho2 = true;
                    tbl.StatusAutho3 = true;
                }
                foreach (string item in userReceive) { 
                    notifications.Add(new TblNotifications
                    {
                        SendId = user,
                        ReceiveId=item,
                        Notification="Nhật ký công việc",
                        Url= "/Diaries/Evaluates/Index/" + id,
                        Status=false,
                        Date= nowDate
                    });
       

                }
                bool status= await _notificationRepository.SaveList(notifications);
                if (status)
                {
                    await _hubContext.Clients.Users(userReceive).SendAsync("GetNotification");
                }
                return status;
            }
            catch
            {
                return false;
            }
        }
    }
}