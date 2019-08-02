using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace RayPI.Entity
{
    ///<summary>
    ///学生实体
    ///</summary>
    [SugarTable("Student")]
    public partial class Student : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Student()
        {
        }
        /// <summary>
        /// Desc:Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long Tid { get; set; }
        /// <summary>
        /// Desc:班级Id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public long ClassId { get; set; }
        /// <summary>
        /// Desc:姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Name { get; set; }

    }
}
