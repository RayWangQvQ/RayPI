using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
//
using RayPI.Bussiness.System;
using RayPI.ConfigService.ConfigModel;
using RayPI.Treasury.Models;
using RayPI.AuthService;


namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 系统接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/Test")]
    [EnableCors("Any")]
    //[Authorize(Policy = "RequireAdminOrClient")]
    public class TestController : Controller
    {
        private EntityBussiness _entityBussiness;
        private IConfiguration _config;
        private IHostingEnvironment _env;
        private JwtAuthConfigModel _jwtAuthConfigModel;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        /// <param name="jwtAuthConfigModel"></param>
        public TestController(IConfiguration configuration, IHostingEnvironment env, JwtAuthConfigModel jwtAuthConfigModel, EntityBussiness entityBLL)
        {
            _config = configuration;
            _env = env;
            _jwtAuthConfigModel = jwtAuthConfigModel;
            _entityBussiness = entityBLL;
        }

        #region 生成实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Entity/Create")]
        public JsonResult CreateEntity(string entityName)
        {
            return Json(_entityBussiness.CreateEntity(entityName, _env.ContentRootPath));
        }
        #endregion

        #region Token
        /// <summary>
        /// 模拟登录，获取JWT
        /// </summary>
        /// <param name="tm"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public string GetJWTStr(TokenModel tm)
        {
            return JwtHelper.IssueJWT(tm, _jwtAuthConfigModel);
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