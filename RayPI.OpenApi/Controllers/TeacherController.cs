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
using RayPI.Bussiness;
using RayPI.Entity;
using RayPI.Treasury.Models;

namespace RayPI.Controllers
{
    /// <summary>
    /// 教师接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/Admin")]
    //[Authorize(Roles = "Admin")]
    //[Authorize(Policy = "RequireAdmin")]
    public class TeacherController : Controller
    {
        private TeacherBussiness _teacheBussiness;

        public TeacherController(TeacherBussiness teacheBussiness)
        {
            _teacheBussiness = teacheBussiness;
        }

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
    }
}