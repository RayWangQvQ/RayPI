using RayPI.Entity;
using RayPI.IRepository;


namespace RayPI.EntityFrameworkRepository.Repository
{
    public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(MySqlSugarClient sugarClient) : base(sugarClient)
        {

        }
    }
}
