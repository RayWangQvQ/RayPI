using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayPI.ConfigService.ConfigModel
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
