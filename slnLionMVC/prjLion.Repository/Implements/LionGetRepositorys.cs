﻿using Dapper;
using prjLion.Repository.Helpers;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Implements
{
    public class LionGetRepositorys : ILionGetRepositorys
    {
        private readonly ILionConnection _lionConnection;

        public LionGetRepositorys(ILionConnection lionConnection)
        {
            _lionConnection = lionConnection;
        }

        /// <summary>
        /// 搜尋單一使用者留言
		/// 指定使用者姓名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<MessageListDto>?> GetMsgByUserName(string userName)
        {
            using (var use = _lionConnection.GetLionDb())
            {
                var querySQL = @"select [mb].[MessageBoardId], [m].[MemberName], [m].[Account], [mb].[MessageText], [mb].[MessageTime]
                                 from [dbo].[MessageBoardTable] as mb
                                 inner join [dbo].[MemberTable] as m on mb.MemberId = m.MemberId
                                 where m.MemberName = @MemberName";

                if (querySQL == null) { return null; }

                return await use.QueryAsync<MessageListDto>(querySQL, new { MemberName = userName });
            }
        }
    }
}