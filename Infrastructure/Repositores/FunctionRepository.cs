using DataContext.WebCoreApp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCoreApp.Infrastructure.Interfaces;
using WebCoreApp.Infrastructure.ViewModels.System;

namespace WebCoreApp.Service.Repositores
{
    public class FunctionRepository : IFunctionRepository
    {
        private readonly EFContext _db;
        private readonly RoleManager<AppRole> roleManager;

        public FunctionRepository(EFContext db, RoleManager<AppRole> roleManager)
        {
            _db = db;
            this.roleManager = roleManager;
        }

        public async Task<bool> Delete(string ma)
        {
            try
            {
                var tbl = await _db.Functions.FindAsync(ma);
                _db.Entry(tbl).State = EntityState.Deleted;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Functions>> GetAll()
        {
            var data = await _db.Functions.Where(m => m.ParentId == null)
                                .OrderBy(m => m.DisplayOrder).Select(m => new Functions
                                {
                                    Id = m.Id,
                                    Name = m.Name,
                                    IconCss = m.IconCss,
                                    Url = m.Url,
                                    DisplayOrder = m.DisplayOrder,
                                    Status = m.Status,
                                    InverseParent = _db.Functions.Where(t => t.ParentId == m.Id).OrderBy(t => t.DisplayOrder).Select(t => new Functions
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        IconCss = t.IconCss,
                                        Url = t.Url,
                                        DisplayOrder = t.DisplayOrder,
                                        Status = t.Status,
                                        InverseParent = _db.Functions.Where(g => g.ParentId == t.Id).OrderBy(g => g.DisplayOrder).ToList()
                                    }).ToList()
                                }).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<Functions>> GetAllId()
        {
            return await _db.Functions.Where(m => m.Status == true).OrderBy(m => m.Url).OrderBy(m => m.DisplayOrder).ToListAsync();
        }

        public async Task<Functions> GetByMa(string ma)
        {
            return await _db.Functions.FindAsync(ma);
        }

        public async Task<bool> Save(Functions tbl)
        {
            try
            {
                await _db.Functions.AddAsync(tbl);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Functions tbl)
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

        public async Task UpdatePermission(CustomPermissionViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Role);

            var funct = await _db.Functions.Where(m => m.Id == model.Id)
                                .OrderBy(m => m.DisplayOrder).Select(m => new Functions
                                {
                                    Id = m.Id,
                                    Name = m.Name,
                                    IconCss = m.IconCss,
                                    Url = m.Url,
                                    DisplayOrder = m.DisplayOrder,
                                    Status = m.Status,
                                    InverseParent = _db.Functions.Where(t => t.ParentId == m.Id).OrderBy(t => t.DisplayOrder).Select(t => new Functions
                                    {
                                        Id = t.Id,
                                        Name = t.Name,
                                        IconCss = t.IconCss,
                                        Url = t.Url,
                                        DisplayOrder = t.DisplayOrder,
                                        Status = t.Status,
                                        InverseParent = _db.Functions.Where(g => g.ParentId == t.Id).OrderBy(g => g.DisplayOrder).ToList()
                                    }).ToList()
                                }).ToListAsync();
            // Xóa Claim cũ
            foreach (var item in funct)
            {
                var listClaim = await _db.RoleClaims.Where(m => m.RoleId == role.Id && m.ClaimType == item.Id).ToListAsync();
                if (listClaim.Count > 0)
                {
                    _db.RoleClaims.RemoveRange(listClaim);
                }

                foreach (var item1 in item.InverseParent)
                {
                    var listClaim1 = await _db.RoleClaims.Where(m => m.RoleId == role.Id && m.ClaimType == item1.Id).ToListAsync();
                    if (listClaim1.Count > 0)
                    {
                        _db.RoleClaims.RemoveRange(listClaim1);
                    }
                    foreach (var item2 in item1.InverseParent)
                    {
                        var listClaim2 = await _db.RoleClaims.Where(m => m.RoleId == role.Id && m.ClaimType == item2.Id).ToListAsync();
                        if (listClaim2.Count > 0)
                        {
                            _db.RoleClaims.RemoveRange(listClaim2);
                        }
                    }
                }
            }
            await _db.SaveChangesAsync();
            // Tạo Claim mới
            foreach (var item in funct)
            {
                if (model.View)
                    await roleManager.AddClaimAsync(role, new Claim(type: item.Id, value: "View"));
                if (model.Edit)
                    await roleManager.AddClaimAsync(role, new Claim(type: item.Id, value: "Edit"));
                if (model.Delete)
                    await roleManager.AddClaimAsync(role, new Claim(type: item.Id, value: "Delete"));
                foreach (var item1 in item.InverseParent)
                {
                    if (model.View)
                        await roleManager.AddClaimAsync(role, new Claim(type: item1.Id, value: "View"));
                    if (model.Edit)
                        await roleManager.AddClaimAsync(role, new Claim(type: item1.Id, value: "Edit"));
                    if (model.Delete)
                        await roleManager.AddClaimAsync(role, new Claim(type: item1.Id, value: "Delete"));
                    foreach (var item2 in item1.InverseParent)
                    {
                        if (model.View)
                            await roleManager.AddClaimAsync(role, new Claim(type: item2.Id, value: "View"));
                        if (model.Edit)
                            await roleManager.AddClaimAsync(role, new Claim(type: item2.Id, value: "Edit"));
                        if (model.Delete)
                            await roleManager.AddClaimAsync(role, new Claim(type: item2.Id, value: "Delete"));
                    }
                }
            }
        }
    }
}