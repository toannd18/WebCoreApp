using DataContext.WebCoreApp;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Enume;
using WebCoreApp.Infrastructure.Interfaces;

namespace WebCoreApp.Service.Repositores
{
    public class UserRepository : IUserRepository
    {
        private readonly EFContext _db;

        public UserRepository(EFContext db)
        {
            _db = db;
        }

        public async Task<bool> Delete(Guid user)
        {
            try
            {
                var tbl = await _db.AppUser.Where(m => m.Id == user).FirstOrDefaultAsync();
                _db.Remove(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<AppUser>> GetAll()
        {
            var tbl = await _db.AppUser.OrderBy(m => m.UserName).ToListAsync();

            return tbl;
        }

        public async Task<bool> Save(AppUser tbl)
        {
            try
            {
                _db.Entry(tbl).State = tbl.Id == null ? EntityState.Added : EntityState.Modified;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Status> IsUser(string user, string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            byte[] result = md5.Hash;

            StringBuilder str = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                str.Append(result[i].ToString("x2"));
            }
            password = str.ToString();
            var Check = await _db.AppUser.AnyAsync(m => m.UserName == user & m.PasswordHash == password);
            if (Check)
            {
                return Status.Successed;
            }
            return Status.Rejected;
        }

        public async Task<(IEnumerable<AppUser>, int totalRecords, int filterRecord)> GetTable(int start, int lenght, string oderId, string oderDir, string search)
        {
            var data = await GetAll();
            int totals = data.Count();
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(m => m.UserName.ToLower().Contains(search.ToLower())
                                      || m.FullName.ToLower().Contains(search.ToLower())
                                      || m.Email.ToLower().Contains(search.ToLower())).ToList();
                if (search == "nam" || search == "nữ")
                {
                    data = data.Where(m => search == "nam" ? m.Gender : m.Gender == false).ToList();
                }
            }
            if (!string.IsNullOrEmpty(oderId))
            {
                switch (oderId)
                {
                    case "0":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.UserName).ToList() : data.OrderBy(m => m.UserName).ToList());
                        break;

                    case "1":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.FullName).ToList() : data.OrderBy(m => m.FullName).ToList());
                        break;

                    case "2":
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Gender).ToList() : data.OrderBy(m => m.Gender).ToList());
                        break;

                    default:
                        data = (oderDir == "desc" ? data.OrderByDescending(m => m.Email).ToList() : data.OrderBy(m => m.Email).ToList());
                        break;
                }
            }

            int filters = data.Count();
            data = data.Skip(start).Take(lenght).ToList();
            return (data, totals, filters);
        }

        public async Task<AppUser> GetByMa(Guid ma)
        {
            return await _db.AppUser.Where(m => m.Id == ma).FirstOrDefaultAsync();
        }

        public Task<bool> Update(AppUser tbl)
        {
            throw new NotImplementedException();
        }

        public async Task<Status> RemoveFromRole(Guid user, Guid role)
        {
            var table = await _db.UserRoles.Where(m => m.RoleId == role && m.UserId == user).FirstAsync();
            try
            {
                _db.UserRoles.Remove(table);
                await _db.SaveChangesAsync();
                return Status.Successed;
            }
            catch
            {
                return Status.Rejected;
            }
        }
    }
}