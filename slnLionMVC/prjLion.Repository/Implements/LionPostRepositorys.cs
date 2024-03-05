using AutoMapper;
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
	}
}