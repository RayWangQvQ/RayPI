using RayPI.Entity;
using RayPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.IService
{
    public interface IStudent
    {
        #region base
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        TableModel<Student> GetPageList(int pageIndex, int pageSize);
        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Student Get(long id);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(Student entity);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Update(Student entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Dels(dynamic[] ids);
        #endregion

        Student GetByName(string name);
    }
}
