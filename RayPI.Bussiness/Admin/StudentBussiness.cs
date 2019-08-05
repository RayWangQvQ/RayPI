using RayPI.Entity;
using RayPI.IRepository;
using RayPI.Model;
using RayPI.Model.ReturnModel;
using RayPI.EntityFrameworkRepository.Repository;
using RayPI.Treasury.Models;
using System.Linq;

namespace RayPI.Bussiness.Admin
{
    public class StudentBussiness
    {
        private IStudentRepository _studentRepository;
        public StudentBussiness(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Student GetById(long id)
        {
            return _studentRepository.FindById(id);
        }

        public PageResult<Student> GetPageList(int pageIndex, int pageSize)
        {
            return _studentRepository.GetPageList<Student>(pageIndex, pageSize);
        }

        public bool Add(Student entity)
        {
            _studentRepository.Add(entity);
            return true;
        }

        public bool Update(Student entity)
        {
            _studentRepository.Update(entity);
            return true;
        }

        public bool Dels(long[] ids)
        {
            _studentRepository.Delete(x => ids.Contains(x.Id));
            return true;
        }
    }
}
