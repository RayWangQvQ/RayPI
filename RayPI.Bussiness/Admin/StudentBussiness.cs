using RayPI.Entity;
using RayPI.IRepository;
using RayPI.Model;
using RayPI.Model.ReturnModel;
using RayPI.EntityFrameworkRepository.Repository;
using RayPI.Treasury.Models;

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
            return _studentRepository.GetPageList(pageIndex, pageSize);
        }

        public bool Add(Student entity)
        {
            return _studentRepository.Add(entity);
        }

        public bool Update(Student entity)
        {
            return _studentRepository.Update(entity);
        }

        public bool Dels(dynamic[] ids)
        {
            return _studentRepository.Dels(ids);
        }
    }
}
