using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Dapper;
using prjLion.Common;
using prjLion.Repository.Helpers;
using prjLion.Repository.Interfaces;
using prjLion.Repository.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Repository.Implements
{
	public class LionPostRepositorys : ILionPostRepositorys
	{
		private readonly ILionConnection _lionConnection;
		private readonly ILionGetRepositorys _lionGetRepositorys;
		private readonly IMapper _mapper;

		public LionPostRepositorys(ILionConnection lionConnection, ILionGetRepositorys lionGetRepositorys, IMapper mapper)
		{
			_lionConnection = lionConnection;
			_lionGetRepositorys = lionGetRepositorys;
			_mapper = mapper;
		}

		/// <summary>
		/// 註冊帳號
		/// </summary>
		/// <param name="memberDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> InsertAccount(MemberAccountDto memberDto)
		{
			using (var use = _lionConnection.GetLionDb())
			{
				var actionSQL = @"insert into [dbo].[MemberTable] ([MemberName], [Account], [HashPassword], [SaltPassword])
                                  values (@MemberName, @Account, @HashPassword, @SaltPassword)";

				var parameters = new DynamicParameters();

				parameters.Add("MemberName", memberDto.MemberName, DbType.String);
				parameters.Add("Account", memberDto.Account, DbType.String);
				parameters.Add("HashPassword", memberDto.HashPassword, DbType.String);
				parameters.Add("SaltPassword", memberDto.SaltPassword, DbType.String);

				await use.ExecuteAsync(actionSQL, parameters);

				return true;
			}
		}

		/// <summary>
		/// 新增留言
		/// </summary>
		/// <param name="createMsgDto"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> InsertMsg(CreateMsgDto createMsgDto)
		{
			using (var use = _lionConnection.GetLionDb())
			{
				var actionSQL = @"insert into [dbo].[MessageBoardTable] ([MemberId], [MessageText], [MessageTime])
                                  values (@MemberId, @MessageText, @MessageTime)";

				var parameters = new DynamicParameters();

				parameters.Add("MemberId", createMsgDto.MemberId, DbType.Int32);
				parameters.Add("MessageText", createMsgDto.MessageText, DbType.String);
				parameters.Add("MessageTime", createMsgDto.MessageTime, DbType.DateTime);

				await use.ExecuteAsync(actionSQL, parameters);

				return true;
			}
		}

		/// <summary>
		/// 刪除留言
		/// 指定留言編號 (流水號)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<bool> DeleteMsg(int id)
		{
			using (var use = _lionConnection.GetLionDb())
			{
				var actionSQL = @"delete from [dbo].[MessageBoardTable] where [MessageBoardId] = @MessageBoardId";

				await use.ExecuteAsync(actionSQL, new { MessageBoardId = id });

				return true;
			}
		}

        /// <summary>
        /// 修改留言
        /// 指定留言編號 (流水號)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editMsgDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> UpdateMsg(int id, EditMsgDto editMsgDto)
        {
			using (var use = _lionConnection.GetLionDb())
			{
				var actionSQL = @"update [dbo].[MessageBoardTable] set MessageText = @MessageText, MessageTime = @MessageTime
							  where MessageBoardId = @MessageBoardId";

                var parameters = new DynamicParameters();

                parameters.Add("MessageBoardId", id, DbType.Int32);
                parameters.Add("MessageText", editMsgDto.MessageText, DbType.String);
                parameters.Add("MessageTime", editMsgDto.MessageTime, DbType.DateTime);

				await use.ExecuteAsync(actionSQL, parameters);

				return true;
            }
        }

        public async Task<bool> InsertPicture(CreateImgDto createImgDto)
        {
			using (var use = _lionConnection.GetLionDb())
			{
				var actionSQL = @"insert into [dbo].[PictureTable] ([MemberId] ,[PictureName] ,[CreateTime])
								  values (@MemberId, @PictureName, @CreateTime);";

				var parameters = new DynamicParameters();

                parameters.Add("MemberId", createImgDto.MemberId, DbType.Int32);
                parameters.Add("PictureName", createImgDto.PictureName, DbType.String);
                parameters.Add("CreateTime", createImgDto.CreateTime, DbType.DateTime);

				await use.ExecuteAsync(actionSQL, parameters);

				return true;
            }
        }
    }
}