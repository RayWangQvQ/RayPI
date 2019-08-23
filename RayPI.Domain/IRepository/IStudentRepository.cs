using RayPI.Domain.Entity;

namespace RayPI.Domain.IRepository
{
    public interface IStudentRepository:IBaseRepository<StudentEntity>
    {
        StudentEntity GetByName(string name);
    }
}
