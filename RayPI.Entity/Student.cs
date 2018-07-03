using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace RayPI.Entity
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Student")]
    public partial class Student
    {
           public Student(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public long Tid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public long ClassId {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

    }
}
