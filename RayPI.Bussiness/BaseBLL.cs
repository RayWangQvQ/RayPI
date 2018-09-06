using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using RayPI.Helper;

namespace RayPI.Bussiness
{
    public abstract class BaseBLL
    {
        public static TokenModel TokenModel;

        public static void SetTokenModel(TokenModel tm)
        {
            TokenModel = tm;
        }
    }
}
