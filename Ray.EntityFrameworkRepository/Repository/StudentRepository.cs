using Ray.EntityFrameworkRepository;
using RayPI.Entity;
using RayPI.IRepository;
using System.Linq;

namespace RayPI.EntityFrameworkRepository.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(MyDbContext myDbContext) : base(myDbContext)
        { }

        public Student GetByName(string name)
        {
            return GetAllMatching(x => x.Name.Contains(name)).FirstOrDefault();
        }
    }
}
