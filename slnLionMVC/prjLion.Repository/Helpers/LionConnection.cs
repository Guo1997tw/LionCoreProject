using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Helpers
{
    public class LionConnection : ILionConnection
    {
        private readonly string? _connectionString;

        /// <summary>
        /// 取得application.json DB連線字串
        /// </summary>
        /// <param name="configuration"></param>
        public LionConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LionHW");
        }

        /// <summary>
        /// 進行SQL Server連線
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetLionDb() => new SqlConnection(_connectionString);
    }
}