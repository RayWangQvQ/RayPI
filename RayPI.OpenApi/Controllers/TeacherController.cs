//微软包
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//本地项目包
using RayPI.Bussiness;
using RayPI.Domain.Entity;
using RayPI.Infrastructure.Auth.Attributes;
using RayPI.Infrastructure.Auth.Enums;

namespace RayPI.OpenApi.Controllers
{
    /// <summary>
    /// 教师接口
    /// </summary>
    [Produces("application/json")]
    [Route("api/Teacher")]
    public class TeacherController : Controller
    {
        private readonly TeacherBusiness _teacheBusiness;

        /// <summary>
        /// 
        /// </summary>
        public TeacherController(TeacherBusiness teacheBussiness)
        {
            _teacheBusiness = teacheBussiness;
        }

        /// <summary>
        /// 获取教师分页列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">条/页</param>
        /// <returns></returns>
        [HttpGet]
        [RayAuthorize(OperateEnum.Retrieve, ResourceEnum.Teacher)]
        public JsonResult GetTeacherPageList(int pageIndex = 1, int pageSize = 10)
        {
            return Json(_teacheBusiness.GetPageList(pageIndex, pageSize));
        }

        /// <summary>
        /// 获取单个教师
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpGet("id")]
        [ProducesResponseType(typeof(TeacherEntity), 200)]
        public JsonResult GetTeacherById(long id)
        {
            return Json(_teacheBusiness.GetById(id));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">学生实体</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddTeacher(TeacherEntity entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(_teacheBusiness.Add(entity));
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity">学生实体</param>
        /// <returns></returns>
        [HttpPut]
        public JsonResult UpdateTeacher(TeacherEntity entity = null)
        {
            if (entity == null)
                return Json("参数为空");
            return Json(_teacheBusiness.Update(entity));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id集合</param>
        /// <returns></returns>
        [HttpDelete]
        public JsonResult DelsTeacher(long[] ids = null)
        {
            if (ids.Length == 0)
                return Json("参数为空");
            return Json(_teacheBusiness.Dels(ids));
        }
    }
}