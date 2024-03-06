using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace prjLion.Service.Interfaces
{
    public interface ITokenService
    {
        public (string Token, DateTime Expiration) CreateToken(IEnumerable<Claim> claims);
    }
}