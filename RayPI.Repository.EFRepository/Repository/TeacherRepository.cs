using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;


namespace RayPI.Repository.EFRepository.Repository
{
    public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(MyDbContext myDbContext) : base(myDbContext)
        {

        }
    }
}
