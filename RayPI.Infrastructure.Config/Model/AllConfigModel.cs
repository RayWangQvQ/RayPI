
namespace RayPI.Infrastructure.Config.Model
{
    public class AllConfigModel
    {
        private readonly JwtAuthConfigModel _jwtAuthConfigModel;

        public AllConfigModel(JwtAuthConfigModel jwtAuthConfigModel)
        {
            _jwtAuthConfigModel = jwtAuthConfigModel;
        }

        public JwtAuthConfigModel JwtAuthConfigModel => _jwtAuthConfigModel;
    }
}
