using Dapper;
using DataContext.Drapper.Interface;
using DataContext.Drapper.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataContext.Drapper.Implemention
{
    public class WareHouseService : IWareHouseService
    {
        private readonly IConfiguration configuration;

        public WareHouseService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IEnumerable<DMVTViewModel>> GetDMVT(string nhomVT)
        {
            using (SqlConnection sqlconnection = new SqlConnection(configuration.GetConnectionString("WareHouseConnect")))
            {
                await sqlconnection.OpenAsync();
                string querystr = "select ma_vt as MaVT, ten_vt as TenVT, ";
                querystr += "dvt as DVT, nh_vt1 as NhomVT, ghi_chu as GhiChu ";
                querystr += "from dmvt ";
                if (!string.IsNullOrEmpty(nhomVT))
                {
                    querystr += "where nh_vt1 like '" + nhomVT + "'";
                }
                try
                {
                    return await sqlconnection.QueryAsync<DMVTViewModel>(querystr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<IEnumerable<NhomVTViewModel>> GetNhomVT()
        {
            using (SqlConnection sqlconnection = new SqlConnection(configuration.GetConnectionString("WareHouseConnect")))
            {
                await sqlconnection.OpenAsync();
                string querystr = "select ma_nh as MaNhom, ten_nh as TenNhom from dmnhvt where status ='1'";

                try
                {
                    return await sqlconnection.QueryAsync<NhomVTViewModel>(querystr);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}