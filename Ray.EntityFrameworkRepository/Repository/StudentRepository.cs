using Ray.EntityFrameworkRepository;
using RayPI.Entity;
using RayPI.IRepository;
using System.Linq;

namespace RayPI.EntityFrameworkRepository.Repository
{
    public class StudentRepository : BaseRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(MyDbContext myDbContext) : base(myDbContext)
        { }

        public StudentEntity GetByName(string name)
        {
            return GetAllMatching(x => x.Name.Contains(name)).FirstOrDefault();
        }
    }
}
