using RayPI.Domain.Entity;

namespace RayPI.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStudentRepository:IBaseRepository<StudentEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        StudentEntity GetByName(string name);
    }
}
