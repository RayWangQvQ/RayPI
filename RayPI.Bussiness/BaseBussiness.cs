using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using RayPI.Treasury.Helpers;
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
