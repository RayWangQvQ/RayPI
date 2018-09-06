using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Bussiness.System;
using RayPI.Helper;
using RayPI.Model;
using RayPI.Model.ConfigModel;

namespace RayPI.Controllers
{
    /// <summary>
    /// 系统
    /// </summary>
    [Produces("application/json")]
    [Route("api/System")]
    [EnableCors("Any")]
    //[Authorize(Policy = "RequireAdminOrClient")]
    public class SystemController : Controller
    {
        private EntityBLL bll = new EntityBLL();

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
            return Json(bll.CreateEntity(entityName, BaseConfigModel.ContentRootPath));
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
        public JsonResult GetJWTStr(TokenModel tm)
        {
            return Json(JwtHelper.IssueJWT(tm));
        }
        #endregion
    }
}