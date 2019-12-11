using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Infrastructure.Auth;
using RayPI.Infrastructure.Auth.Jwt;
using RayPI.Infrastructure.Config;
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
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly AllConfigModel _allConfigModel;

        public AccountController(IAuthService authService, IJwtService jwtService, AllConfigModel allConfigModel)
        {
            _authService = authService;
            _jwtService = jwtService;
            _allConfigModel = allConfigModel;
        }
        /// <summary>
        /// 登录获取token
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="pwd"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public JsonResult Login(string userName = "StuAdmin", string pwd = "123456")
        {
            List<string> GetRoleCodeList()
            {
                switch (userName)
                {
                    case "Admin": return new List<string> { "Admin" };
                    case "StuAdmin": return new List<string> { "StudentAdmin" };
                    case "TeacherAdmin": return new List<string> { "TeacherAdmin" };
                    case "Test":
                        return new List<string> { "StuAdmin", "TeacherAdmin" };
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            string tokenStr = _authService.GetToken(userName, GetRoleCodeList());

            return new JsonResult(tokenStr);
        }
    }
}