using Ray.EntityFrameworkRepository;
using RayPI.Entity;
using RayPI.IRepository;


namespace RayPI.EntityFrameworkRepository.Repository
{
    public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(MyDbContext myDbContext) : base(myDbContext)
        {

        }
    }
}
