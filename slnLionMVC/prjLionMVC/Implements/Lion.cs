using prjLionMVC.Interfaces;
using prjLionMVC.Models;
using prjLionMVC.Models.Entity;
using System.Security.Cryptography;
using System.Text;

namespace prjLionMVC.Implements
{
    public class Lion : ILion
    {
        private readonly LionHwContext _lionHwContext;

        public Lion(LionHwContext lionHwContext)
        {
            _lionHwContext = lionHwContext;
        }

        /// <summary>
        /// 留言版清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MsgListDto> GetAllMsg()
        {
            return _lionHwContext.MessageBoardTables.Join(
                _lionHwContext.MemberTables,
                mb => mb.MemberId,
                m => m.MemberId,
                (mb, m) => new MsgListDto
                {
                    MessageBoardId = mb.MemberId,
                    MemberName = m.MemberName,
                    Account = m.Account,
                    MessageText = mb.MessageText,
                    MessageTime = mb.MessageTime,
                });
        }

        /// <summary>
        /// 建立帳號
        /// </summary>
        /// <param name="createAccountDto"></param>
        /// <returns></returns>
        public bool CreateMember(CreateAccountDto createAccountDto)
        {
            var salt = RandomSalt();
            var hasPwd = HashPwdWithHMACSHA256(createAccountDto.HashPassword, salt);

            var mapper = new MemberTable
            {
                MemberName = createAccountDto.MemberName,
                Account = createAccountDto.Account,
                HashPassword = hasPwd,
                SaltPassword = salt
            };

            try
            {
                _lionHwContext.MemberTables.Add(mapper);
                _lionHwContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return true;
        }

        /// <summary>
        /// 會員登入
        /// </summary>
        /// <param name="loginAccountDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CheckMember(string account, string password)
        {
            var queryResult = _lionHwContext.MemberTables.FirstOrDefault(m => m.Account == account);

            if(queryResult != null)
            {
                var HashPasswordTemp = queryResult.HashPassword;
                var SaltPasswordTemp = queryResult.SaltPassword;
                var HashPassword = HashPwdWithHMACSHA256(password, SaltPasswordTemp);

                return HashPassword == HashPasswordTemp;
            }

            return false;
        }

        /// <summary>
        /// 亂數產生大小
        /// </summary>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        private int RandomNumberSize(int minNum, int maxNum)
        {
            byte[] intBytes = new byte[4];

            RandomNumberGenerator.Fill(intBytes);

            int randomInt = BitConverter.ToInt32(intBytes, 0);

            return Math.Abs(randomInt % (maxNum - minNum)) + minNum;
        }

        /// <summary>
        /// 亂數產生鹽值
        /// </summary>
        /// <param name="minNum"></param>
        /// <param name="maxNum"></param>
        /// <returns></returns>
        private string RandomSalt(int minNum = 8, int maxNum = 256)
        {
            int size = RandomNumberSize(minNum, maxNum);
            var buffer = new byte[size];

            RandomNumberGenerator.Fill(buffer);

            return Convert.ToBase64String(buffer);
        }

        /// <summary>
        /// 密碼雜湊 & 鹽值
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string HashPwdWithHMACSHA256(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var hmac = new HMACSHA256(saltBytes))
            {
                var pwdBytes = Encoding.UTF8.GetBytes(password);
                var hash = hmac.ComputeHash(pwdBytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}