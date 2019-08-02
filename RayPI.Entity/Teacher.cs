using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace RayPI.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Teacher")]
    public partial class Teacher : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Teacher()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Tid { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

    }
}
