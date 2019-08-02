using RayPI.Treasury.Models;


namespace RayPI.Bussiness
{
    public abstract class BaseBussiness
    {
        public static TokenModel TokenModel;

        public static void SetTokenModel(TokenModel tm)
        {
            TokenModel = tm;
        }
    }
}
