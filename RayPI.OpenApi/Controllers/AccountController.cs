using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Security.Models;
using RayPI.Infrastructure.Security.Services;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 账号接口
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 登录获取token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public async Task<JsonResult> Login(string userName = "stuAdmin", string pwd = "123456", string roleName = "学生管理员")
        {
            // 用户名是否正确
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pwd) || string.IsNullOrWhiteSpace(roleName))
            {
                return new JsonResult(new
                {
                    Code = 0,
                    Message = "账号不存在",
                });
            }
            //密码是否正确
            if (!((userName == "stuAdmin" || userName == "bb" || userName == "cc") && pwd == "123456"))
            {
                return new JsonResult(new
                {
                    Code = 0,
                    Message = "密码错误",
                });
            }

            // 你自己定义的角色/用户信息服务
            RoleService roleService = new RoleService();

            // 检验用户是否属于此角色
            var role = roleService.IsUserToRole(userName, roleName);

            // 用于加密解密的类
            EncryptionHash hash = new EncryptionHash();

            // 设置用户标识
            Claim[] userClaims = hash.BuildClaims(userName, roleName);

            //// 自定义构建配置用户标识
            /// 自定义的话，至少包含如下标识
            //var userClaims = new Claim[]
            //{
            //new Claim(ClaimTypes.Name, userName),
            //    new Claim(ClaimTypes.Role, roleName),
            //    new Claim(JwtRegisteredClaimNames.Aud, Audience),
            //    new Claim(ClaimTypes.Expiration, TimeSpan.TotalSeconds.ToString()),
            //    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
            //};
            /*
            iss (issuer)：签发人
            exp (expiration time)：过期时间
            sub (subject)：主题
            aud (audience)：受众
            nbf (Not Before)：生效时间
            iat (Issued At)：签发时间
            jti (JWT ID)：编号
            */



            // 方法一，直接颁发 Token
            ResponseToken token = hash.BuildToken(userClaims);


            //方法二，拆分多步，颁发 token，方便调试
            //var identity = hash.GetIdentity(userClaims);
            //var jwt = hash.BuildJwtToken(userClaims);
            //var token = hash.BuildJwtResponseToken(jwt);

            return new JsonResult(token);
        }
    }
}