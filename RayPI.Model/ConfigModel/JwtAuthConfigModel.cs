using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace RayPI.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtAuthConfigModel:BaseConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public JwtAuthConfigModel()
        {
            try
            {
                JWTSecretKey = Configuration["JwtAuth:SecurityKey"];
                WebExp = double.Parse(Configuration["JwtAuth:WebExp"]);
                AppExp = double.Parse(Configuration["JwtAuth:AppExp"]);
                MiniProgramExp = double.Parse(Configuration["JwtAuth:MiniProgramExp"]);
                OtherExp = double.Parse(Configuration["JwtAuth:OtherExp"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JWTSecretKey = "This is JWT Secret Key";
        /// <summary>
        /// 
        /// </summary>
        public double WebExp = 12;
        /// <summary>
        /// 
        /// </summary>
        public double AppExp = 12;
        /// <summary>
        /// 
        /// </summary>
        public double MiniProgramExp = 12;
        /// <summary>
        /// 
        /// </summary>
        public double OtherExp = 12;
    }
}
