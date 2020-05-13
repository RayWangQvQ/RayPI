using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;
using RayPI.Infrastructure.Treasury.Models;

namespace RayPI.Bussiness
{
    public class TeacherBusiness
    {
        private ITeacherRepository _teacherRepository;

        public TeacherBusiness(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public TeacherEntity GetById(long id)
        {
            return _teacherRepository.FindById(id);
        }

        public PageResult<TeacherEntity> GetPageList(int pageIndex, int pageSize)
        {
            return _teacherRepository.GetPageList<TeacherEntity>(pageIndex, pageSize);
        }

        public bool Add(TeacherEntity entity)
        {
            _teacherRepository.Add(entity);
            return true;
        }

        public bool Update(TeacherEntity entity)
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
