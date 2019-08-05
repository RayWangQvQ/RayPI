using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Rest.TransientFaultHandling;
using RayPI.Bussiness.Admin;
using RayPI.Entity;
using RayPI.Model;

namespace RayPI.Controllers
{
    /// <summary>
    /// 后台
    /// </summary>
    [Produces("application/json")]
    [Route("api/Admin")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Policy = "RequireAdmin")]
    public class AdminController : Controller
    {
        #region 学生
        private StudentBussiness _studentBussiness;
        private TeacherBussiness _teacheBussiness;

        public AdminController(StudentBussiness studentBussiness, TeacherBussiness teacheBussiness)
        {
            _studentBussiness = studentBussiness;
            _teacheBussiness = teacheBussiness;
        }

        /// <summary>
        /// 获取学生分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">条/页</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Student")]
        public JsonResult GetStudentPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(_studentBussiness.GetPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 获取单个学生
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Student/{id}")]
        [ProducesResponseType(typeof(Student), 200)]
        public JsonResult GetStudentById(long id = 0)
        {
            if (id == 0)
                throw new MyException("参数id不合法", StatusCodes.Status400BadRequest);
            return Json(_studentBussiness.GetById(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">学生实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Student")]
        public JsonResult Add(Student entity = null)
        {
            if (entity == null)
                throw new ArgumentNullException();
            return Json(_studentBussiness.Add(entity));
        }
        /// <summary>
        /// 编辑学生
        /// </summary>
        /// <param name="entity">学生实体</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Student")]
        public JsonResult Update(Student entity = null)
        {
            if (entity == null)
                throw new ArgumentNullException();
            return Json(_studentBussiness.Update(entity));
        }

        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Student")]
        public JsonResult Dels(long[] ids = null)
        {
            if (ids.Length == 0)
                throw new ArgumentNullException();
            return Json(_studentBussiness.Dels(ids));
        }
        #endregion

        #region 教师

        /// <summary>
        /// 获取教师分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">条/页</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Teacher")]
        public JsonResult GetTeacherPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(_teacheBussiness.GetPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 获取单个教师
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Teacher/{id}")]
        [ProducesResponseType(typeof(Teacher), 200)]
        public JsonResult GetTeacherById(long id)
        {
            return Json(_teacheBussiness.GetById(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">学生实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Teacher")]
        public JsonResult AddTeacher(Teacher entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(_teacheBussiness.Add(entity));
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity">学生实体</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Teacher")]
        public JsonResult UpdateTeacher(Teacher entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(_teacheBussiness.Update(entity));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Teacher")]
        public JsonResult DelsTeacher(long[] ids = null)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(_teacheBussiness.Dels(ids));
        }
        #endregion
    }
}