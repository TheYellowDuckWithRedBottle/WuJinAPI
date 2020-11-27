using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WuJinAPI.JWT
{
    public class JWTConfig
    {
        /// <summary>
        /// token的发布者
        /// </summary>
        public string  Issuer { get; set; }
        /// <summary>
        /// Token的接收者
        /// </summary>
        public string  Audience { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string  IssuerSigningKey { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public int AccessTokenExpiresMinutes { get; set; }
    }
}
