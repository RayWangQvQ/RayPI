using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Bussiness.Client;

namespace RayPI.Controllers
{
    /// <summary>
    /// 客户端
    /// </summary>
    [Produces("application/json")]
    [Route("api/Client")]
    public class ClientController : Controller
    {
        #region 学生
        private CStuentBLL bll = new CStuentBLL();
        /// <summary>
        /// 根据姓名获取学生
        /// </summary>
        /// <param name="name">学生姓名</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Student/GetByName")]
        public JsonResult GetByName(string name = null)
        {
            if (name == null)
                return Json("参数为空");
            return Json(bll.GetByName(name));
        }
        #endregion
    }
}