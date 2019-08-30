//微软包
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//本地项目包
using RayPI.Infrastructure.Auth.Models;
using RayPI.Infrastructure.Auth.Enums;
using RayPI.Infrastructure.Auth.Jwt;
using RayPI.Infrastructure.Config.Model;
using RayPI.Infrastructure.Auth.Attribute;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 系统接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/Test")]
    [EnableCors("Any")]
    [ApiAuthorize(PolicyEnum.RequireRoleOfAdminOrClient)]
    public class TestController : Controller
    {
        private IConfiguration _config;
        private IHostingEnvironment _env;
        private JwtAuthConfigModel _jwtAuthConfigModel;
        private IJwtService _jwtService;

        public TestController(IConfiguration configuration,
            IHostingEnvironment env,
            JwtAuthConfigModel jwtAuthConfigModel,
            IJwtService jwtService)
        {
            _config = configuration;
            _env = env;
            _jwtAuthConfigModel = jwtAuthConfigModel;
            _jwtService = jwtService;
        }

        #region Token
        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="uname"></param>
        /// <param name="role"></param>
        /// <param name="project"></param>
        /// <param name="tokenType"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public string GetJWTStr(long uid = 1, string uname = "Admin", string role = "Admin", string project = "RayPI", TokenTypeEnum tokenType = TokenTypeEnum.Web)
        {
            var tm = new TokenModel
            {
                Uid = uid,
                Uname = uname,
                Role = role,
                Project = project,
                TokenType = tokenType
            };
            return _jwtService.IssueJWT(tm, _jwtAuthConfigModel);
        }
        #endregion

        /// <summary>
        /// 测试异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException")]
        public JsonResult TestException()
        {
            string s = null;
            return Json(s.Length);
        }
    }
}