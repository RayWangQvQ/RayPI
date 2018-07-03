using RayPI.Entity;
using RayPI.IService;
using RayPI.Model;

namespace RayPI.Bussiness.Admin
{
    public class StudentBLL
    {
        private IStudent IService =new Service.StudentService();

        public Student GetById(long id)
        {
            return IService.Get(id);
        }

        public TableModel<Student> GetPageList(int pageIndex,int pageSize)
        {
            return IService.GetPageList(pageIndex, pageSize);
        }

        public MessageModel<Student> Add(Student entity)
        {
            if (IService.Add(entity))
                return new MessageModel<Student> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Student> { Success = false, Msg = "操作失败" };
        }

        public MessageModel<Student> Update(Student entity)
        {
            if(IService.Update(entity))
                return new MessageModel<Student> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Student> { Success = false, Msg = "操作失败" };
        }

        public MessageModel<Student> Dels(dynamic[] ids)
        {
            if (IService.Dels(ids))
                return new MessageModel<Student> { Success = true, Msg = "操作成功" };
            else
                return new MessageModel<Student> { Success = false, Msg = "操作失败" };
        }
    }
}
