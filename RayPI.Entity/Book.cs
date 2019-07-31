using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace RayPI.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Book")]
    public partial class Book
    {
        /// <summary>
        /// 
        /// </summary>
        public Book()
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
        /// Desc:书名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Title { get; set; }

        /// <summary>
        /// Desc:作者
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string Writer { get; set; }

    }
}
