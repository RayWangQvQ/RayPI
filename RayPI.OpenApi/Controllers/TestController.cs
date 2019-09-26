//微软包
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//本地项目包
using RayPI.Infrastructure.Auth.Models;
using RayPI.Infrastructure.Auth.Enums;
using RayPI.Infrastructure.Auth.Jwt;
using RayPI.Infrastructure.Auth.Attributes;
using RayPI.Infrastructure.Config;
using RayPI.Infrastructure.Config.FrameConfigModel;
using RayPI.Infrastructure.RayException;
using RayPI.Infrastructure.Cors.Attributes;
using RayPI.Infrastructure.Cors.Enums;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 系统接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/Test")]
    [RayCors(CorsPolicyEnum.Free)]
    //[RayAuthorize(AuthPolicyEnum.RequireRoleOfAdminOrClient)]
    public class TestController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AllConfigModel _allConfigModel;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// 
        /// </summary>
        public TestController(IConfiguration configuration,
            AllConfigModel allConfigModel,
            IJwtService jwtService)
        {
            _config = configuration;
            _allConfigModel = allConfigModel;
            _jwtService = jwtService;
        }

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
        [RayAuthorizeFree]
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
            return _jwtService.IssueJwt(tm);
        }

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

        /// <summary>
        /// 测试异常2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException2")]
        public JsonResult TestException2()
        {
            throw new System.Exception("测试");
        }

        /// <summary>
        /// 测试异常3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestException3")]
        public JsonResult TestException3()
        {
            throw new RayAppException();
        }

        /// <summary>
        /// 测试配置1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestConfig1")]
        public string TestConfig1()
        {
            return _allConfigModel.TestConfigModel.Key1;
        }

        /// <summary>
        /// 测试配置2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TestConfig2")]
        public string TestConfig2()
        {
            return _config["Test:Key1"];
        }
    }
}