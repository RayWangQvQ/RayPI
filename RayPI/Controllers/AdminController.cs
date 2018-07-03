using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayPI.Bussiness.Admin;
using RayPI.Entity;

namespace RayPI.Controllers
{
    /// <summary>
    /// 后台
    /// </summary>
    [Produces("application/json")]
    [Route("api/Admin")]
    [Authorize(Policy ="Admin")]
    public class AdminController : Controller
    {
        #region 学生
        private StudentBLL bll = new StudentBLL();
        
        /// <summary>
        /// 获取学生分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Student")]
        public JsonResult GetStudentPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(bll.GetPageList(pageIndex, pageSize));
        }
        
        /// <summary>
        /// 获取单个学生
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Student/{id}")]
        public JsonResult GetStudentById(long id)
        {
            return Json(bll.GetById(id));
        }
        
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Student")]
        public JsonResult Add(Student entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(bll.Add(entity));
        }
        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Student")]
        public JsonResult Update(Student entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(bll.Update(entity));
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Student")]
        public JsonResult Dels(dynamic[] ids = null)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(bll.Dels(ids));
        }
        #endregion
    }
}