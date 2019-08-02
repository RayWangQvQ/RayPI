using RayPI.Entity;
using RayPI.IRepository;


namespace RayPI.EntityFrameworkRepository.Repository
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(MySqlSugarClient sugarClient) : base(sugarClient)
        {

        }

        public Student GetByName(string name)
        {
            return _sugarClient.Client.Queryable<Student>().Where(it => it.Name == name).First();
        }
    }
}
