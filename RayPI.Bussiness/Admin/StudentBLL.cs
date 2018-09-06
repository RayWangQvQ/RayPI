using RayPI.Entity;
using RayPI.IRepository;
using RayPI.Model;
using RayPI.Model.ReturnModel;
using RayPI.Repository;

namespace RayPI.Bussiness.Admin
{
    public class StudentBLL
    {
        private IStudent iStudent =new StudentDAL();

        public Student GetById(long id)
        {
            return iStudent.Get(id);
        }

        public TableModel<Student> GetPageList(int pageIndex,int pageSize)
        {
            return iStudent.GetPageList(pageIndex, pageSize);
        }

        public bool Add(Student entity)
        {
            return iStudent.Add(entity);
        }

        public bool Update(Student entity)
        {
            return iStudent.Update(entity);
        }

        public bool Dels(dynamic[] ids)
        {
            return iStudent.Dels(ids);
        }
    }
}
