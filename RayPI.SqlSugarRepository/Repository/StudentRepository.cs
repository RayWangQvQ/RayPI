using RayPI.Domain.Entity;
using RayPI.Domain.IRepository;


namespace RayPI.SqlSugarRepository.Repository
{
    public class StudentRepository : BaseRepository<StudentEntity>, IStudentRepository
    {
        public StudentRepository(MySqlSugarClient sugarClient) : base(sugarClient)
        {

        }

        public StudentEntity GetByName(string name)
        {
            return _sugarClient.Client.Queryable<StudentEntity>().Where(it => it.Name == name).First();
        }
    }
}
