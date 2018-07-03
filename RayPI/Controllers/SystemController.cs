using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Bussiness.System;
using RayPI.Token;

namespace RayPI.Controllers
{
    /// <summary>
    /// 系统
    /// </summary>
    [Produces("application/json")]
    [Route("api/System")]
    public class SystemController : Controller
    {
        private EntityBLL bll = new EntityBLL();
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public SystemController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        #region 生成实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Entity/Create")]
        public JsonResult CreateEntity(string entityName = null)
        {
            if (entityName == null)
                return Json("参数为空");
            return Json(bll.CreateEntity(entityName, _hostingEnvironment.ContentRootPath));
        }
        #endregion

        #region Token
        /// <summary>
        /// 获取JWT，并存入缓存
        /// </summary>
        /// <param name="id">用户Id</param>
        /// <param name="sub">身份</param>
        /// <param name="expiresSliding">相对过期时间，单位为分</param>
        /// <param name="expiresAbsoulute">绝对过期时间，单位为天</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token")]
        public JsonResult GetJWTStr(long id, string sub, int expiresSliding = 30, int expiresAbsoulute = 30)
        {
            Token.Model.TokenModel tokenModel = new Token.Model.TokenModel();
            tokenModel.Uid = id;
            tokenModel.Sub = sub;

            DateTime d1 = DateTime.Now;
            DateTime d2 = d1.AddMinutes(expiresSliding);
            DateTime d3 = d1.AddDays(expiresAbsoulute);
            TimeSpan sliding = d2 - d1;
            TimeSpan absoulute = d3 - d1;

            string jwtStr = RayPIToken.IssueJWT(tokenModel, sliding, absoulute);
            return Json(jwtStr);
        }
        #endregion
    }
}