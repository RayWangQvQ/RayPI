using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;


namespace RayPI.SqlSugarRepository.Repository
{
    public class TeacherRepository : BaseRepository<TeacherEntity>, ITeacherRepository
    {
        public TeacherRepository(MySqlSugarClient sugarClient) : base(sugarClient)
        {

        }
    }
}
