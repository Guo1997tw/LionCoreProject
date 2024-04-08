using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prjLion.Common
{
    public class CustomizedMethod
    {
        /// <summary>
		/// 亂數產生大小
		/// </summary>
		/// <param name="minNum"></param>
		/// <param name="maxNum"></param>
		/// <returns></returns>
		public int RandomNumberSize(int minNum, int maxNum)
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
		public string RandomSalt(int minNum = 8, int maxNum = 256)
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
		public string HashPwdWithHMACSHA256(string password, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            using (var hmac = new HMACSHA256(saltBytes))
            {
                var pwdBytes = Encoding.UTF8.GetBytes(password);
                var hash = hmac.ComputeHash(pwdBytes);

                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// 檢核正則表示邏輯
        /// 輸入帳號、密碼判斷是否符合規則
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <exception cref="Exception"></exception>
        public void isVerifyRuleAP(string account, string password)
        {
            var accountRule = new Regex(@"^[A-Za-z0-9_]+$");
            var passwordRule = new Regex(@"^\S+$");

            if (!accountRule.IsMatch(account))
            {
                throw new Exception("帳號欄位只能有字母、數字、底線");
            }

            if (!passwordRule.IsMatch(password))
            {
                throw new Exception("密碼欄位不允許空格");
            }
		}

        /// <summary>
        /// 檢核正則表示邏輯
        /// 輸入內容判斷是否符合規則
        /// </summary>
        /// <param name="messageText"></param>
        /// <exception cref="Exception"></exception>

        public void isVerifyRuleMsg(string messageText)
        {
            var textRule = new Regex(@"^[0-9a-zA-Z\u4e00-\u9fa5]+$");

            if(!textRule.IsMatch(messageText))
            {
                throw new Exception("留言欄位只允許中文、英文、數字");
            }
        }

        /// <summary>
        /// 輸入圖片名稱判斷是否符合規則
        /// </summary>
        /// <param name="pictureName"></param>
        /// <exception cref="Exception"></exception>
        public void isVerifyPictureContent(string pictureName)
        {
            var textRule = new Regex(@"^[^\W_]+$");

            if(!textRule.IsMatch(pictureName))
            {
                throw new Exception("圖片名稱只允許中文、英文、數字");
            }
        }
    }
}