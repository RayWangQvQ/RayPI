using Ray.EntityFrameworkRepository;
using RayPI.Entity;
using RayPI.IRepository;


namespace RayPI.EntityFrameworkRepository.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(MyDbContext myDbContext) : base(myDbContext)
        { }
    }
}
