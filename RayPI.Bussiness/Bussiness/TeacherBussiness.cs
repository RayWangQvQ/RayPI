using RayPI.Entity;
using RayPI.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using RayPI.Treasury.Models;
using System.Linq;

namespace RayPI.Bussiness
{
    public class TeacherBussiness
    {
        private ITeacherRepository _teacherRepository;

        public TeacherBussiness(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public Teacher GetById(long id)
        {
            return _teacherRepository.FindById(id);
        }

        public PageResult<Teacher> GetPageList(int pageIndex, int pageSize)
        {
            return _teacherRepository.GetPageList<Teacher>(pageIndex, pageSize);
        }

        public bool Add(Teacher entity)
        {
            _teacherRepository.Add(entity);
            return true;
        }

        public bool Update(Teacher entity)
        {
            _teacherRepository.Update(entity);
            return true;
        }

        public bool Dels(long[] ids)
        {
            _teacherRepository.Delete(x => ids.Contains(x.Id));
            return true;
        }
    }
}
