namespace RayPI.Entity
{
    ///<summary>
    ///学生实体
    ///</summary>
    public partial class StudentEntity : EntityBase
    {
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
