using System.Linq;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;

namespace RayPI.Repository.EFRepository.Repository
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
