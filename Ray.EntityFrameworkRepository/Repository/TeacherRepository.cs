using Ray.EntityFrameworkRepository;
using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;


namespace RayPI.EntityFrameworkRepository.Repository
{
    public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(MyDbContext myDbContext) : base(myDbContext)
        {

        }
    }
}
