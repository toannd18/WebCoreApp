using Dapper;
using DataContext.Drapper.Interface;
using DataContext.Drapper.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.Drapper.Implemention
{
    public class ReportService : IReportService
    {
        public readonly IConfiguration configuration;
        public ReportService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<IEnumerable<TotalBpViewModel>> GetTotalBpAsync(DateTime dateFrom, DateTime dateTo)
        {
            using(SqlConnection sqlconnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlconnection.OpenAsync();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@dateFrom", dateFrom);
                parameters.Add("@dateTo", dateTo);
                try
                {
                    return await sqlconnection.QueryAsync<TotalBpViewModel>(
                        "usp_GetTotalDairyByBP", parameters, commandType: CommandType.StoredProcedure);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
