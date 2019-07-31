using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Bussiness.Client;
using RayPI.Entity;

namespace RayPI.Controllers
{
    /// <summary>
    /// 客户端
    /// </summary>
    [Produces("application/json")]
    [Route("api/Client")]
    //[Authorize(Roles = "Client")]
    [EnableCors("Limit")]
    public class ClientController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientController()
        {

        }

        #region 学生
        private CStuentBussiness bll = new CStuentBussiness();
        /// <summary>
        /// 根据姓名获取学生
        /// </summary>
        /// <remarks>精确查询</remarks>
        /// <param name="name">学生姓名</param>
        /// <returns></returns>
        [HttpGet()]
        [Route("Student/GetByName")]
        [Produces(typeof(Student))]
        public JsonResult GetByName(string name = null)
        {
            if (name == null)
                throw new ArgumentNullException();
            return Json(bll.GetByName(name));
        }
        #endregion

        /// <summary>
        /// 测试异常
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Student/TestException")]
        public JsonResult TestException()
        {
            string s = null;
            return Json(s.Length);
        }
    }
}