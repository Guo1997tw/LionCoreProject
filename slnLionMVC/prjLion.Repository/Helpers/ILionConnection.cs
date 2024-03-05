using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Helpers
{
    /// <summary>
    /// 管理資料庫連線
    /// </summary>
    public interface ILionConnection
    {
        /// <summary>
        /// 取得LionHW資料庫連線
        /// </summary>
        /// <returns></returns>
        IDbConnection GetLionDb();
    }
}