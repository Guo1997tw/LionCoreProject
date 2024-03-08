using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        //public LionConnection(IConfiguration configuration)
        //{
        //    _connectionString = configuration.GetConnectionString("LionHW");
        //}


        /// <summary>
        /// 使用Options方式取得DB連線字串
        /// </summary>
        /// <param name="options"></param>
        public LionConnection(IOptions<ConnectionStringOptionsModel> options)
        {
            _connectionString = options.Value.LionHW ?? throw new ArgumentNullException(nameof(options.Value.LionHW), "LionHW connection string is not configured.");
        }

        /// <summary>
        /// 進行SQL Server連線
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetLionDb() => new SqlConnection(_connectionString);
    }
}