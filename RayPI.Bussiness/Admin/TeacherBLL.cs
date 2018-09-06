using RayPI.Entity;
using RayPI.IRepository;
using RayPI.Model;
using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Repository;
using RayPI.Model.ReturnModel;

namespace RayPI.Bussiness.Admin
{
    public class TeacherBLL
    {
        private ITeacher iTeacher = new TeacherDAL();

        public Teacher GetById(long id)
        {
            return iTeacher.Get(id);
        }

        public TableModel<Teacher> GetPageList(int pageIndex, int pageSize)
        {
            return iTeacher.GetPageList(pageIndex, pageSize);
        }

        public bool Add(Teacher entity)
        {
            return iTeacher.Add(entity);
        }

        public bool Update(Teacher entity)
        {
            return iTeacher.Update(entity);
        }

        public bool Dels(dynamic[] ids)
        {
            return iTeacher.Dels(ids);
        }
    }
}
