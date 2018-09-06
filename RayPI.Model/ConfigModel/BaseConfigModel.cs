using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace RayPI.Model.ConfigModel
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseConfigModel
    {
        /// <summary>
        /// 
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public static string ContentRootPath { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public static string WebRootPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="contentRootPath"></param>
        /// <param name="webRootPath"></param>
        public static void SetBaseConfig(IConfiguration config,string contentRootPath,string webRootPath)
        {
            Configuration = config;
            ContentRootPath = contentRootPath;
            WebRootPath = webRootPath;
        }
    }
}