using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class BaseRepository
    {
        protected readonly IConfiguration _configuration;
        protected readonly int _pageSize;
        protected readonly int _exportReportSize;

        public BaseRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
            // Safely parse the PageSize from configuration
            if (!int.TryParse(configuration["PageSize"], out _pageSize))
            {
                _pageSize = 10; // Default value if parsing fails
            }
            if (!int.TryParse(configuration["ExportReportSize"], out _exportReportSize))
            {
                _pageSize = 10000; // Default value if parsing fails
            }
        }

        public async Task<List<T>> QueryAsync<T>(string sql)
        {
            using (var oracleConnection = new SqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
            {
                var clientStatus = await oracleConnection.QueryAsync<T>(sql);
                return clientStatus.ToList();
            }
        }
        public async Task<List<T>> QueryAsyncWithParams<T>(string sql, object param)
        {
            using (var oracleConnection = new SqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
            {
                var clientStatus = await oracleConnection.QueryAsync<T>(sql, param);
                return clientStatus.ToList();
            }
        }

        public async Task<T?> QueryFirstOrDefaultAsync<T>(string sql)
        {
            using (var oracleConnection = new SqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
            {
                return await oracleConnection.QueryFirstOrDefaultAsync<T>(sql);
            }
        }
        public async Task<T?> QueryFirstOrDefaultWithParamsAsync<T>(string sql, object param)
        {
            using (var oracleConnection = new SqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
            {
                return await oracleConnection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
        }
    }

}
